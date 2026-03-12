<p align="center">
    <img src="./logo.png" alt="Logo">
</p>

# UtilityAI для LeoECS Proto
Реализация UtilityAI для LeoECS Proto.

> **ВАЖНО!** Требует C#9 (или Unity >=2021.2).

> **ВАЖНО!** Зависит от: **Leopotam.EcsProto**, **Leopotam.EcsProto.QoL**.

> **ВАЖНО!** Не забывайте использовать `DEBUG`-версии билдов для разработки и `RELEASE`-версии билдов для релизов: все внутренние проверки/исключения будут работать только в `DEBUG`-версиях и удалены для увеличения производительности в `RELEASE`-версиях.

> **ВАЖНО!** Проверено на Unity 2021.3 (не зависит от нее) и содержит asmdef-описания для компиляции в виде отдельных сборок и уменьшения времени рекомпиляции основного проекта.


# Социальные ресурсы
[Официальный блог](https://leopotam.com)


# Установка


## В виде исходников
Поддерживается установка в виде исходников из архива, который надо распаковать в проект.


## Прочие источники
Официальные версии выпускаются для активных подписчиков в виде ссылок на актуальные версии.


# Основные типы


## Решение
Это набор проверок, определяющих важность данного решения в конкретный момент времени, а так же набор действий, которые будут выполнены при выборе этого решения.
Представлен пользовательским классом, реализующим интерфейс `IAiUtilitySolver`:
```c#
// Компонент с данными.
struct Unit {
    public int Hunger;
    public int Food;
}
// Аспект с пулами.
class UnitAspect : ProtoAspectInject {
    public readonly ProtoPool<Unit> Units = default;
}
// Пример решения.
class EatFoodSolver : IAiUtilitySolver {
    // Решение поддерживает автоматическую инъекцию как обычная ECS-система.
    [DI] readonly UnitAspect _unit = default;

    // Этот метод будет вызван для каждой сущности,
    // для которой был совершен запрос на обработку.
    public float Solve (ProtoEntity entity) {
        ref Unit unit = ref _unit.Units.Get (entity);
        // проверяем, что голодны и есть еда.
        if (unit.Hunger >= 100 && unit.Food > 0) {
            return 1f;
        }
        return 0f;
    }

    public void Apply (ProtoEntity entity) {
        // Употребляем еду.
        ref Unit unit = ref _unit.Units.Get (entity);
        unit.Food--;
        unit.Hunger = 0;
    }
}
```
Метод `Solve()` возвращает важность/ценность решения без нормализации, значение может быть любым, будет выбрано самое лучшее решение по этой оценке.

Метод `Apply()` будет вызван только для того решения, которое было признано лучшим в данный момент времени.

## Запрос
Это специальный скрытый компонент, добавляющийся на сущность, данные на которой требуется обработать. Для удобства вся работа скрыта за специальным методом в аспекте модуля:
```c#
[DI] readonly AiUtilityAspect _aiUtility = default;
// Запрос на обработку сущности, для которой будет
// вызван метод Resolve() у всех активных решений.
_aiUtility.Request(entity);
```
> **ВАЖНО!** Удалять компонент руками не нужно - он будет удален системами модуля автоматически при обработке.

## Ответ
Это специальный компонент, добавляющийся на сущность, ранее помеченную запросом на обработку:
```c#
[DI] readonly AiUtilityAspect _aiUtility = default;
[DI] readonly ProtoIt _responses = new (It.Inc<AiUtilityResponseEvent> ());
// Ответ с результатами обработки, представленными лучшей важностью/ценностью
// и решением, которое было признано лучшим.
foreach (ProtoEntity entity in _responses) {
    ref AiUtilityResponseEvent res = ref _aiUtility.ResponseEvent.Get (entity);
    // Применение решения.
    res.Solver.Apply (entity);
}
```
> **ВАЖНО!** Удалять компонент руками не нужно - он будет удален системами модуля автоматически при обработке.


# Подключение модуля
```c#
ProtoModules modules = new (
        // Модуль автоинъекции, необходим для работы модуля.
        new AutoInjectModule (),
        new AiUtilityModule (
            // Имя мира, в котором будет работать модуль.
            default,
            // Именованная точка для подключения. 
            "ai-utility"
            // Список активных решений, порядок не важен.
            new WaitSolver (),
            new SearchFoodSolver (),
            new EatFoodSolver ()
        ));
ProtoWorld world = new (modules.BuildAspect ());
ProtoSystems systems = new (world);
systems
    // Регистрация композитного модуля.
    .AddModule (modules.BuildModule ())
    // Модуль UtilityAI будет отрабатывать тут.
    .AddPoint ("ai-utility")
    .Init ();
```


# Лицензия
Расширение выпускается под коммерческой лицензией, [подробности тут](./LICENSE.md).
