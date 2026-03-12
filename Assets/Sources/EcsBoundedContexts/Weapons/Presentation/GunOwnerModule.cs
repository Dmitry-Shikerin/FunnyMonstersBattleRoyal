using Leopotam.EcsProto.Unity.Plugins.LeoEcsProtoCs.Leopotam.EcsProto.Unity.Runtime;
using UnityEngine;
using UnityEngine.Animations;

namespace Sources.EcsBoundedContexts.Weapons.Presentation
{
    public class GunOwnerModule : EntityModule
    {
        [field: SerializeField] public WeaponView Weapon { get; private set; }
        [field: Header("Equipped")]
        [field: SerializeField] public ParentConstraint EquippedParentConstraint { get; private set; }
        [field: SerializeField] public Vector3 EquippedLocalPosition { get; private set; }
        [field: SerializeField] public Vector3 EquippedLocalRotation { get; private set; }
        [field: Header("UnEquipped")]
        [field: SerializeField] public ParentConstraint UnequippedParentConstraint { get; private set; }
        [field: SerializeField] public Vector3 UnEquippedLocalPosition { get; private set; }
        [field: SerializeField] public Vector3 UnEquippedLocalRotation { get; private set; }

        public void Equip()
        {
            Weapon.SetParent(EquippedParentConstraint.transform);
            Weapon.SetPosition(EquippedLocalPosition);
            Weapon.SetRotation(EquippedLocalRotation);
        }

        public void UnEquip()
        {
            Weapon.SetParent(UnequippedParentConstraint.transform);
            Weapon.SetPosition(UnEquippedLocalPosition);
            Weapon.SetRotation(UnEquippedLocalRotation);
        }
    }
}