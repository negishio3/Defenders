using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    [CreateAssetMenu(fileName = "NewMeleeAsset", menuName = "Weapon/Melee/Create Melee Asset")]

    public class WeaponMeleeAsset : ScriptableObject
    {
        public float Power;
    }
}