// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023-2024 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using System;
using System.Text;
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
    public class XlsxProxyClient {
        readonly string _serverUrl;
        readonly string _auth;
        const string PathDirect = "/direct";
        const string PathYandex = "/yandex";
        const string PathGoogle = "/google";

        public XlsxProxyClient (string serverUrl, string login = default, string pass = default) {
            _serverUrl = serverUrl;
#if DEBUG
            if (string.IsNullOrEmpty (login) != string.IsNullOrEmpty (pass)) {
                throw new Exception ("логин и пароль должны быть одновременно или пустыми или проинициализированными");
            }
#endif
            if (!string.IsNullOrEmpty (login)) {
                _auth = $"Basic {Convert.ToBase64String (Encoding.UTF8.GetBytes (login + ":" + pass))}";
            }
        }

        public async Task<(string, string)> LoadFromDirect (string docUrl, string docPage) {
            var url = GetProxyUrl (_serverUrl, PathDirect, docUrl, docPage);
            return await LoadFile (url, _auth);
        }

        public async Task<(string, string)> LoadFromYandex (string docUrl, string docPage) {
            var url = GetProxyUrl (_serverUrl, PathYandex, docUrl, docPage);
            return await LoadFile (url, _auth);
        }

        public async Task<(string, string)> LoadFromGoogle (string docUrl, string docPage) {
            var url = GetProxyUrl (_serverUrl, PathGoogle, docUrl, docPage);
            return await LoadFile (url, _auth);
        }

        static async Task<(string, string)> LoadFile (string url, string auth) {
            using (var client = UnityWebRequest.Get (url)) {
                if (auth != null) {
                    client.SetRequestHeader ("Authorization", auth);
                }
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

        static string GetProxyUrl (string serverUrl, string loaderType, string docUrl, string docPage) {
            docUrl = UnityWebRequest.EscapeURL (docUrl);
            docPage = UnityWebRequest.EscapeURL (docPage);
            return $"{serverUrl}{loaderType}?page={docPage}&url={docUrl}";
        }
    }
}
