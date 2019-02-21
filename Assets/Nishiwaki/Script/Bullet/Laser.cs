using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    public class Laser : MonoBehaviour
    {
        [SerializeField]
        private BulletAsset bAsset = null;

        float Speed; //スピード
        public float Power; //パワー

        // Start is called before the first frame update
        void Start()
        {
            Power = bAsset.Power;
        }
        
        // Update is called once per frame
        void Update()
        {

        }
        void OnTriggerEnter(Collider other)
        {
            Debug.Log("レーザー当たったよ");
        }
    }
}
