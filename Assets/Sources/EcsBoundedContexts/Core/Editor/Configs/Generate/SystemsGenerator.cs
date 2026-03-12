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
    public static class SystemsGenerator
    {
        public static void GenerateSystemsInstaller(AspectName aspect)
        {
            List<Type> types = GetSystemsTypes(aspect);
            List<string> namespaces = new List<string>();
            string path = EcsGenerator.Instance.AspectPath;
            string fileName = aspect.ToString() + "SystemsInstaller";
            string fullPath = $"{Application.dataPath}/{path}/{fileName}.cs";
            StringBuilder builder = new StringBuilder();
            builder.Append("using MyDependencies.Sources.Containers;\n");
            builder.Append("using MyDependencies.Sources.Containers.Extensions;\n");

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
            builder.Append($"\tpublic static class {fileName}\n");
            builder.Append("\t{\n");
            builder.Append("\t\tpublic static void InstallBindings(DiContainer container)\n");
            builder.Append("\t\t{\n");

            foreach (string groupName in Enum.GetNames(typeof(ComponentGroup)))
            {
                builder.Append($"\t\t\t//{groupName}\n");

                foreach (Type type in types)
                {
                    ComponentGroupAttribute attribute = type.GetCustomAttribute<ComponentGroupAttribute>();

                    if (attribute == null)
                        continue;

                    if (attribute.Group.ToString() != groupName)
                        continue;

                    string name = type.Name;

                    builder.Append($"\t\t\tcontainer.Bind<{name}>();\n");
                }

                builder.AppendLine();
            }
            
            builder.Append("\t\t}\n");
            builder.Append("\t}\n");
            builder.Append("}\n");

            string fullCode = builder.ToString();

            File.WriteAllText(fullPath, fullCode);
            builder.Clear();
        }
        
        public static void GenerateSystemsCollector(AspectName aspect)
        {
            List<Type> types = GetSystemsTypes(aspect);
            List<string> namespaces = new List<string>();
            string path = EcsGenerator.Instance.AspectPath;
            string fileName = aspect.ToString() + "SystemsCollector";
            string fullPath = $"{Application.dataPath}/{path}/{fileName}.cs";
            StringBuilder builder = new StringBuilder();
            builder.Append("using System.Collections.Generic;\n");
            builder.Append("using Leopotam.EcsProto;\n");
            builder.Append($"using {typeof(ISystemsCollector).Namespace};\n");

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
            builder.Append($"\tpublic class {fileName} : ISystemsCollector\n");
            builder.Append("\t{\n");
            builder.Append("\t\tprivate readonly ProtoSystems _protoSystems;\n");
            builder.Append("\t\tprivate readonly IEnumerable<IProtoSystem> _systems;\n");
            builder.AppendLine();
            builder.Append($"\t\tpublic {fileName}(\n");
            builder.Append("\t\t\tProtoSystems protoSystems,\n");

            //string[] groupNames = Enum.GetNames(typeof(ComponentGroup));
            // foreach (Type type in types)
            // {
            //     string name = type.Name;
            //     int order = type.GetCustomAttribute<EcsSystemAttribute>().Order;
            //     string groupName = type.GetCustomAttribute<EcsSystemAttribute>().Group.ToString();
            //     builder.Append($"\t\t\t{name} {name.FirstToLower()}, //Order: {order} //{groupName}\n");
            // }

            for (int i = 0; i < types.Count; i++)
            {
                string name = types[i].Name;
                int order = types[i].GetCustomAttribute<EcsSystemAttribute>().ExecutionOrder;
                string groupName = types[i].GetCustomAttribute<ComponentGroupAttribute>().Group.ToString();
                
                if (i == types.Count - 1)
                {
                    builder.Append($"\t\t\t{name} {name.FirstToLower()} //Order: {order} //{groupName}\n");
                    continue;
                }
                
                builder.Append($"\t\t\t{name} {name.FirstToLower()}, //Order: {order} //{groupName}\n");
            }

            // for (int i = 0; i < groupNames.Length; i++)
            // {
            //     List<Type> groupTypes = GetGroupTypes(types, groupNames[i]);
            //
            //     for (int j = 0; j < groupTypes.Count; j++)
            //     {
            //         string name = groupTypes[j].Name;
            //         int order = groupTypes[j].GetCustomAttribute<EcsSystemAttribute>().Order;
            //
            //         if (i == groupNames.Length - 1 && j == groupTypes.Count - 1)
            //         {
            //             builder.Append($"\t\t\t{name} {name.FirstToLower()} //Order: {order} //{groupNames[i]}\n");
            //             continue;
            //         }
            //         
            //         builder.Append($"\t\t\t{name} {name.FirstToLower()}, //Order: {order} //{groupNames[i]}\n");
            //     }
            // }
            
            builder.Append("\t\t)\n");
            builder.Append("\t\t{\n");
            builder.Append("\t\t\t_protoSystems = protoSystems;\n");
            builder.Append("\t\t\t_systems = new IProtoSystem[]\n");
            builder.Append("\t\t\t{\n");

            // foreach (string groupName in groupNames)
            // {
            //     List<Type> groupTypes = GetGroupTypes(types, groupName);
            //
            //     foreach (Type groupType in groupTypes)
            //     {
            //         string name = groupType.Name;
            //         builder.Append($"\t\t\t\t{name.FirstToLower()}, //{groupName}\n");
            //     }
            // }
            
            foreach (Type type in types)
            {
                string name = type.Name;
                string groupName = type.GetCustomAttribute<ComponentGroupAttribute>().Group.ToString();
                builder.Append($"\t\t\t\t{name.FirstToLower()}, //{groupName}\n");
            }
            
            builder.Append("\t\t\t};\n");
            builder.Append("\t\t}\n");
            builder.AppendLine();
            builder.Append("\t\tpublic void AddSystems()\n");
            builder.Append("\t\t{\n");
            builder.Append("\t\t\tforeach (IProtoSystem system in _systems)\n");
            builder.Append("\t\t\t\t_protoSystems.AddSystem(system);\n");
            builder.Append("\t\t}\n");
            builder.Append("\t}\n");
            builder.Append("}\n");

            string fullCode = builder.ToString();

            File.WriteAllText(fullPath, fullCode);
            builder.Clear();
        }
        
        private static List<Type> GetSystemsTypes(AspectName aspectName)
        {
            List<Type> types = new List<Type>();
            List<Type> allTypes = GetSystemsTypes();

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
            
            return types.OrderBy(type => type.GetCustomAttribute<EcsSystemAttribute>().ExecutionOrder).ToList();
        }     
        
        private static List<Type> GetSystemsTypes()
        {
            List<Type> types = new List<Type>();
            Type attrType = typeof(EcsSystemAttribute);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsClass && Attribute.IsDefined(type, attrType))
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