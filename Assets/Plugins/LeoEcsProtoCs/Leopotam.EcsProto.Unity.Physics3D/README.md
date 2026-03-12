<p align="center">
    <img src="./logo.png" alt="Proto">
</p>

# LeoECS Proto - интеграция событий Unity Physics3D
Интеграция событий Unity Physics3D для LeoECS Proto.

> **ВАЖНО!** Требует C#9 (или Unity >=2021.2).

> **ВАЖНО!** Зависит от: **Leopotam.EcsProto**, **Leopotam.EcsProto.QoL**, **Leopotam.EcsProto.Unity**.

> **ВАЖНО!** Не забывайте использовать `DEBUG`-версии билдов для разработки и `RELEASE`-версии билдов для релизов: все внутренние проверки/исключения будут работать только в `DEBUG`-версиях и удалены для увеличения производительности в `RELEASE`-версиях.

> **ВАЖНО!** Проверено на Unity 2021.3 (зависит от нее) и содержит asmdef-описания для компиляции в виде отдельных сборок и уменьшения времени рекомпиляции основного проекта.


# Социальные ресурсы
[Официальный блог](https://leopotam.com)


# Установка


## В виде исходников
Поддерживается установка в виде исходников из архива, который надо распаковать в проект.


## Прочие источники
Официальные версии выпускаются для активных подписчиков в виде ссылок на актуальные версии.


# UnityPhysics3D модуль
Модуль выполняет очистку полученных событий с unity-физики и их регистрацию в качестве компонентов,
состоит из 2 частей - аспекта и логики.


## Подключение аспекта в ручном режиме
Для подключения аспекта physics3d интеграции достаточно добавить его в главный аспект мира:
```c#
class Aspect1 : ProtoAspectInject {
    public readonly UnityPhysics3DAspect Physics3D;
    // Регистрация прочих аспектов.
}
```


## Подключение логики в ручном режиме
```c#
// Инициализация окружения.
using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Ugui;

IProtoSystems _systems;

void Start () {        
    _systems = new ProtoSystems (new ProtoWorld (new Aspect1 ()));
    _systems
        .AutoInject ()
        // Подключение модуля интеграции unity.
        .AddModule (new UnityModule ())
        .Add (new TestSystem1 ())
        // Подключение модуля интеграции physics3d.
        .AddModule (new UnityPhysics3DModule ())
        .Init ();
}
```

> **ВАЖНО!** Следует внимательно следить за тем, в какую группу систем подключается модуль.
> В подавляющем большинстве случаев это должна быть группа систем, выполняющаяся в FixedUpdate().

Модуль physics3d подключается в том месте, в котором будет производиться очистка отработавших событий физики.
Для того, чтобы разделить место регистрации и место удаления событий, можно воспользоваться
именованной точкой и указать ее в конструкторе:
```c#
// Инициализация окружения.
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Physics3D;

IProtoSystems _systems;

void Start () {        
    _systems = new ProtoSystems (new ProtoWorld (new Aspect1 ()));
    _systems
        .AutoInject ()
        // Подключение модуля интеграции unity.
        .AddModule (new UnityModule ())
        // Подключение модуля интеграции physics3d для мира по умолчанию
        // с очисткой в точке с именем "physics3d.clear-point".
        .AddModule (new UnityPhysics3DModule (default, "physics3d.clear-point"))
        .Add (new TestSystem1 ())
        // События physics3d будут удаляться тут.
        .AddPoint ("physics3d.clear-point")
        .Init ();
}
```


## Подключение модуля целиком в автоматическом режиме
```c#
// Инициализация окружения.
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Physics3D;

IProtoSystems _systems;

void Start () {
    ProtoModules modules = new ProtoModules (
        new AutoInjectModule (),
        new UnityModule (),
        new UnityPhysics3DModule (default, "physics3d.clear-point")
    );
    _systems = new ProtoSystems (new ProtoWorld (modules.BuildAspect ()));
    _systems
        // Подключение композитного модуля.
        .AddModule (modules.BuildModule ())
        .Add (new TestSystem1 ())
        // События physics3d будут удаляться тут.
        .AddPoint ("physics3d.clear-point")
        .Init ();
}
```

# События
Все поддерживаемые типы событий описаны в подключаемом physics3d-аспекте:
```c#
public sealed class UnityPhysics3DAspect : ProtoAspectInject {
    public readonly ProtoPool<UnityPhysics3DCollisionEnterEvent> CollisionEnterEvent;
    public readonly ProtoPool<UnityPhysics3DCollisionExitEvent> CollisionExitEvent;
    public readonly ProtoPool<UnityPhysics3DControllerColliderHitEvent> ControllerColliderHitEvent;
    public readonly ProtoPool<UnityPhysics3DTriggerEnterEvent> TriggerEnterEvent;
    public readonly ProtoPool<UnityPhysics3DTriggerExitEvent> TriggerExitEvent;
}
```
По любому из них можно собрать итератор для обработки реакции на действия пользователя:
```c#
using Leopotam.EcsProto.Unity.Physics3D;

public class TestTriggerEnterEventSystem : IProtoRunSystem {
    [DI] UnityPhysics3DAspect _phys3DEvents;
    [DI] ProtoIt _triggerEnterIt = new (It.Inc<UnityPhysics3DTriggerEnterEvent> ());
    
    public void Run () {
        foreach (ProtoEntity entity in _triggerEnterIt) {
            ref UnityPhysics3DTriggerEnterEvent data = ref _phys3DEvents.TriggerEnterEvent.Get (entity);
            Debug.Log ("enter to trigger!", data.Sender);
        }
    }
}
```


# Действия
Действия (классы `xxxAction`) - это `MonoBehaviour`-компоненты, которые слушают события unity-физики и генерируют соответствующие ECS-события:
* UnityPhysics3DCollisionEnterAction
* UnityPhysics3DCollisionExitAction
* UnityPhysics3DControllerColliderHitAction
* UnityPhysics3DTriggerEnterAction
* UnityPhysics3DTriggerExitAction


# Лицензия
Расширение выпускается под коммерческой лицензией, [подробности тут](./LICENSE.md).
