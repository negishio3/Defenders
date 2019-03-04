using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    public class MineExplosion : MonoBehaviour
    {
        [SerializeField]
        private MineExplosionAsset MineExplosionAsset = null;

        float Power; // パワー
        float Range; // 範囲

        SphereCollider sphereCollider;

        public float power
        {
            get { return Power; }
        }

        // Start is called before the first frame update
        void Start()
        {
            Power = MineExplosionAsset.Power;
            Range = MineExplosionAsset.Range;

            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.radius = Range; // SphereColliderのradiusに範囲を代入
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<TestEnemy>()) // 「TestEnemy」をティウスが作ったやつに変える
            {
                Debug.Log("爆発");

                //Destroy(gameObject);
            }
        }
    }
}