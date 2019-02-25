using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    public class WeaponRanged : MonoBehaviour, iWeapon
    {
        [SerializeField]
        private WeaponRangedAsset RAsset = null;

        public GameObject BulletPrefab;
        GameObject Muzzle;

        // Start is called before the first frame update
        void Start()
        {
            Muzzle = transform.Find("Muzzle").gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Z))
            {
                Attack();
            }
        }
        public virtual void AttackDown()
        {

        }
        public virtual void AttackUp()
        {

        }
        public void Attack()
        {
            BulletCreate();
        }
        void BulletCreate()
        {
            GameObject Bullets = Instantiate(BulletPrefab) as GameObject;

            Bullets.transform.position = Muzzle.transform.position;
        }
    }
}
