using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private BulletAsset BAsset = null;

        float Power; //パワー

        public float power // 敵にダメージを伝える
        {
            get { return Power; }
        }

        // Start is called before the first frame update
        void Start()
        {
            Power = BAsset.Power;
        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<TestEnemy>()) // 「TestEnemy」をティウスが作ったやつに変える
            {
                Debug.Log("弾当たったよ");
            }
            if(other.tag=="Enemy")
                Destroy(gameObject);
        }
    }
}
