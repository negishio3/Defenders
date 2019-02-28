using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    [CreateAssetMenu(fileName = "NewRangedAsset", menuName = "Weapon/Ranged/Create Ranged Asset")]

    public class WeaponRangedAsset : ScriptableObject
    {
        public GameObject BulletPrefab;
        public float Rate;
        public int Magazine;
        public int ShotSpeed;
    }
}