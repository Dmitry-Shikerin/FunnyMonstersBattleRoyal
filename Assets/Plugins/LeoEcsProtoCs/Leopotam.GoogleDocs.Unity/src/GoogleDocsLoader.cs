// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UnityEngine.Networking;
#if ENABLE_IL2CPP
using Unity.IL2CPP.CompilerServices;
#endif

namespace Leopotam.GoogleDocs.Unity {
#if ENABLE_IL2CPP
    [Il2CppSetOption (Option.NullChecks, false)]
    [Il2CppSetOption (Option.ArrayBoundsChecks, false)]
#endif
    public static class GoogleDocsLoader {
        public static async Task<(string, string)> LoadCsvPage (string fullPageUrl) {
            var url = fullPageUrl
                .Replace ("?", "")
                .Replace ("#", "&")
                .Replace ("/edit", "/export?format=csv&")
                .Replace ("&&", "&");
            using (var client = UnityWebRequest.Get (url)) {
                try {
                    await client.SendWebRequest ();
                    if (client.error != null) {
                        return (null, client.error);
                    }
                    var data = client.downloadHandler.text;
                    return (data, default);
                } catch (Exception ex) {
                    return (default, ex.Message);
                }
            }
        }
    }

    static class AsyncExtensions {
        public static TaskAwaiter GetAwaiter (this UnityEngine.AsyncOperation op) {
            var tcs = new TaskCompletionSource<object> ();
            op.completed += _ => tcs.SetResult (null);
            return ((Task) tcs.Task).GetAwaiter ();
        }
    }
}
#if ENABLE_IL2CPP
// Unity IL2CPP performance optimization attribute.
namespace Unity.IL2CPP.CompilerServices {
    enum Option {
        NullChecks = 1,
        ArrayBoundsChecks = 2
    }

    [AttributeUsage (AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    class Il2CppSetOptionAttribute : Attribute {
        public Option Option { get; private set; }
        public object Value { get; private set; }

        public Il2CppSetOptionAttribute (Option option, object value) { Option = option; Value = value; }
    }
}
#endif
