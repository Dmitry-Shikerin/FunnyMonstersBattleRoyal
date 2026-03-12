// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.Runtime.CompilerServices;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.EcsProto.QoL {
    public struct ProtoPackedEntity : IEquatable<ProtoPackedEntity> {
        public short Gen;
        public ProtoEntity Id;

        public override int GetHashCode () {
            unchecked {
                return (23 * 31 + Id.GetHashCode ()) * 31 + Gen;
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Equals (ProtoPackedEntity rhs) => Id.Equals (rhs.Id) && Gen == rhs.Gen;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool operator == (ProtoPackedEntity lhs, ProtoPackedEntity rhs) {
            return lhs.Id.Equals (rhs.Id) && lhs.Gen == rhs.Gen;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool operator != (ProtoPackedEntity lhs, ProtoPackedEntity rhs) {
            return !lhs.Id.Equals (rhs.Id) || lhs.Gen != rhs.Gen;
        }

        public override bool Equals (Object obj) {
            return obj is ProtoPackedEntity rhs && this == rhs;
        }
    }

    public struct ProtoPackedEntityWithWorld : IEquatable<ProtoPackedEntityWithWorld> {
        public short Gen;
        public ProtoEntity Id;
        public ProtoWorld World;

        public override int GetHashCode () {
            unchecked {
                return ((23 * 31 + Id.GetHashCode ()) * 31 + Gen) * 31 + (World?.GetHashCode () ?? 0);
            }
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool Equals (ProtoPackedEntityWithWorld rhs) => Id.Equals (rhs.Id) && Gen == rhs.Gen && World == rhs.World;

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool operator == (ProtoPackedEntityWithWorld lhs, ProtoPackedEntityWithWorld rhs) {
            return lhs.Id.Equals (rhs.Id) && lhs.Gen == rhs.Gen && lhs.World == rhs.World;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool operator != (ProtoPackedEntityWithWorld lhs, ProtoPackedEntityWithWorld rhs) {
            return !lhs.Id.Equals (rhs.Id) || lhs.Gen != rhs.Gen || lhs.World != rhs.World;
        }

        public override bool Equals (Object obj) {
            return obj is ProtoPackedEntityWithWorld rhs && this == rhs;
        }
    }

#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class ProtoEntityExtensions {
        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static ProtoPackedEntity PackEntity (this ProtoWorld world, ProtoEntity entity) {
            ProtoPackedEntity packed;
            packed.Id = entity;
            packed.Gen = world.EntityGen (entity);
            return packed;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static ProtoPackedEntityWithWorld PackEntityWithWorld (this ProtoWorld world, ProtoEntity entity) {
            ProtoPackedEntityWithWorld packed;
            packed.Id = entity;
            packed.Gen = world.EntityGen (entity);
            packed.World = world;
            return packed;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool Unpack (this in ProtoPackedEntity packed, ProtoWorld world, out ProtoEntity entity) {
            entity = packed.Id;
            return world != null && world.IsAlive () && world.EntityGen (packed.Id) == packed.Gen;
        }

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public static bool Unpack (this in ProtoPackedEntityWithWorld packed, out ProtoWorld world, out ProtoEntity entity) {
            entity = packed.Id;
            world = packed.World;
            return world != null && world.IsAlive () && world.EntityGen (packed.Id) == packed.Gen;
        }
    }
}
