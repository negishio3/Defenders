using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    [CreateAssetMenu(fileName = "NewMineExplosionAsset", menuName = "Item/Mine/Create MineExplosion Asset")]

    public class MineExplosionAsset : ScriptableObject
    {
        public float Power;
        public float Range;
    }
}