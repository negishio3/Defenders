using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    [CreateAssetMenu(fileName = "NewBulletAsset", menuName = "Bullet/Create Bullet Asset")]

    public class BulletAsset : ScriptableObject
    {
        public enum BULLET_TYPE
        {
            LASER,
            BIG,
            NORMAL,
            SMALL
        }

        public BULLET_TYPE type;

    }
}