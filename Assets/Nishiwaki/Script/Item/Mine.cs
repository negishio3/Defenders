using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    public class Mine : MonoBehaviour
    {
        [SerializeField]
        private MineAsset MineAsset = null;

        public GameObject ExplosionPrefab;

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
            Power = MineAsset.Power;
            Range = MineAsset.Range;

            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.radius = Range; // SphereColliderのradiusに範囲を代入
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<TestEnemy>()) // 「TestEnemy」をティウスが作ったやつに変える
            {
                Debug.Log("敵が範囲に入った");

                GameObject Explosion = Instantiate(ExplosionPrefab) as GameObject; // 爆発を生成

                Destroy(gameObject);
            }
        }
    }
}
