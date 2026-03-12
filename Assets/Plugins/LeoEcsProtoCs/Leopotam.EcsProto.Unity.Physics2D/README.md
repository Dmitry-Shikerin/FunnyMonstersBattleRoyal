<p align="center">
    <img src="./logo.png" alt="Proto">
</p>

# LeoECS Proto - интеграция событий Unity Physics2D
Интеграция событий Unity Physics2D для LeoECS Proto.

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


# UnityPhysics2D модуль
Модуль выполняет очистку полученных событий с unity-физики и их регистрацию в качестве компонентов,
состоит из 2 частей - аспекта и логики.


## Подключение аспекта в ручном режиме
Для подключения аспекта physics2d интеграции в ручном режиме достаточно добавить его в главный аспект мира:
```c#
class Aspect1 : ProtoAspectInject {
    public readonly UnityPhysics2DAspect Physics2D;
    // Регистрация прочих аспектов.
}
```


## Подключение логики в ручном режиме
```c#
// Инициализация окружения.
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Physics2D;

IProtoSystems _systems;

void Start () {        
    _systems = new ProtoSystems (new ProtoWorld (new Aspect1 ()));
    _systems
        .AutoInject ()
        // Подключение модуля интеграции unity.
        .AddModule (new UnityModule ())
        .Add (new TestSystem1 ())
        // Подключение модуля интеграции physics2d.
        .AddModule (new UnityPhysics2DModule ())
        .Init ();
}
```

Модуль physics2d подключается в том месте, в котором будет производиться очистка отработавших событий физики.
Для того, чтобы разделить место регистрации и место удаления событий, можно воспользоваться
именованной точкой и указать ее в конструкторе:
```c#
// Инициализация окружения.
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Physics2D;

IProtoSystems _systems;

void Start () {        
    _systems = new ProtoSystems (new ProtoWorld (new Aspect1 ()));
    _systems
        .AutoInject ()
        // Подключение модуля интеграции unity.
        .AddModule (new UnityModule ())
        // Подключение модуля интеграции physics2d для мира по умолчанию
        // с очисткой в точке с именем "physics3d.clear-point".
        .AddModule (new UnityPhysics2DModule (default, "physics2d.clear-point"))
        .Add (new TestSystem1 ())
        // События physics2d будут удаляться тут.
        .AddPoint ("physics2d.clear-point")
        .Init ();
}
```


## Подключение модуля целиком в автоматическом режиме
```c#
// Инициализация окружения.
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Physics2D;

IProtoSystems _systems;

void Start () {
    ProtoModules modules = new ProtoModules (
        new AutoInjectModule (),
        new UnityModule (),
        new UnityPhysics2DModule (default, "physics2d.clear-point")
    );
    _systems = new ProtoSystems (new ProtoWorld (modules.BuildAspect ()));
    _systems
        // Подключение композитного модуля.
        .AddModule (modules.BuildModule ())
        .Add (new TestSystem1 ())
        .AddPoint ("physics2d.clear-point")
        .Init ();
}
```


> **ВАЖНО!** Следует внимательно следить за тем, в какую группу систем подключается модуль.
> В подавляющем большинстве случаев это должна быть группа систем, выполняющаяся в FixedUpdate().

Модуль physics2d подключается в том месте, в котором будет производиться очистка отработавших ECS-событий.
Для того, чтобы разделить место регистрации и место удаления отработавших событий, можно воспользоваться
именованной точкой и указать ее в конструкторе `UnityPhysics2DModule`:
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
        // Подключение модуля интеграции physics2d для мира по умолчанию
        // с очисткой в точке с именем "physics2d.clear-point".
        .AddModule (new UnityPhysics2DModule (default, "physics2d.clear-point"))
        .Add (new TestSystem1 ())
        // События physics2d будут удаляться тут.
        .AddPoint ("physics2d.clear-point")
        .Init ();
}
```


# События
Все поддерживаемые типы событий описаны в подключаемом physics2d-аспекте:
```c#
public sealed class UnityPhysics2DAspect : ProtoAspectInject {
    public readonly ProtoPool<UnityPhysics2DCollisionEnterEvent> CollisionEnterEvent;
    public readonly ProtoPool<UnityPhysics2DCollisionExitEvent> CollisionExitEvent;
    public readonly ProtoPool<UnityPhysics2DTriggerEnterEvent> TriggerEnterEvent;
    public readonly ProtoPool<UnityPhysics2DTriggerExitEvent> TriggerExitEvent;
}
```
По любому из них можно собрать итератор для обработки реакции на действия пользователя:
```c#
using Leopotam.EcsProto.Unity.Physics2D;

public class TestTriggerEnterEventSystem : IProtoRunSystem {
    [DI] UnityPhysics2DAspect _phys2DEvents;
    [DI] ProtoIt _triggerEnterIt = new (It.Inc<UnityPhysics2DTriggerEnterEvent> ());
    
    public void Run () {
        foreach (ProtoEntity entity in _triggerEnterIt) {
            ref UnityPhysics2DTriggerEnterEvent data = ref _phys2DEvents.TriggerEnterEvent.Get (entity);
            Debug.Log ("enter to trigger!", data.Sender);
        }
    }
}
```


# Действия
Действия (классы `xxxAction`) - это `MonoBehaviour`-компоненты, которые слушают события unity-физики и генерируют соответствующие ECS-события:
* UnityPhysics2DCollisionEnterAction
* UnityPhysics2DCollisionExitAction
* UnityPhysics2DControllerColliderHitAction
* UnityPhysics2DTriggerEnterAction
* UnityPhysics2DTriggerExitAction


# Лицензия
Расширение выпускается под коммерческой лицензией, [подробности тут](./LICENSE.md).
