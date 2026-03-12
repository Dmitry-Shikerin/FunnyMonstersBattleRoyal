<p align="center">
    <img src="./logo.png" alt="Logo">
</p>

# Загрузка таблиц с GoogleDocs, YandexDisk и CDN
Поддержка загрузки табличных данных из GoogleDocs напрямую и с использованием XlsxProxy-сервера (прямые ссылки, YandexDisk, GoogleDocs).

> **ВАЖНО!** Требует C#9 (или Unity >=2021.2).

> **ВАЖНО!** Проверено на Unity 2021.3 (зависит от нее) и содержит asmdef-описания для компиляции в виде отдельных сборок и уменьшения времени рекомпиляции основного проекта.


# Социальные ресурсы
[Официальный блог](https://leopotam.com)


# Установка


## В виде исходников
Поддерживается установка в виде исходников из архива, который надо распаковать в проект.


## Прочие источники
Официальные версии выпускаются для активных подписчиков в виде ссылок на актуальные версии.


# Подготовка данных
> **ВАЖНО!** Скачивание возможно только если документ доступен по публичной ссылке на чтение.

> **ВАЖНО!** Не рекомендуется давать доступ на запись по публичной ссылке.

> **ВАЖНО!** Не рекомендуется использовать скачивание данных с GoogleDocs/YandexDisk/CDN в релизных билдах:
> * Время отклика не гарантировано, может достигать десятка секунд.
> * Лимит по обращению к документу может быстро переполниться на сотнях одновременных обращениях и документ будет заблокирован на какое-то время.


# Чтение GoogleDocs-таблиц напрямую
Поддерживается чтение данных из `GoogleDocs`-таблиц через указание ссылки на одну из страниц документа. Ссылку можно скопировать прямо из адресной строки браузера или через `Sharing`-диалог:
```c#
// Прямая ссылка на страницу в документе.
string url = "https://docs.google.com/spreadsheets/d/1_xxxxxxx-yyyy/edit#gid=0";
(string csv, string err) = await GoogleDocsLoader.LoadCsvPage (url);
if (err != null) {
    // Что-то пошло не так, в err текст ошибки.
}
// Можно работать со строкой csv-данных.
```

# Чтение с использованием XlsxProxy
> **ВАЖНО!** Требуется наличие сконфигурированного и запущенного `xlsxproxy`-сервера из пакета `go/xlsxproxy`.

> **ВАЖНО!** YandexDisk и прямые ссылки подразумевают работу с `xlsx`-файлами.

Для подключения к прокси-серверу используется тип `XlsxProxyClient`:
```c#
// Адрес xlsxproxy-сервера, включая порт и префикс апи.
string xlsxProxy = "http://my.proxy.site:3000";
// Прямая ссылка на xlsx-документ.
string docUrl = "https://direct.download.site/file.xlsx";
string docPage = "Лист1";
var client = new XlsxProxyClient (xlsxProxy);
(string csv, string err) = await client.LoadFromDirect (docUrl, docPage);
if (err != null) {
    // Что-то пошло не так, в err текст ошибки.
}
// Можно работать со строкой csv-данных.
```
Поддерживается скачивание с YandexDisk:
```c#
string xlsxProxy = "http://my.proxy.site:3000";
// Ссылка на xlsx-документ из окна "Поделиться" веб-интерфейса.
string docUrl = "https://disk.yandex.ru/i/aABbCcDdEeFfGg";
string docPage = "Лист1";
var client = new XlsxProxyClient (xlsxProxy);
(string csv, string err) = await client.LoadFromYandex (docUrl, docPage);
```
Поддерживается скачивание с GoogleDocs:
```c#
string xlsxProxy = "http://my.proxy.site:3000";
// Ссылка из адресной строки браузера.
string docUrl = "https://docs.google.com/spreadsheets/d/1_xxxxxxx-yyyy/edit#gid=0";
string docPage = "Лист1";
var client = new XlsxProxyClient (xlsxProxy);
(string csv, string err) = await client.LoadFromGoogle (docUrl, docPage);
```

Поддерживается `http basic` аутентификация для `xlsxproxy`-сервера:
```c#
string xlsxProxy = "http://my.proxy.site:3000";
string login = "test";
string password = "pass";
string docUrl = "https://direct.download.site/file.xlsx";
string docPage = "Лист1";
var client = new XlsxProxyClient (xlsxProxy, login, pass);
(string csv, string err) = await client.LoadFromDirect (docUrl, docPage);
```


# Лицензия
Расширение выпускается под коммерческой лицензией, [подробности тут](./LICENSE.md).
