<p align="center">
    <img src="./logo.png" alt="Proto">
</p>

# LeoECS Proto - интеграция событий Unity uGui
Интеграция событий Unity uGui для LeoECS Proto.

> **ВАЖНО!** Требует C#9 (или Unity >=2021.2).

> **ВАЖНО!** Зависит от: **Leopotam.EcsProto**, **Leopotam.EcsProto.QoL**, **Leopotam.EcsProto.Unity**, **Unity.TextMeshPro**.

> **ВАЖНО!** Не забывайте использовать `DEBUG`-версии билдов для разработки и `RELEASE`-версии билдов для релизов: все внутренние проверки/исключения будут работать только в `DEBUG`-версиях и удалены для увеличения производительности в `RELEASE`-версиях.

> **ВАЖНО!** Проверено на Unity 2021.3 (зависит от нее) и содержит asmdef-описания для компиляции в виде отдельных сборок и уменьшения времени рекомпиляции основного проекта.


# Социальные ресурсы
[Официальный блог](https://leopotam.com)


# Установка


## В виде исходников
Поддерживается установка в виде исходников из архива, который надо распаковать в проект.


## Прочие источники
Официальные версии выпускаются для активных подписчиков в виде ссылок на актуальные версии.


# UnityUgui модуль
Модуль выполняет очистку полученных событий с uGui и их регистрацию в качестве компонентов,
состоит из 2 частей - аспекта и логики.


## Подключение аспекта в ручном режиме
Для подключения аспекта достаточно добавить тип `UnityUguiAspect` в главный аспект мира:
```c#
class Aspect1 : ProtoAspectInject {
    public readonly UnityUguiAspect Ugui;
    // Регистрация прочих аспектов.
}
```


## Подключение логики в ручном режиме
```c#
// Инициализация окружения.
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
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
        // Подключение модуля интеграции ugui.
        .AddModule (new UnityUguiModule ())
        .Init ();
}
```

Модуль ugui подключается в том месте, в котором будет производиться очистка отработавших ugui-событий.
Для того, чтобы разделить место регистрации и место удаления отработавших событий, можно воспользоваться
именованной точкой и указать ее в конструкторе `UnityUguiModule`:
```c#
// Инициализация окружения.
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Ugui;

IProtoSystems _systems;

void Start () {        
    _systems = new ProtoSystems (new ProtoWorld (new Aspect1 ()));
    _systems
        .AutoInject ()
        // Подключение модуля интеграции unity.
        .AddModule (new UnityModule ())
        // Подключение модуля интеграции ugui для мира по умолчанию
        // с очисткой в точке с именем "ugui.clear-point".
        .AddModule (new UnityUguiModule (default, "ugui.clear-point"))
        .Add (new TestSystem1 ())
        // События ugui будут удаляться тут.
        .AddPoint ("ugui.clear-point")
        .Init ();
}
```

## Подключение модуля целиком в автоматическом режиме
```c#
// Инициализация окружения.
using Leopotam.EcsProto;
using Leopotam.EcsProto.QoL;
using Leopotam.EcsProto.Unity;
using Leopotam.EcsProto.Unity.Ugui;

IProtoSystems _systems;

void Start () {
    ProtoModules modules = new ProtoModules (
        new AutoInjectModule (),
        new UnityModule (),
        new UnityUguiModule (default, "ugui.clear-point")
    );
    _systems = new ProtoSystems (new ProtoWorld (modules.BuildAspect ()));
    _systems
        // Подключение композитного модуля.
        .AddModule (modules.BuildModule ())
        .Add (new TestSystem1 ())
        // События ugui будут удаляться тут.
        .AddPoint ("ugui.clear-point")
        .Init ();
}
```


# События
Все поддерживаемые типы событий описаны в подключаемом ugui-аспекте:
```c#
public sealed class UnityUguiAspect : ProtoAspectInject {
    public readonly ProtoPool<UnityUguiClickEvent> ClickEvent;
    public readonly ProtoPool<UnityUguiDownEvent> DownEvent;
    public readonly ProtoPool<UnityUguiDragEndEvent> DragEndEvent;
    public readonly ProtoPool<UnityUguiDragMoveEvent> DragMoveEvent;
    public readonly ProtoPool<UnityUguiDragStartEvent> DragStartEvent;
    public readonly ProtoPool<UnityUguiDropEvent> DropEvent;
    public readonly ProtoPool<UnityUguiEnterEvent> EnterEvent;
    public readonly ProtoPool<UnityUguiExitEvent> ExitEvent;
    public readonly ProtoPool<UnityUguiScrollViewEvent> ScrollViewEvent;
    public readonly ProtoPool<UnityUguiSliderChangeEvent> SliderChangeEvent;
    public readonly ProtoPool<UnityUguiDropdownChangeEvent> DropdownChangeEvent;
    public readonly ProtoPool<UnityUguiInputChangeEvent> InputChangeEvent;
    public readonly ProtoPool<UnityUguiInputEndEvent> InputEndEvent;
    public readonly ProtoPool<UnityUguiUpEvent> UpEvent;
}
```
По любому из них можно собрать итератор для обработки реакции на действия пользователя:
```c#
using Leopotam.EcsProto.Unity.Ugui;

public class TestUguiClickEventSystem : IProtoRunSystem {
    [DI] UnityUguiAspect _uguiEvents;
    [DI] ProtoIt _clickIt = new (It.Inc<UnityUguiClickEvent> ());
    
    public void Run () {
        foreach (ProtoEntity entity in _clickIt) {
            ref UnityUguiClickEvent data = ref _uguiEvents.ClickEvent.Get (entity);
            Debug.Log ("Im clicked!", data.Sender);
        }
    }
}
```


# Действия
Действия (классы `xxxAction`) - это `MonoBehaviour`-компоненты, которые слушают события uGui-виджетов и генерируют соответствующие ECS-события:
* UnityUguiClickAction
* UnityUguiDownAction
* UnityUguiDragEndAction
* UnityUguiDragMoveAction
* UnityUguiDragStartAction
* UnityUguiDropAction
* UnityUguiDropdownAction
* UnityUguiEnterAction
* UnityUguiExitAction
* UnityUguiInputChangeAction
* UnityUguiInputEndAction
* UnityUguiScrollViewAction
* UnityUguiSliderAction
* UnityUguiUpAction


# Лицензия
Расширение выпускается под коммерческой лицензией, [подробности тут](./LICENSE.md).
