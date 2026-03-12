using Sources.Frameworks.DeepFramework.DeepTwens.Domain.Dictionaries;
using Sources.Frameworks.DeepFramework.DeepTwens.Eases;
using UnityEngine;

namespace Sources.Frameworks.DeepFramework.DeepTwens.Domain.Configs
{
    [CreateAssetMenu(fileName = nameof(DeepTweenSettings), menuName = "MyTween/" + nameof(DeepTweenSettings), order = 51)]
    public class DeepTweenSettings : ScriptableObject
    {
        private static DeepTweenSettings s_instance;
        
        public static DeepTweenSettings Instance
        {
            get
            {
                if (s_instance == null)
                    s_instance = Resources.Load<DeepTweenSettings>(nameof(DeepTweenSettings));

                if (s_instance == null)
                {
                    s_instance = CreateInstance<DeepTweenSettings>();
                    s_instance.name = nameof(DeepTweenSettings);
                }
                
                return s_instance;
            }
        }
        
        [SerializeField] private EaseDictionary _easeDictionary = new ();
        
        public static AnimationCurve Get(Ease ease) =>
            Instance._easeDictionary[ease];
    }
}