using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    [CreateAssetMenu(fileName = "NewTurretAsset", menuName = "Item/Turret/Create Turret Asset")]

    public class TurretAsset : ScriptableObject
    {
        public float Range;
        public float Rate;
        public int ShotSpeed;
        public int HP;
    }
}