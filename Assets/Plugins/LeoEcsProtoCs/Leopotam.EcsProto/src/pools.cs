// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.IO;
using System.Runtime.CompilerServices;
#if ENABLE_IL2CPP
using UnityEngine.Scripting;
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.EcsProto {
    public interface IProtoPool {
        void Init (ushort id, ProtoWorld host, Func<ProtoEntity> entityCreator);
        Type ItemType ();
        ushort Id ();
        ProtoWorld World ();
        void NewEntityRaw (out ProtoEntity entity);
        bool Has (ProtoEntity entity);
        void Del (ProtoEntity entity);
        void AddRaw (ProtoEntity entity);
        object Raw (ProtoEntity entity);
        void SetRaw (ProtoEntity entity, object dataRaw);
        void AddBlocker (int amount);
        void Resize (int cap);
        int Len ();
        ProtoEntity[] Entities ();
        void Copy (ProtoEntity srcEntity, ProtoEntity dstEntity);
        bool Serialize (ProtoEntity entity, Stream writer);
        bool Deserialize (ProtoEntity entity, Stream reader);
    }

    public interface IProtoAutoReset<T> where T : struct {
        void AutoReset (ref T c);
    }

    public interface IProtoAutoCopy<T> where T : struct {
        void AutoCopy (ref T src, ref T dst);
    }

    public interface IProtoAutoSerialize<T> where T : struct {
        void AutoSerialize (ref T c, Stream writer);
        void AutoDeserialize (ref T c, Stream reader);
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public class ProtoPool<T> : IProtoPool where T : struct {
        const int DefaultCapacity = 128;
        int _initCap;
        ushort _id;
        ProtoWorld _world;
        Func<ProtoEntity> _entityCreator;
        ProtoEntity[] _dense;
#if LEOECSPROTO_SMALL_WORLD
        ushort[] _sparse;
        ushort _len;
#else
        int[] _sparse;
        int _len;
#endif
        T[] _data;
        Type _itemType;
        AutoResetHandler _autoResetHandler;
        AutoCopyHandler _autoCopyHandler;
        AutoSerializeHandler _autoSerializeHandler;
        AutoDeserializeHandler _autoDeserializeHandler;
        T _default = default;
#if DEBUG
        int _blockers;
#endif
#if ENABLE_IL2CPP
        [Preserve]
#endif
        public ProtoPool () : this (0) { }

        public ProtoPool (int capacity) {
            _initCap = capacity;
        }

        void IProtoPool.Init (ushort id, ProtoWorld world, Func<ProtoEntity> entityCreator) {
#if DEBUG
            if (_world != null) { throw new Exception ($"пул компонентов \"{DebugHelpers.CleanTypeName (_itemType)}\" уже привязан к миру"); }
            _blockers = 0;
#endif
            if (_initCap == 0) {
                _initCap = DefaultCapacity;
            }
            _dense = new ProtoEntity[_initCap];
            _data = new T[_initCap];
            _len = 0;
            _itemType = typeof (T);
            var arType = typeof (IProtoAutoReset<T>);
            if (arType.IsAssignableFrom (_itemType)) {
                var searchMethod = arType.GetMethod (nameof (IProtoAutoReset<T>.AutoReset));
                foreach (var m in _itemType.GetInterfaceMap (arType).InterfaceMethods) {
                    if (m == searchMethod) {
                        _autoResetHandler =
                            (AutoResetHandler) Delegate.CreateDelegate (typeof (AutoResetHandler), _default, m!);
                        break;
                    }
                }
            }
            var acType = typeof (IProtoAutoCopy<T>);
            if (acType.IsAssignableFrom (_itemType)) {
                var searchMethod = acType.GetMethod (nameof (IProtoAutoCopy<T>.AutoCopy));
                foreach (var m in _itemType.GetInterfaceMap (acType).InterfaceMethods) {
                    if (m == searchMethod) {
                        _autoCopyHandler =
                            (AutoCopyHandler) Delegate.CreateDelegate (typeof (AutoCopyHandler), _default, m!);
                        break;
                    }
                }
            }
            var asType = typeof (IProtoAutoSerialize<T>);
            if (asType.IsAssignableFrom (_itemType)) {
                var searchMethod = asType.GetMethod (nameof (IProtoAutoSerialize<T>.AutoSerialize));
                foreach (var m in _itemType.GetInterfaceMap (asType).InterfaceMethods) {
                    if (m == searchMethod) {
                        _autoSerializeHandler =
                            (AutoSerializeHandler) Delegate.CreateDelegate (typeof (AutoSerializeHandler), _default, m!);
                        break;
                    }
                }
                searchMethod = asType.GetMethod (nameof (IProtoAutoSerialize<T>.AutoDeserialize));
                foreach (var m in _itemType.GetInterfaceMap (asType).InterfaceMethods) {
                    if (m == searchMethod) {
                        _autoDeserializeHandler =
                            (AutoDeserializeHandler) Delegate.CreateDelegate (typeof (AutoDeserializeHandler), _default, m!);
                        break;
                    }
                }
            }
            _id = id;
            _world = world;
            _entityCreator = entityCreator;
#if LEOECSPROTO_SMALL_WORLD
            _sparse = new ushort[_world.EntityGens ().Cap ()];
#else
            _sparse = new int[_world.EntityGens ().Cap ()];
#endif
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public Type ItemType () {
            if (_itemType == null) {
                _itemType = typeof (T);
            }
            return _itemType;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ushort Id () => _id;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ProtoWorld World () => _world;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ref T NewEntity (out ProtoEntity entity) {
            entity = _entityCreator ();
            return ref Add (entity);
        }

        void IProtoPool.NewEntityRaw (out ProtoEntity entity) {
            NewEntity (out entity);
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ref T Get (ProtoEntity entity) {
#if DEBUG
            if (_world.EntityGens ().Get (entity._id) < 0) { throw new Exception ("не могу получить доступ к удаленной сущности"); }
            if (_sparse[entity._id] == 0) { throw new Exception ($"компонент \"{DebugHelpers.CleanTypeName (_itemType)}\" отсутствует на сущности"); }
#endif
            return ref _data[_sparse[entity._id] - 1];
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Has (ProtoEntity entity) {
#if DEBUG
            if (_world.EntityGens ().Get (entity._id) < 0) { throw new Exception ("не могу получить доступ к удаленной сущности"); }
#endif
            return _sparse[entity._id] > 0;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ref T Add (ProtoEntity entity) {
#if DEBUG
            if (Has (entity)) { throw new Exception ($"не могу добавить компонент \"{DebugHelpers.CleanTypeName (_itemType)}\", он уже существует"); }
            if (_blockers > 1) { throw new Exception ($"нельзя изменить пул компонентов \"{DebugHelpers.CleanTypeName (_itemType)}\", он находится в режиме \"только чтение\" из-за множественного доступа"); }
#endif
            if (_dense.Length == _len) {
                Array.Resize (ref _dense, _len << 1);
                Array.Resize (ref _data, _len << 1);
            }

            var idx = _len;
            _len++;
            _dense[idx] = entity;
            _sparse[entity._id] = _len;

            _autoResetHandler?.Invoke (ref _data[idx]);

            _world.SetEntityMaskBit (entity, _id);

            return ref _data[idx];
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void Del (ProtoEntity entity) {
#if DEBUG
            if (_world.EntityGens ().Get (entity._id) < 0) { throw new Exception ("не могу получить доступ к удаленной сущности"); }
            if (_blockers > 1) { throw new Exception ($"нельзя изменить пул компонентов \"{DebugHelpers.CleanTypeName (_itemType)}\", он находится в режиме \"только чтение\" из-за множественного доступа"); }
#endif
            var idx = _sparse[entity._id] - 1;
            if (idx >= 0) {
                _sparse[entity._id] = 0;
                _len--;

                if (_autoResetHandler != null) {
                    _autoResetHandler.Invoke (ref _data[idx]);
                } else {
                    _data[idx] = default;
                }

                if (idx < _len) {
                    _dense[idx] = _dense[_len];
#if LEOECSPROTO_SMALL_WORLD
                    _sparse[_dense[idx]._id] = (ushort) (idx + 1);
#else
                    _sparse[_dense[idx]._id] = idx + 1;
#endif
                    (_data[idx], _data[_len]) = (_data[_len], _data[idx]);
                }

                _world.UnsetEntityMaskBit (entity, _id);
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public void Copy (ProtoEntity srcEntity, ProtoEntity dstEntity) {
#if DEBUG
            if (_world.EntityGens ().Get (srcEntity._id) < 0) { throw new Exception ("не могу получить доступ к удаленной исходной сущности"); }
            if (_world.EntityGens ().Get (dstEntity._id) < 0) { throw new Exception ("не могу получить доступ к удаленной целевой сущности"); }
            if (_blockers > 1) { throw new Exception ($"нельзя изменить пул компонентов \"{DebugHelpers.CleanTypeName (_itemType)}\", он находится в режиме \"только чтение\" из-за множественного доступа"); }
#endif
            if (Has (srcEntity)) {
                ref var srcData = ref Get (srcEntity);
                if (!Has (dstEntity)) {
                    Add (dstEntity);
                }
                ref var dstData = ref Get (dstEntity);
                if (_autoCopyHandler != null) {
                    _autoCopyHandler.Invoke (ref srcData, ref dstData);
                } else {
                    dstData = srcData;
                }
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Serialize (ProtoEntity entity, Stream writer) {
#if DEBUG
            if (_world.EntityGens ().Get (entity._id) < 0) { throw new Exception ("не могу получить доступ к удаленной исходной сущности"); }
            if (writer == null) { throw new Exception ("поток записи не инициализирован"); }
#endif
            if (_autoSerializeHandler == null || !Has (entity)) {
                return false;
            }
            _autoSerializeHandler.Invoke (ref Get (entity), writer);
            return true;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Deserialize (ProtoEntity entity, Stream reader) {
#if DEBUG
            if (_world.EntityGens ().Get (entity._id) < 0) { throw new Exception ("не могу получить доступ к удаленной исходной сущности"); }
            if (reader == null) { throw new Exception ("поток чтения не инициализирован"); }
#endif
            if (_autoDeserializeHandler == null || !Has (entity)) {
                return false;
            }
            _autoDeserializeHandler.Invoke (ref Get (entity), reader);
            return true;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public int Len () => _len;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public ProtoEntity[] Entities () => _dense;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public T[] Data () => _data;

        void IProtoPool.Resize (int cap) => Array.Resize (ref _sparse, cap);

        void IProtoPool.AddRaw (ProtoEntity entity) => Add (entity);

        object IProtoPool.Raw (ProtoEntity entity) => Get (entity);

        void IProtoPool.SetRaw (ProtoEntity entity, object dataRaw) {
#if DEBUG
            if (dataRaw != null && dataRaw.GetType () != _itemType) { throw new Exception ($"неправильный тип данных для использования в качестве компонента \"{DebugHelpers.CleanTypeName (_itemType)}\""); }
#endif
            Get (entity) = dataRaw != null ? (T) dataRaw : default;
        }

        void IProtoPool.AddBlocker (int amount) {
#if DEBUG
            _blockers += amount;
            if (_blockers < 0) { throw new Exception ("ошибочный баланс пользователей пула при попытке освобождения"); }
#endif
        }

        delegate void AutoResetHandler (ref T component);
        delegate void AutoCopyHandler (ref T srcComponent, ref T dstComponent);
        delegate void AutoSerializeHandler (ref T component, Stream writer);
        delegate void AutoDeserializeHandler (ref T component, Stream reader);
    }
}
