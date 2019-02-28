using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    public class WeaponMelee : MonoBehaviour, iWeapon
    {
        [SerializeField]
        private WeaponMeleeAsset WMAsset = null;

        float Power; //パワー

        // Start is called before the first frame update

        public float power
        {
            get { return Power; }
        }

        void Start()
        {
            Power = WMAsset.Power;
        }

        // Update is called once per frame
        void Update()
        {

        }
        void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<TestEnemy>()) // 「TestEnemy」をティウスが作ったやつに変える
            {
                Debug.Log("剣当たったよ");
            }
        }

        public virtual void AttackDown()
        {

        }
        public virtual void AttackUp()
        {

        }
        public virtual void Attack()
        {

        }
    }
}
