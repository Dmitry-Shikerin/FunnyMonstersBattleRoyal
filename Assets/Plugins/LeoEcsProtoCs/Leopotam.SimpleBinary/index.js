/** @type {Object.<string, string>} */
const SimpleTypes = {
    'i8': 'sbyte',
    'u8': 'byte',
    'i16': 'short',
    'u16': 'ushort',
    'i32': 'int',
    'u32': 'uint',
    'f32': 'float',
    'f64': 'double',
    's16': 'string',
}

class IniSectionItem {
    /** @type {string} */
    #name
    /** @type {string} */
    #value
    /** @param {string} name @param {string} value   */
    constructor(name, value) {
        this.#name = name
        this.#value = value
    }
    name() {
        return this.#name
    }
    value() {
        return this.#value
    }
}
class IniSection {
    /** @type {string} */
    #name
    /** @type {IniSectionItem[]} */
    #items
    /** @param {string} name  */
    constructor(name) {
        this.#name = name
        this.#items = []
    }
    /** @param {string} name @param {string} type */
    addItem(name, type) {
        for (const i of this.#items) {
            if (i.name() === name) { throw new Error(`секция "${this.#name}" уже содержит элемент "${name}"`) }
        }
        this.#items.push(new IniSectionItem(name, type))
    }
    name() {
        return this.#name
    }
    items() {
        return this.#items
    }
    /** @param {string} name  */
    getItem(name) {
        for (const s of this.#items) {
            if (s.name() === name) { return s }
        }
        return null
    }
    /** @param {string} name  */
    getValue(name) {
        const item = this.getItem(name)
        return item && item.value() || ''
    }
}

class IniFile {
    /** @type {IniSection[]} */
    #sections
    /** @param {string} text  */
    constructor(text) {
        this.#sections = []
        const rawLines = text.match(/[^\r\n]+/g)
        if (!rawLines) { return }
        /** @type {IniSection|null} */
        let section = null
        for (const rawLine of rawLines) {
            let line = rawLine.trim()
            // комментарии.
            const commentIdx = line.indexOf(';')
            if (commentIdx !== -1) { line = line.substring(0, commentIdx).trimEnd() }
            if (!line) { continue }
            // новая секция.
            if (line.indexOf('[') === 0) {
                if (line[line.length - 1] !== ']') { throw new Error(`некорректное описание секции: "${line}"`) }
                section = this.addSection(line.substring(1, line.length - 1))
                continue
            }
            if (!section) { throw new Error(`строка "${line}" существует вне секции`) }
            const sepIdx = line.indexOf('=')
            if (sepIdx === -1) { throw new Error(`некорректная строка "${line}" - нет разделителя имени и значения`) }
            const fieldName = line.substring(0, sepIdx).trim()
            const fieldValue = line.substring(sepIdx + 1).trim()
            if (!fieldName) { throw new Error(`некорректная строка "${line}" - имя поля не может быть пустым`) }
            section.addItem(fieldName, fieldValue)
        }
    }
    /** @param {string} name  */
    addSection(name) {
        for (const s of this.#sections) {
            if (s.name() === name) { throw new Error(`секция "${name}" уже существует`) }
        }
        const section = new IniSection(name)
        this.#sections.push(section)
        return section
    }
    sections() {
        return this.#sections
    }
    /** @param {string} name  */
    getSection(name) {
        for (const s of this.#sections) {
            if (s.name() === name) { return s }
        }
        return null
    }
}

class Config {
    /** @type {IniSection} */
    #cfg
    /** @type {string} */
    #enumName
    #listPool
    /** @type {IniFile} */
    #data
    /** @param {IniFile} cfg, @param {IniFile} data   */
    constructor(cfg, data) {
        this.#cfg = cfg.getSection('cs') || new IniSection('cs')
        this.#data = data
        // кеш.
        this.#enumName = this.#cfg.getValue('enumName') || 'SB_PacketType'
        this.#listPool = this.#cfg.getValue('threads').toLowerCase() === 'true' ? 'ListPoolThreaded' : 'ListPool'
    }
    packets() {
        return this.#data.sections()
    }
    /** @param {string} name  */
    getPacket(name) {
        return this.#data.getSection(name)
    }
    namespace() {
        return this.#cfg.getValue('namespace')
    }
    prefix() {
        return this.#cfg.getValue('prefix').replace(/\\n/g, '\n')
    }
    enumName() {
        return this.#enumName
    }
    listPool() {
        return this.#listPool
    }
}
/** @param {string} name */
function toPascal(name) {
    return `${name[0].toUpperCase()}${name.substring(1)}`
}
/** @param {string} name */
function toCamel(name) {
    return `${name[0].toLowerCase()}${name.substring(1)}`
}
/** @param {number} size  */
function tab(size) {
    return ' '.repeat(size * 4)
}
/** @param {IniSection} packet @param {Config} config @param {number} nTab */
function generatePacket(packet, config, nTab) {
    const upcasedPacketName = toPascal(packet.name())
    const enumPackedName = toPascal(config.enumName())
    let content = `${tab(nTab)}public partial struct ${upcasedPacketName} {\n`
    let readContent =
        `${tab(nTab + 1)}public static (${upcasedPacketName}, bool) Deserialize(ref SimpleBinaryReader r, bool withType = true) {\n` +
        `${tab(nTab + 2)}bool ok;\n` +
        `${tab(nTab + 2)}if (withType) {\n` +
        `${tab(nTab + 3)}ushort t;\n` +
        `${tab(nTab + 3)}(t, ok) = r.ReadU16();\n` +
        `${tab(nTab + 3)}if (!ok || t != (ushort)${enumPackedName}.${upcasedPacketName}) { return (default, false); }\n` +
        `${tab(nTab + 2)}}\n` +
        `${tab(nTab + 2)}var v = New();\n`
    let writeContent =
        `${tab(nTab + 1)}public void Serialize(ref SimpleBinaryWriter w, bool withType = true) {\n` +
        `${tab(nTab + 2)}if (withType) { w.WriteU16((ushort)${enumPackedName}.${upcasedPacketName}); }\n`
    let poolContent = ''
    let createContent =
        `${tab(nTab + 1)}public static ${upcasedPacketName} New() {\n` +
        `${tab(nTab + 2)}${upcasedPacketName} v = default;\n`
    let recycleContent = `${tab(nTab + 1)}public void Recycle() {\n`
    for (const field of packet.items()) {
        const upcasedFieldName = toPascal(field.name())
        const lowcasedFieldName = toCamel(field.name())
        let srcType = field.value()
        // проверка на перечисление.
        let isEnum = srcType[0] === '@'
        if (isEnum) { srcType = srcType.substring(1) }
        // проверка на коллекцию.
        const arrIdx = srcType.indexOf('[]')
        const isArray = arrIdx !== -1
        if (isArray) { srcType = srcType.substring(0, arrIdx) }
        let targetType = SimpleTypes[srcType]
        // проверка на простой тип.
        const isSimpleType = !!targetType
        if (!isSimpleType) {
            const checkType = isEnum ? `@${srcType}` : srcType
            if (!config.getPacket(checkType)) { throw new Error(`некорректный тип "${srcType}" для поля "${field.name()}" в пакете "${packet.name()}"`) }
            targetType = toPascal(srcType)
        }
        // обновление блока описания.
        if (isArray) {
            content += `${tab(nTab + 1)}public List<${targetType}> ${upcasedFieldName};\n`
            createContent += `${tab(nTab + 2)}v.${upcasedFieldName} = ${config.listPool()}<${targetType}>.Get();\n`
        } else {
            content += `${tab(nTab + 1)}public ${targetType} ${upcasedFieldName};\n`
            if (targetType === 'string') {
                createContent += `${tab(nTab + 2)}v.${upcasedFieldName} = "";\n`
            }
        }
        // обновление блока чтения.
        if (isArray) {
            readContent += `${tab(nTab + 2)}ushort ${lowcasedFieldName}Count;\n`
            readContent += `${tab(nTab + 2)}(${lowcasedFieldName}Count, ok) = r.ReadU16();\n`
            readContent += `${tab(nTab + 2)}if (!ok) { v.Recycle(); return (default, false); }\n`
            readContent += `${tab(nTab + 2)}for (var i = 0; i < ${lowcasedFieldName}Count; i++) {\n`
            if (isSimpleType || isEnum) {
                if (isEnum) {
                    readContent += `${tab(nTab + 3)}byte ${lowcasedFieldName}Item;\n`
                    readContent += `${tab(nTab + 3)}(${lowcasedFieldName}Item, ok) = r.ReadU8();\n`
                    readContent += `${tab(nTab + 3)}if (!ok) { v.Recycle(); return (default, false); }\n`
                    readContent += `${tab(nTab + 3)}v.${upcasedFieldName}.Add((${targetType})${lowcasedFieldName}Item);\n`
                } else {
                    readContent += `${tab(nTab + 3)}${targetType} ${lowcasedFieldName}Item;\n`
                    readContent += `${tab(nTab + 3)}(${lowcasedFieldName}Item, ok) = r.Read${srcType.toUpperCase()}();\n`
                    readContent += `${tab(nTab + 3)}if (!ok) { v.Recycle(); return (default, false); }\n`
                    readContent += `${tab(nTab + 3)}v.${upcasedFieldName}.Add(${lowcasedFieldName}Item);\n`
                }
                recycleContent += `${tab(nTab + 2)}${config.listPool()}<${targetType}>.Recycle(${upcasedFieldName});\n`
            } else {
                readContent += `${tab(nTab + 3)}${targetType} ${lowcasedFieldName}Item;\n`
                readContent += `${tab(nTab + 3)}(${lowcasedFieldName}Item, ok) = ${targetType}.Deserialize(ref r, false);\n`
                readContent += `${tab(nTab + 3)}if (!ok) { v.Recycle(); return (default, false); }\n`
                readContent += `${tab(nTab + 3)}v.${upcasedFieldName}.Add(${lowcasedFieldName}Item);\n`
                // recycleContent += `${tab(nTab + 2)}if (${lowcasedFieldName} != null) {\n`
                recycleContent += `${tab(nTab + 2)}for (int i = 0, iMax = ${upcasedFieldName}.Count; i < iMax; i++) {\n`
                recycleContent += `${tab(nTab + 3)}${upcasedFieldName}[i].Recycle();\n`
                recycleContent += `${tab(nTab + 2)}}\n`
                recycleContent += `${tab(nTab + 2)}${config.listPool()}<${targetType}>.Recycle(${upcasedFieldName});\n`
                // recycleContent += `${tab(nTab + 2)}${upcasedFieldName} = null;\n`
                // recycleContent += `${tab(nTab + 2)}}\n`
            }
            readContent += `${tab(nTab + 2)}}\n`
        } else {
            if (isSimpleType || isEnum) {
                if (isEnum) {
                    readContent += `${tab(nTab + 2)}byte ${lowcasedFieldName};\n`
                    readContent += `${tab(nTab + 2)}(${lowcasedFieldName}, ok) = r.ReadU8();\n`
                } else {
                    readContent += `${tab(nTab + 2)}(v.${upcasedFieldName}, ok) = r.Read${srcType.toUpperCase()}();\n`
                }
            } else {
                readContent += `${tab(nTab + 2)}(v.${upcasedFieldName}, ok) = ${targetType}.Deserialize(ref r, false);\n`
            }
            readContent += `${tab(nTab + 2)}if (!ok) { v.Recycle(); return (default, false); }\n`
            if (isEnum) {
                readContent += `${tab(nTab + 2)}v.${upcasedFieldName} = (${targetType})${lowcasedFieldName};\n`
            }
        }
        // обновление блока записи.
        if (isArray) {
            writeContent += `${tab(nTab + 2)}var ${lowcasedFieldName}Count = ${upcasedFieldName}.Count;\n`
            writeContent += `${tab(nTab + 2)}w.WriteU16((ushort)${lowcasedFieldName}Count);\n`
            writeContent += `${tab(nTab + 2)}for (var i = 0; i < ${lowcasedFieldName}Count; i++) {\n`
            if (isSimpleType || isEnum) {
                if (isEnum) {
                    writeContent += `${tab(nTab + 3)}w.WriteU8((byte)${upcasedFieldName}[i]);\n`
                } else {
                    writeContent += `${tab(nTab + 3)}w.Write${srcType.toUpperCase()}(${upcasedFieldName}[i]);\n`
                }
            } else {
                writeContent += `${tab(nTab + 3)}${upcasedFieldName}[i].Serialize(ref w, false);\n`
            }
            writeContent += `${tab(nTab + 2)}}\n`
        } else {
            if (isSimpleType | isEnum) {
                if (isEnum) {
                    writeContent += `${tab(nTab + 2)}w.WriteU8((byte)${upcasedFieldName});\n`
                } else {
                    writeContent += `${tab(nTab + 2)}w.Write${srcType.toUpperCase()}(${upcasedFieldName});\n`
                }
            } else {
                writeContent += `${tab(nTab + 2)}${upcasedFieldName}.Serialize(ref w, false);\n`
            }
        }
    }
    // финальная сборка.
    // пулы.
    content += poolContent
    if (packet.items().length > 0) { content += '\n' }
    // создание экземпляра.
    content += `${createContent}${tab(nTab + 2)}return v;\n${tab(nTab + 1)}}\n\n`
    // убирание в пул.
    content += `${recycleContent}${tab(nTab + 1)}}\n\n`
    // чтение.
    content += `${readContent}${tab(nTab + 2)}return (v, true);\n${tab(nTab + 1)}}\n\n`
    // запись.
    content += `${writeContent}${tab(nTab + 1)}}\n${tab(nTab)}}\n\n`
    return content
}

/** @param {IniSection} packet @param {Config} config @param {number} nsIndent */
function generateEnum(packet, config, nsIndent) {
    const items = packet.items()
    if (items.length == 0 || items.length > 255) { throw new Error(`некорректное перечисление "${packet.name()}" - должно содержать от 1 до 255 элементов`) }
    const casedPacketName = toPascal(packet.name().substring(1))
    let content = `${tab(nsIndent)}public enum ${casedPacketName} : byte {\n`
    for (const field of items) {
        content += `${tab(nsIndent + 1)}${toPascal(field.name())},\n`
    }
    content += `${tab(nsIndent)}}\n\n`
    return content
}

/** @param {IniFile} iniCfg @param {IniFile} iniSchema */
function generate(iniCfg, iniSchema) {
    if (iniSchema.sections().length == 0) { throw new Error('нет типов для генерации') }
    const config = new Config(iniCfg, iniSchema)
    const prefix = config.prefix()
    const namespace = config.namespace()
    const nsIndent = namespace ? 1 : 0
    let content = prefix ? `${prefix}\n\n` : ''
    content +=
        'using System.Collections.Generic;\n' +
        'using Leopotam.SimpleBinary;\n\n' +
        '// ReSharper disable InconsistentNaming\n\n'
    if (namespace) {
        content += `namespace ${namespace} {\n`
    }

    // список типов пакетов, кроме перечислений.
    content += `${tab(nsIndent)}public enum ${toPascal(config.enumName())} : ushort {\n`
    for (const packet of config.packets()) {
        if (packet.name()[0] === '@') { continue }
        content += `${tab(nsIndent + 1)}${toPascal(packet.name())},\n`
    }
    content += `${tab(nsIndent)}}\n\n`

    // генерация перечислений.
    for (const packet of config.packets()) {
        if (packet.name()[0] !== '@') { continue }
        content += generateEnum(packet, config, nsIndent)
    }

    // генерация пакетов.
    for (const packet of config.packets()) {
        if (packet.name()[0] === '@') { continue }
        content += generatePacket(packet, config, nsIndent)
    }

    if (namespace) {
        content = `${content.trim()}\n}`
    }

    return `${content.trim()}\n`
}

/** @returns {string} */
// eslint-disable-next-line no-unused-vars
function defaultConfig() {
    return '[cs]\n' +
        'namespace=Client.Serialization.V1\n' +
        'prefix=// Сгенерировано автоматически, руками править не рекомендуется.\n' +
        'enumName=SB_PacketType\n' +
        'outFile=generated.cs\n' +
        'threads=false'
}

/** @returns {string} */
// eslint-disable-next-line no-unused-vars
function defaultTitle() {
    return 'SimpleBinary - Генерация для C#'
}

/** @param {IniFile} iniCfg */
function outFileOrDefault(iniCfg) {
    const s = iniCfg.getSection('cs')
    return s && s.getValue('outFile') || 'generated.cs'
}

if (typeof window === 'undefined' || typeof window.document === 'undefined') {
    try {
        const cfgPath = process.argv[2] || ''
        const schemaPath = process.argv[3] || ''
        if (!cfgPath || !schemaPath) {
            throw new Error(
                'Некорректные параметры вызова.\n' +
                'Пример вызова:\n' +
                'npm start "cfg.ini" "schema.ini"\n' +
                '* "cfg.ini" - путь до файла конфигурации.\n' +
                '* "schema.ini" - путь до файла схемы данных.')
        }
        const fs = require('fs')
        const cfgFile = fs.readFileSync(cfgPath, 'utf8')
        const schemaFile = fs.readFileSync(schemaPath, 'utf8')

        const iniCfg = new IniFile(cfgFile)
        const iniSchema = new IniFile(schemaFile)

        const code = generate(iniCfg, iniSchema)
        if (code) {
            fs.writeFileSync(`${outFileOrDefault(iniCfg)}`, code, 'utf8')
        }
    } catch (ex) {
        console.error(ex.message)
    }
}