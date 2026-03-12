// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–
// Коммерческая лицензия подписчика
// (c) 2023 Leopotam <leopotam@yandex.ru>
// –‒–‒–‒‒––‒–‒––‒––––––‒–‒–––––––––‒––‒–––––‒‒–––‒–‒––––‒––‒–––‒–‒––‒–––‒–

using TMPro;
using UnityEngine;

namespace Leopotam.Localization.Unity {
    [RequireComponent (typeof (TMP_Text))]
    public class UnityTextLocalization : MonoBehaviour, ILocalizationListener {
        [SerializeField] string _category;
        [SerializeField] string _token;

        TMP_Text _text;
        Localization _l;

        void Awake () {
            _text = GetComponent<TMP_Text> ();
        }

        void OnEnable () {
            var lg = UnityLocalization.Get ();
            if (lg != null) {
                _l = lg;
                _l.AddListener (this);
                UpdateLocalization ();
            }
        }

        void OnDisable () {
            if (_l != null) {
                _l.RemoveListener (this);
                _l = null;
            }
        }

        public void UpdateLocalization () {
            if (_l != null) {
                var (val, ok) = _l.Get (_token, _category);
                _text.text = val;
#if DEBUG
                if (!ok) {
                    Debug.LogWarning ($"[Локализация] Не могу найти ключ \"{_token}\" в категории \"{_category}\"", gameObject);
                }
#endif
            }
        }

        public void OnLanguageChanged () {
            UpdateLocalization ();
        }
    }
}
