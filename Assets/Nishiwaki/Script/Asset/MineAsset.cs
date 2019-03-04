using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    [CreateAssetMenu(fileName = "NewMineAsset", menuName = "Mine/Create Mine Asset")]

    public class MineAsset : ScriptableObject
    {
        public float Power;
        public float Range;
    }
}