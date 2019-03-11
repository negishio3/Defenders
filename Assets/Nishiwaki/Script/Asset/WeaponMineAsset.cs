using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    [CreateAssetMenu(fileName = "NewWeaponMineAsset", menuName = "Item/Mine/Create WeaponMine Asset")]

    public class WeaponMineAsset : ScriptableObject
    {
        public GameObject MinePrefab;
        public int Magazine;
    }
}