<p align="center">
    <img src="./logo.png" alt="Logo">
</p>

# Unity-интеграция UtilityAI для LeoECS Proto
Поддержка интеграции в Unity для UtilityAI для LeoECS Proto.

> **ВАЖНО!** Требует C#9 (или Unity >=2021.2).

> **ВАЖНО!** Не забывайте использовать `DEBUG`-версии билдов для разработки и `RELEASE`-версии билдов для релизов: все внутренние проверки/исключения будут работать только в `DEBUG`-версиях и удалены для увеличения производительности в `RELEASE`-версиях.

> **ВАЖНО!** Проверено на Unity 2021.3 (зависит от нее) и содержит asmdef-описания для компиляции в виде отдельных сборок и уменьшения времени рекомпиляции основного проекта.


# Социальные ресурсы
[Официальный блог](https://leopotam.com)


# Установка


## В виде исходников
Поддерживается установка в виде исходников из архива, который надо распаковать в проект.


## Прочие источники
Официальные версии выпускаются для активных подписчиков в виде ссылок на актуальные версии.


# Основные типы


## Данные
Существует 3 готовых шаблона данных, доступных через контекстное меню ресурсов
проекта `Create / LeoECS Proto / UtilityAI` с 1, 2 и 3 параметрами соответственно:
`UnityAiUtilityData1`, `UnityAiUtilityData2`, `UnityAiUtilityData3`.

Для их использования экземпляр подключается любым способом в `UtilityAI`-решение
(через инъекцию в конструкторе или как сервис для систем):
```c#
// Создаем новый тип для возможности автоматической инъекции.
class EatFoodSolverData : UnityAiUtilityData2 { }
// Пример решения.
class EatFoodSolver : IAiUtilitySolver {
    [DI] readonly UnitAspect _unit = default;
    [DI] readonly EatFoodSolverData _diData = default;
    
    readonly UnityAiUtilityData2 _ctorData;
    
    // Инъекция через конструктор.
    public EatFoodSolver(UnityAiUtilityData2 data) {
        _data = data;
    }

    public float Solve (ProtoEntity entity) {
        ref Unit unit = ref _unit.Units.Get (entity);
        // Можно воспользоваться любым способом оценки данных.
        // return _diData.Evaluate (unit.Hunger, unit.Food);
        // return _ctorData.Evaluate (unit.Hunger, unit.Food);
    }

    public void Apply (ProtoEntity entity) {
        // Употребляем еду.
        ref Unit unit = ref _unit.Units.Get (entity);
        unit.Food--;
        unit.Hunger = 0;
    }
}
```
> **ВАЖНО!** Поле имени у каждого параметра не играет никакой роли и используется только в информационных целях.

> **ВАЖНО!** Данные не нормализуются и используются с кривых как есть.


# Лицензия
Расширение выпускается под коммерческой лицензией, [подробности тут](./LICENSE.md).
