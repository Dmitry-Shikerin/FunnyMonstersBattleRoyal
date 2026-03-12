using UnityEngine;

namespace Sources.EcsBoundedContexts.Weapons.Presentation
{
    public class WeaponView : MonoBehaviour
    {
        [field: SerializeField] public Transform WeaponTransform { get; private set; }

        public void SetPosition(Vector3 position)
        {
            transform.localPosition = Vector3.zero;
            WeaponTransform.localPosition = position;
        }

        public void SetRotation(Vector3 rotation)
        {
            transform.localRotation = Quaternion.identity;
            WeaponTransform.localRotation = Quaternion.Euler(rotation);
        }

        public void SetParent(Transform parent)
        {
            transform.SetParent(parent);
        }
    }
}