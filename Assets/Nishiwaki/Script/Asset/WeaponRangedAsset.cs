using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    [CreateAssetMenu(fileName = "NewRangedAsset", menuName = "Weapon/Ranged/Create Ranged Asset")]

    public class WeaponRangedAsset : ScriptableObject
    {
        public GameObject BulletObject;
        public float Rate;
        public int Magazine;
    }
}