<p align="center">
    <img src="./logo.png" alt="Proto">
</p>

# LeoECS Proto QoL
Набор расширений для `LeoECS Proto`, призванных улучшить "качество жизни" (Quality of Life) разработчика.

> **ВАЖНО!** Требует C#9 (или Unity >=2021.2).

> **ВАЖНО!** Зависит от: **Leopotam.EcsProto**.

> **ВАЖНО!** Не забывайте использовать `DEBUG`-версии билдов для разработки и `RELEASE`-версии билдов для релизов: все внутренние проверки/исключения будут работать только в `DEBUG`-версиях и удалены для увеличения производительности в `RELEASE`-версиях.

> **ВАЖНО!** Проверено на Unity 2021.3 (не зависит от нее) и содержит asmdef-описания для компиляции в виде отдельных сборок и уменьшения времени рекомпиляции основного проекта.


# Социальные ресурсы
[Официальный блог](https://leopotam.com)


# Установка


## В виде исходников
Поддерживается установка в виде исходников из архива, который надо распаковать в проект.


## Прочие источники
Официальные версии выпускаются для активных подписчиков в виде ссылок на актуальные версии.


# Итераторы


## Инициализация
Итераторы получили возможность инициализации 3 способами (итератор по сущностям с компонентами C1 и C2, но без C3):
```c#
ProtoItExc it1 = new (It.List (new C1 (), new C2 ()), It.List (new C3 ()));
ProtoItExc it2 = new (It.Inc<C1, C2> (), It.Exc<C3> ());
ProtoItExc it3 = It.Chain<C1> ().Inc<C2> ().Exc<C3> ().End ();
```
Если нужен итератор без исключений компонентов, для этого существует отдельный тип:
```c#
ProtoIt it1 = new (It.List (new C1 (), new C2 ()));
ProtoIt it2 = new (It.Inc<C1, C2> ());
ProtoIt it3 = It.Chain<C1> ().Inc<C2> ().End ();
```
> **ВАЖНО!** Рекомендованный способ - первый, с использованием `It.List()`. Варианты с обобщениями более лаконичны, но на большом количестве компонентов и их комбинаций сильно увеличивают размер исполняемого файла.


## Поддержка foreach-циклов
Итераторы получили возможность использования в foreach-циклах:
```c#
ProtoIt it1 = new (It.Inc<C1> ()).Init (world);
ProtoItExc it2 = new (It.Inc<C1> (), It.Exc<C2> ()).Init (world);

foreach (ProtoEntity entity in it1) {
    ref C1 c1 = ref aspect1.C1Pool.Get (entity);
}
foreach (ProtoEntity entity in it2) {
    ref C1 c1 = ref aspect1.C1Pool.Get (entity);
}
```
> **ВАЖНО!** Принудительно вызывать `it.End()` при досрочном прерывании цикла не нужно.


## Подсчет сущностей в итераторе
> **ВАЖНО!** Не рекомендуется к использованию если сущностей может быть больше
> пары десятков - подсчет ведется полным перебором.

```c#
ProtoIt it = new (It.Inc<C1> ()).Init (world);

int entitiesCount = it.Len();
```

Если точное количество не требуется, а достаточно просто знать, что итератор не пустой, то можно
воспользоваться следующим методом:
```c#
ProtoIt it = new (It.Inc<C1> ()).Init (world);

bool isEmpty = it.IsEmpty();
```
> **ВАЖНО!** Этот метод быстрее `IProtoIt.Len()`, но в худшем случае все-равно выполняется полный перебор.


## Получение первой сущности в итераторе
Если требуется получить только первую сущность из итератора с корректной обработкой ее отсутствия, то можно воспользоваться следующим методом:
```c#
(ProtoEntity entity, bool ok) = it.First ();
if (ok) {
    // Проверяем успешность операции,
    // сущность валидна и ее можно использовать.
}
```


## Итераторы с кешированием
Если точно известно, что выборка данных не поменяется и требуется обработать ее несколько раз, то можно воспользоваться кеширующими итераторами:
```c#
// Создаем пул обычным способом, отличается только тип.
ProtoItCached it1 = new (It.Inc<C1> ()).Init (world);
// Кешируем данные, пулы переводятся в ReadOnly.
it1.BeginCaching ();
foreach (ProtoEntity entity in it1) {
    ref C1 c1 = ref aspect1.C1Pool.Get (entity);
}
// Несколько проходов по итератору it1.
// ...
// Отключаем кеширование, пулы переводятся в обычный режим.
it1.EndCaching ();

// Точно так же создаем пул с Exclude обычным способом, отличается только тип.
ProtoItExcCached it2 = new (It.Inc<C1> (), It.Exc<C2> ()).Init (world);
// Кешируем данные, пулы переводятся в ReadOnly.
it2.BeginCaching ();
foreach (ProtoEntity entity in it2) {
    ref C1 c1 = ref aspect1.C1Pool.Get (entity);
}
// Несколько проходов по итератору it2.
// ...
// Отключаем кеширование, пулы переводятся в обычный режим.
it2.EndCaching ();
```

> **ВАЖНО!** Если не вызвать `it.BeginCaching()`, то итератор будет работать в режиме обычного итератора (медленнее обычного из-за дополнительных проверок).

> **ВАЖНО!** Если был вызван `it.BeginCaching()`, в пару ему обязательно вызывать `it.EndCaching()` после завершения обработки, иначе зависимые пулы останутся в режиме ReadOnly.

> **ВАЖНО!** Если не требуется обработка в несколько проходов (например, только 1 итерация), то вариант с кешированием будет работать медленнее.

> **ВАЖНО!** Если требуется проверить режим работы итератора (кешируется или нет), то для этого существует метод `it.IsCached()`.

Если есть необходимость получить закешированные сущности в виде непрерывного массива, то можно воспользоваться следующим способом:
```c#
ProtoItCached it3 = new (It.Inc<C1> ()).Init (world);
it3.BeginCaching ();
(ProtoEntity[] entities, int len) = it3.CachedData();
// Кеш сущностей заполнен от 0 до len, если режим
// кеширования не был активирован, то len будет 0.
// ...
// Выполняем обработку полученных данных.
// ...
it3.EndCaching ();
```

Кеширующие итераторы поддерживают сортировку сущностей на основе данных их компонентов:
```c#
struct C2 {
    public int Id;
}

ProtoItCached it4 = new (It.Inc<C2> ()).Init (world);
// Кешируем данные, пулы переводятся в ReadOnly.
it4.BeginCaching ();
// Выполняем сортировку сущностей по возрастанию значений
// в C2.Id (использовать можно любые поля, Id тут для примера):
it4.Sort((ProtoEntity a, ProtoEntity b) => {
    return aspect1.C2Pool.Get (a).Id - aspect1.C2Pool.Get (b).Id;
});
// Работаем с отсортированными данными.
foreach (ProtoEntity entity in it4) {
    ref C2 c2 = ref aspect1.C2Pool.Get (entity);
}
// Отключаем кеширование, пулы переводятся в обычный режим.
it4.EndCaching ();
```
> **ВАЖНО!** Для уменьшения аллокаций для обработчиков сортировки вместо лямбд рекомендуется использовать локальные функции или методы систем.


# Сущности


## Упаковка сущностей
Сущности отдаются в пользовательский код в виде `ProtoEntity`-идентификаторов и валидны только в пределах
текущего метода - **нельзя** хранить ссылки на сущности если нет уверенности, что они не могут быть
уничтожены где-то в коде.
Если требуется сохранять сущности, то их следует упаковать:
```c#
// Создаем новую сущность в мире.
ProtoEntity entity = world.NewEntity ();
// Упаковываем ее для долгосрочного хранения за пределами текущего метода.
ProtoPackedEntity packed = world.PackEntity (entity);
// Когда придет время - мы можем распаковать ее с одновременной проверкой на ее существование.
if (packed.Unpack (world, out ProtoEntity unpackedEntity)) {
    // Если условие истинно - сущность валидна, можем с ней работать.
    world.DelEntity (unpackedEntity);
}
```
Если используется несколько миров и важно сохранять привязку к ним, то можно упаковывать другим способом:
```c#
// Создаем новую сущность в мире.
ProtoEntity entity = world.NewEntity ();
// Упаковываем ее для долгосрочного хранения за пределами текущего метода.
ProtoPackedEntityWithWorld packed = world.PackEntityWithWorld (entity);
// Когда придет время - мы можем распаковать ее с одновременной проверкой на ее существование.
if (packed.Unpack (out ProtoWorld unpackedWorld, out ProtoEntity unpackedEntity)) {
    // Если условие истинно - сущность валидна, можем с ней работать.
    unpackedWorld.DelEntity (unpackedEntity);
}
```
Для сравнения двух упакованных сущностей следует применять оператор `==`:
```c#
ProtoPackedEntity packedA = world.PackEntity (entity);
ProtoPackedEntity packedB = world.PackEntity (entity);
if (packedA == packedB) {
    // Упакованные сущности идентичны.
}
```
То же самое касается и упаковки сущности с миром:
```c#
ProtoPackedEntityWithWorld packedA = world.PackEntityWithWorld (entity);
ProtoPackedEntityWithWorld packedB = world.PackEntityWithWorld (entity);
if (packedA == packedB) {
    // Упакованные сущности идентичны.
}
```

# Миры


## Инъекции полей в аспекте
Для сокращения количества кода инициализации аспекта мира можно использовать наследование от специального типа - поддерживается инъекция полей, реализующих `IProtoAspect`, `IProtoPool` и `IProtoIt`:
```c#
class Aspect1 : ProtoAspectInject {
    public Aspect2 Aspect2;
    public ProtoPool<C1> C1Pool;
    public ProtoPool<C2> C2Pool;
    public ProtoIt ItC1 = new (It.Inc<C1> ());
    public ProtoItExc ItInc1Exc2 = new (It.Inc<C1> (), It.Exc<C2> ());
}
```
Это идентично следующему коду:
```c#
class Aspect1 : IProtoAspect {
    public Aspect2 Aspect2;
    public ProtoPool<C1> C1Pool;
    public ProtoPool<C2> C2Pool;
    public ProtoIt ItC1 = new (It.Inc<C1> ());
    public ProtoItExc ItInc1Exc2 = new (It.Inc<C1> (), It.Exc<C2> ());
    ProtoWorld _world;

    public void Init (ProtoWorld world) {
        _world = world;
        world.AddAspect (this);
        Aspect2 ??= new ();
        Aspect2.Init (world);
        if (world.HasPool(typeof (C1))) {
            C1Pool = (ProtoPool<C1>) world.Pool (typeof (C1));
        } else {
            C1Pool = new ();
            world.AddPool (C1Pool);
        }
        if (world.HasPool(typeof (C2))) {
            C2Pool = (ProtoPool<C2>) world.Pool (typeof (C2));
        } else {
            C2Pool = new ();
            world.AddPool (C2Pool);
        }
    }
    public void PostInit () {
        ItC1.Init (_world);
        ItInc1Exc2.Init (_world);
    }
}
```
Если нужна дополнительная кастомная инициализация, то можно выполнить ее через перегрузки методов `Init()`/`PostInit()`:
```c#
class Aspect1 : ProtoAspectInject {
    public ProtoPool<C1> C1Pool;
    public ProtoPool<C2> C2Pool;
    // Поле итератора должно быть проинициализировано к моменту инъекции.
    public ProtoIt ItC1 = new (It.Inc<C1> ());
    public ProtoItExc ItInc1Exc2 = new (It.Inc<C1> (), It.Exc<C2> ());

    public override void Init (ProtoWorld world) {
        base.Init (world);
        // Дополнительная инициализация.
    }
    public override void PostInit () {
        base.PostInit ();
        // Дополнительная инициализация.
    }
}
```
Поля могут быть проинициализированы экземплярами данных до начала инъекции - в этом случае они будут
использоваться для дальнейшей настройки, это один из способов вызова кастомных конструкторов пулов и аспектов.


Для получения ссылки на мир аспекта можно воспользоваться специальным методом:
```c#
ProtoWorld world = new (new Aspect1 ());
Aspect1 aspect = (Aspect1) world.Aspect (typeof (Aspect1));
ProtoWorld aspectWorld = aspect.World ();
// aspectWorld и world содержат ссылку на один и тот же экземпляр.
```


## Список активных сущностей
```c#
Slice<ProtoEntity> items = new ();
world.GetAliveEntities (items);
for (int i = 0; i < items.Len (); i++) {
    ProtoEntity entity = items.Get (i);
}
```
Если требуется узнать только количество активных сущностей (быстрее):
```c#
int count = world.GetAliveEntitiesCount ();
```


## Список компонентов на сущности
```c#
Slice<object> items = new ();
world.GetComponents(entity, items);
for (int i = 0; i < items.Len (); i++) {
    object c = items.Get (i);
}
```


# Системы


## Инъекции полей в системы
Для инъекции в поля систем их достаточно пометить атрибутом `[DI]`:
```c#
class TestSystem : IProtoInitSystem {
    // Поле будет проинициализировано экземпляром аспекта с типом Aspect1.
    [DI] Aspect1 _aspectDef;
    // Поле будет проинициализировано экземпляром аспекта с типом Aspect1 из мира "events".
    [DI ("events")] Aspect1 _aspectEvt;
    // Поле будет проинициализировано экземпляром итератора
    // для компонентов из мира по умолчанию.
    [DI] ProtoIt _itDef = new (It.Inc<C1> ());
    // Поле будет проинициализировано экземпляром итератора
    // для компонентов из мира "events".
    [DI ("events")] ProtoIt _itEvt = new (It.Inc<C1> ());
    // Поле будет проинициализировано экземпляром сервиса с типом Service.
    [DI] Service1 _svc;

    public void Init (IProtoSystems systems) {
        // Все поля проинициализированы, можно работать с ними.
    }
}
```

> **ВАЖНО!** Инъекция итератора подразумевает, что его экземпляр уже создан через инициализатор поля.

> **ВАЖНО!** Так же поддерживается инъекция `ProtoWorld`-типа через атрибут `[DI]`, но для оптимизации
> рекомендуется использовать вызов `ProtoAspectInject.World()`. Прямая инъекция может быть полезна для
> инициализации полей сервисов.

Для корректной работы инъекции в поля систем необходимо подключить специальный модуль:
```c#
ProtoWorld world1 = new (new Aspect1 ());
ProtoWorld world2 = new (new Aspect2 ());
ProtoSystems systems = new (world1);
systems
    // Модуль инъекции.
    .AddModule (new AutoInjectModule ())

    .AddWorld (world2, "events")
    .AddSystem (new TestSystem ())
    .AddService (new Service1 ())
    .Init ();
```

> **ВАЖНО!** Модуль инъекций должен идти первым, до регистрации остальных модулей и систем.
> Подключать модуль нужно только один раз для каждой `IProtoSystems`-группы систем.

Технически, можно выполнять инъекции в поля не только систем, но и любых объектов через вызов метода `AutoInjectModule.Inject()`:
```c#
class MyObject {
    [DI] readonly MyAspect _myAspect = default;
    [DI] readonly ProtoIt _myIt = new (It.Inc<C1> ());
}
Dictionary<Type, object> services = systems.Services ();
MyObject obj = new ();
AutoInjectModule.Inject (obj, systems, services);
// Поля экземпляра объекта будут проинициализированы
// и готовы к использованию здесь.
```

Можно выполнять инъекцию в подключенные сервисы автоматически, указав опциональный флаг в конструкторе `AutoInjectModule`:
```c#
systems
    .AddModule (new AutoInjectModule (true))
    .Init ();
```

## Удаление всех компонентов нужного типа
Можно автоматизировать удаление компонентов определенного типа в нужном месте:
```c#
ProtoWorld world1 = new (new Aspect1 ());
ProtoSystems systems = new (world1);
systems
    .AddSystem (new TestSystem1 ())
    // Все компоненты c типом C1 будут удалены тут
    // со всех активных сущностей.
    .DelHere<C1> ()
    .AddSystem (new TestSystem2 ())
    .Init ();
```
`DelHere()` поддерживает указание мира для удаляемых компонентов и именованную точку вызова через явное указание дополнительных параметров.

Если этот метод-расширение не устраивает и нужно получить поведение в виде системы - можно воспользоваться ручным созданием экземпляра системы `DelHereSystem` - расширение `DelHere()` является оберткой над ней.


## Дополнительная инициализация сервисов
Можно автоматизировать дополнительную инициализацию сервисов нужном месте без создания дополнительных систем:
```c#
ProtoWorld world1 = new (new Aspect1 ());
ProtoSystems systems = new (world1);
systems
    .AddSystem (new TestSystem1 ())
    // Сервис Service1 будет дополнительно
    // инициализирован здесь.
    .InitHere<Service1> ()
    .AddSystem (new TestSystem2 ())
    // Инициализируемый сервис должен быть
    // зарегистрирован в списке сервисов.
    .AddService (new Service1 ())
    .Init ();
```
Чтобы сервис поддерживал дополнительную инициализацию, он должен реализовывать специальный интерфейс:
```c#
class Service1 : IProtoInitService {
    public void Init (IProtoSystems systems) {
        // Дополнительная инициализация.
    }
}
```
`InitHere()` поддерживает указание именованной точки вызова через явное указание дополнительного параметра.

Если этот метод-расширение не устраивает и нужно получить поведение в виде системы - можно воспользоваться ручным созданием экземпляра системы `InitHereSystem` - расширение `InitHere()` является оберткой над ней.


# Пулы


## Запрос или добавление
Для того, чтобы запросить существующий компонент или добавить новый в случае его отсутствия, можно воспользоваться следующим методом:
```c#
// Если нужно знать о существовании компонента до вызова.
ref C1 c1 = ref aspect1.C1Pool.GetOrAdd (e, out bool added);
// Если не нужно знать о существовании компонента до вызова.
ref C1 c1 = ref aspect1.C1Pool.GetOrAdd (e);
```


# Модули


## Инициализация
По умолчанию аспекты модуля регистрируются в мире отдельно. Для упрощения одновременной регистрации аспектов, систем и сервисов модуля можно использовать специальный класс:
```c#
// Все модули можно передать в конструктор класса.
ProtoModules modules = new (
    new Module1 (),
    new Module2 (),
    new Module3 ());
// Или через отдельный метод.
modules.AddModule (new Module4 ());
// Также возможно подключать отдельные аспекты вне модулей.
modules.AddAspect (new Aspect1 ());
// В конструктор мира передаем композитный аспект, в который автоматически
// включены аспекты всех подключенных модулей.
ProtoWorld world = new (modules.BuildAspect ())
ProtoSystems systems = new (world);
systems
    // Выполняем подключение композитный модуля, который автоматически
    // выполнит регистрацию всех подключенных в него модулей.
    .AddModule (modules.BuildModule ())
    .Init ();
```
> **ВАЖНО!** Если используется `ProtoModules` - регистрация отдельных модулей
> через `IProtoSystems.AddModule()` не рекомендуется, все модули должны проходить через `ProtoModules` единообразно.

# Утилиты


## Сервис-локатор
Экземпляра класса можно сохранить глобально:
```c#
UserSession sess = new ();
Service<UserSession>.Set (sess);
```

Экземпляра сохраненного глобально класса можно получить в любом месте:
```c#
UserSession sess = Service<UserSession>.Get ();
```

> **ВАЖНО!** Если данные больше не нужны - следует сбросить ссылку на них путем передачи `null` в метод Set().


# Лицензия
Расширение выпускается под коммерческой лицензией, [подробности тут](./LICENSE.md).
