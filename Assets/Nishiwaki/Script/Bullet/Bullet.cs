using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField]
        private BulletAsset bAsset = null;

        float Speed; //スピード
        float Power; //パワー

        // Start is called before the first frame update
        void Start()
        {
            Speed = bAsset.Speed;
            Power = bAsset.Power;
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
