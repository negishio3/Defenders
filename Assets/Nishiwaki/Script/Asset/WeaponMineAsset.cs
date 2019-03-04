using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    [CreateAssetMenu(fileName = "NewWeaponMineAsset", menuName = "Weapon/Item/Create Mine Asset")]

    public class WeaponMineAsset : ScriptableObject
    {
        public GameObject MinePrefab;
        public int Magazine;
    }
}