using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    [CreateAssetMenu(fileName = "NewWeaponTurretAsset", menuName = "Item/Turret/Create WeaponTurret Asset")]

    public class WeaponTurretAsset : ScriptableObject
    {
        public GameObject TurretPrefab;
        public int Magazine;
    }
}