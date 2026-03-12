using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Sources.EcsBoundedContexts.Core.Domain;
using Sources.EcsBoundedContexts.Core.Domain.Systems;
using Sources.Frameworks.DeepFramework.DeepUtils.Extensions;
using UnityEngine;

namespace Sources.EcsBoundedContexts.Core.Editor.Configs.Generate
{
    public static class AspectGenerator
    {
        public static void GenerateAspect(AspectName aspectName)
        {
            List<Type> types = GetComponentsTypes();
            List<string> namespaces = new List<string>();
            string path = EcsGenerator.Instance.AspectPath;
            
            if (string.IsNullOrEmpty(path))
                throw new Exception("Path is null or empty");

            if (string.IsNullOrWhiteSpace(path))
                throw new Exception("Path is null or whitespace");
            
            string fileName = aspectName.ToString() + "Aspect";
            string fullPath = $"{Application.dataPath}/{path}/{fileName}.cs";
            StringBuilder builder = new StringBuilder();
            builder.Append("using System;\n");
            builder.Append("using System.Collections.Generic;\n");
            builder.Append("using Leopotam.EcsProto;\n");
            builder.Append("using Leopotam.EcsProto.QoL;\n");

            foreach (Type type in types)
            {
                string namesp = type.Namespace;

                if (namespaces.Contains(namesp))
                    continue;

                namespaces.Add(namesp);
                builder.Append($"using {namesp};\n");
            }

            builder.AppendLine();
            builder.Append($"namespace {path.Replace("/", ".")}\n");
            builder.Append("{\n");
            builder.Append($"\tpublic class {fileName} : ProtoAspectInject\n");
            builder.Append("\t{\n");
            builder.Append("\t\tpublic readonly Dictionary<Type, IProtoPool> Pools;\n");
            builder.AppendLine();

            foreach (string groupName in Enum.GetNames(typeof(ComponentGroup)))
            {
                builder.Append($"\t\t//{groupName}\n");

                foreach (Type type in types)
                {
                    ComponentAttribute attribute = type.GetCustomAttribute<ComponentAttribute>();

                    if (attribute == null)
                        continue;

                    if (attribute.Group.ToString() != groupName)
                        continue;

                    string name = GetComponentName(type);

                    builder.Append($"\t\tpublic readonly ProtoPool<{type.Name}> {name} = new ();\n");
                }

                builder.AppendLine();
            }

            builder.Append($"\t\tpublic {fileName}()\n");
            builder.Append("\t\t{\n");
            builder.Append("\t\t\tPools = new ()\n");
            builder.Append("\t\t\t{\n");

            foreach (Type type in types)
            {
                string name = GetComponentName(type);

                builder.Append($"\t\t\t\t[typeof(ProtoPool<{type.Name}>)] = {name},\n");
            }

            builder.Append("\t\t\t};\n");
            builder.AppendLine();
            builder.Append($"\t\t\t{fileName}Ext.Construct(this);\n");
            builder.Append("\t\t}\n");
            builder.Append("\t}\n");
            builder.Append("}\n");

            string fullCode = builder.ToString();

            File.WriteAllText(fullPath, fullCode);
            builder.Clear();
        }

        public static void GenerateAspectExt(AspectName aspect)
        {
            List<Type> types = GetComponentsTypes();
            List<string> namespaces = new List<string>();
            string path = EcsGenerator.Instance.AspectPath;
            string aspectName = aspect.ToString() + "Aspect";
            string fieldAspectName = $"s_{aspectName}";
            string fileName = aspectName + "Ext";
            string fullPath = $"{Application.dataPath}/{path}/{fileName}.cs";
            StringBuilder builder = new StringBuilder();
            builder.Append("using System;\n");
            builder.Append("using UnityEngine;\n");
            builder.Append("using System.Collections.Generic;\n");
            builder.Append("using Leopotam.EcsProto;\n");
            builder.Append("using Leopotam.EcsProto.QoL;\n");

            foreach (Type type in types)
            {
                string namesp = type.Namespace;

                if (namespaces.Contains(namesp) == false)
                {
                    namespaces.Add(namesp);
                    builder.Append($"using {namesp};\n");
                }

                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
                
                foreach (FieldInfo fieldInfo in fields)
                {
                    namesp = fieldInfo.FieldType.Namespace;
                    
                    if (namespaces.Contains(namesp))
                        continue;
                    
                    namespaces.Add(namesp);
                    builder.Append($"using {namesp};\n");
                }

                PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                
                foreach (PropertyInfo propertyInfo in properties)
                {
                    namesp = propertyInfo.PropertyType.Namespace;
                    
                    if (namespaces.Contains(namesp))
                        continue;
                    
                    namespaces.Add(namesp);
                    builder.Append($"using {namesp};\n");
                }
            }

            builder.AppendLine();
            builder.Append($"namespace {path.Replace("/", ".")}\n");
            builder.Append("{\n");
            builder.Append($"\tpublic static class {fileName}\n");
            builder.Append("\t{\n");
            builder.Append($"\t\tprivate static {aspectName} {fieldAspectName};\n");
            builder.AppendLine();
            builder.Append($"\t\tpublic static void Construct({aspectName} aspect) =>\n");
            builder.Append($"\t\t\t{fieldAspectName} = aspect ?? throw new ArgumentNullException(nameof(aspect));\n");
            builder.AppendLine();

            //Unique
            foreach (Type type in types)
            {
                if (type.GetCustomAttribute<UniqueComponentAttribute>() == null)
                    continue;
                
                string name = GetComponentName(type);
                string lowerName = name.FirstToLower();
                string typeName = type.Name;
                string typeLowerName = typeName.FirstToLower();

                builder.Append($"\t\t//{name}\n");
                
                //Get
                builder.Append($"\t\tpublic static ProtoEntity Get{name}(this MainAspect aspect)\n");
                builder.Append("\t\t{\n");
                builder.Append($"\t\t\tif ({fieldAspectName}.{name}.Entities().Length == 0)\n");
                builder.Append("\t\t\t\tthrow new Exception($\"Entity not initialized\");\n");
                builder.AppendLine();
                builder.Append($"\t\t\treturn {fieldAspectName}.{name}.Entities()[0];\n");
                builder.Append("\t\t}\n");
                builder.AppendLine();
                
                //Is
                builder.Append($"\t\tpublic static void Is{name}(this ProtoEntity entity, bool value)\n");
                builder.Append("\t\t{\n");
                builder.Append("\t\t\tif (value)\n");
                builder.Append("\t\t\t{\n");
                builder.Append($"\t\t\t\tif ({fieldAspectName}.{name}.Entities().Length > 0)\n");
                builder.Append("\t\t\t\t\tthrow new Exception($\"Entity {entity} already has\");\n");
                builder.AppendLine();
                builder.Append($"\t\t\t\t{fieldAspectName}.{name}.Add(entity);\n");
                builder.Append($"\t\t\t\treturn;\n");
                builder.Append("\t\t\t}\n");
                builder.AppendLine();
                builder.Append($"\t\t\t{fieldAspectName}.{name}.Del(entity);\n");
                builder.Append("\t\t}\n");
                builder.AppendLine();
                
                //Has
                builder.Append($"\t\tpublic static bool Has{name}(this ProtoEntity entity) =>\n");
                builder.Append($"\t\t\t{fieldAspectName}.{name}.Has(entity);\n");
                builder.AppendLine();
            }
            
            foreach (Type type in types)
            {
                if (type.GetCustomAttribute<UniqueComponentAttribute>() != null)
                    continue;
                
                string name = GetComponentName(type);
                string lowerName = name.FirstToLower();
                string typeName = type.Name;
                string typeLowerName = typeName.FirstToLower();

                builder.Append($"\t\t//{name}\n");
                
                //Has
                builder.Append($"\t\tpublic static bool Has{name}(this ProtoEntity entity) =>\n");
                builder.Append($"\t\t\t{fieldAspectName}.{name}.Has(entity);\n");

                builder.AppendLine();

                //Get
                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance);
                PropertyInfo[] properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                StringBuilder fieldBuilder = new StringBuilder();

                if (fields.Length != 0)
                {
                    builder.Append($"\t\tpublic static ref {typeName} Get{name}(this ProtoEntity entity) =>\n");
                    builder.Append($"\t\t\tref {fieldAspectName}.{name}.Get(entity);\n");

                    builder.AppendLine();

                    foreach (FieldInfo fieldInfo in fields)
                    {
                        string fieldName = GetFieldName(fieldInfo.FieldType);
                        string lowerFieldName = fieldInfo.Name.FirstToLower();
                        fieldBuilder.Append($", {fieldName} {lowerFieldName}");
                    }
                    
                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        string fieldName = GetFieldName(propertyInfo.PropertyType);
                        string lowerFieldName = propertyInfo.Name.FirstToLower();
                        fieldBuilder.Append($", {fieldName} {lowerFieldName}");
                    }

                }
                
                string arguments = fieldBuilder.ToString();

                //Replace
                if (fields.Length != 0)
                {
                    builder.Append($"\t\tpublic static void Replace{name}(this ProtoEntity entity{arguments})\n");
                    fieldBuilder.Clear();
                    builder.Append("\t\t{\n");
                    builder.Append($"\t\t\tref {type.Name} {typeLowerName} = ref {fieldAspectName}.{name}.Get(entity);\n");

                    foreach (FieldInfo fieldInfo in fields)
                    {
                        string fieldName = fieldInfo.Name;
                        builder.Append($"\t\t\t{typeLowerName}.{fieldName} = {fieldName.FirstToLower()};\n");
                    }
                
                    foreach (PropertyInfo propertyInfo in properties)
                    {
                        string fieldName = propertyInfo.Name;
                        builder.Append($"\t\t\t{typeLowerName}.{fieldName} = {fieldName.FirstToLower()};\n");
                    }

                    builder.Append("\t\t}\n");
                    builder.AppendLine();
                }

                //Add
                builder.Append($"\t\tpublic static ref {typeName} Add{name}(this ProtoEntity entity{arguments})\n");
                builder.Append("\t\t{\n");
                builder.Append($"\t\t\tref {type.Name} {typeLowerName} = ref {fieldAspectName}.{name}.Add(entity);\n");

                foreach (FieldInfo fieldInfo in fields)
                {
                    string fieldName = fieldInfo.Name;
                    builder.Append($"\t\t\t{typeLowerName}.{fieldName} = {fieldName.FirstToLower()};\n");
                }

                foreach (PropertyInfo propertyInfo in properties)
                {
                    string fieldName = propertyInfo.Name;
                    builder.Append($"\t\t\t{typeLowerName}.{fieldName} = {fieldName.FirstToLower()};\n");
                }

                builder.Append($"\t\t\treturn ref {typeLowerName};\n");
                builder.Append("\t\t}\n");

                builder.AppendLine();
                
                //Del
                builder.Append($"\t\tpublic static void Del{name}(this ProtoEntity entity)\n");
                builder.Append($"\t\t\t=> {fieldAspectName}.{name}.Del(entity);\n");
                builder.AppendLine();
                
                //TODO добавить метод для NewEntity
            }
            
            builder.Append("\t}\n");
            builder.Append("}\n");

            string fullCode = builder.ToString();

            File.WriteAllText(fullPath, fullCode);
            builder.Clear();
        }

        private static string GetComponentName(Type type)
        {
            string name = type.Name;

            if (name.Contains("Component"))
                name = name.Replace("Component", "");

            if (name.Contains("Tag"))
                name = name.Replace("Tag", "");

            return name;
        }

        private static string GetFieldName(Type type)
        {
            return type.Name switch
            {
                nameof(Boolean) => "bool",
                nameof(Single) => "float",
                nameof(Int32) => "int",
                "List`1" => "List<" + GetFieldName(type.GetGenericArguments()[0]) + ">",
                _ => type.Name
            };
        }

        private static List<Type> GetComponentsTypes(AspectName aspectName)
        {
            List<Type> types = new List<Type>();
            List<Type> allTypes = GetComponentsTypes();

            foreach (Type type in allTypes)
            {
                AspectAttribute attribute = type.GetCustomAttribute<AspectAttribute>();
                
                if (attribute == null && aspectName == EcsGenerator.Instance.DefaultAspectName)
                {
                    types.Add(type);
                    continue;
                }
                
                if (attribute.Aspects.ToList().Contains(aspectName) == false)
                    continue;
                
                types.Add(type);
            }
            
            return types;
        }     
        
        private static List<Type> GetComponentsTypes()
        {
            List<Type> types = new List<Type>();
            Type attrType = typeof(ComponentAttribute);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsValueType && Attribute.IsDefined(type, attrType))
                    {
                        string typeName = type.Name;

                        if (string.IsNullOrEmpty(typeName))
                        {
                            // ReSharper disable once PossibleNullReferenceException
                            typeName = type.FullName.Replace('.', '/').Replace('+', '/');
                        }

                        if (types.Contains(type))
                        {
                            Debug.LogWarning($"[ProtoUnityAuthoring] компонент с именем \"{typeName}\" уже зарегистрирован, тип \"{type}\" проигнорирован");
                            continue;
                        }

                        types.Add(type);
                    }
                }
            }

            return types;
        }
    }
}