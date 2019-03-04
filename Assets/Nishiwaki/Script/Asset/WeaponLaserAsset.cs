using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    [CreateAssetMenu(fileName = "NewLaserAsset", menuName = "Weapon/Laser/Create Laser Asset")]

    public class WeaponLaserAsset : ScriptableObject
    {
        public float Range;
        public float OverHeat;
        public float CoolTime;
    }
}
