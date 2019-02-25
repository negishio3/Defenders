using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private BulletAsset BAsset = null;

        float Speed; //スピード
        float Power; //パワー

        public float power
        {
            get { return Power; }
        }
        public float speed
        {
            get { return Speed; }
        }


        // Start is called before the first frame update
        void Start()
        {
            Speed = BAsset.Speed;
            Power = BAsset.Power;
        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnTriggerEnter(Collider other)
        {
            Debug.Log("弾当たったよ");
        }
    }
}
