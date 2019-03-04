using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{

    public class WeaponMine : MonoBehaviour
    {
        [SerializeField]
        private WeaponMineAsset WMineAsset = null;

        GameObject MinePrefab;
        GameObject PutPoint;

        private int Magazine;


        // Start is called before the first frame update
        void Start()
        {
            PutPoint = transform.Find("PutPoint").gameObject; // 子のPutPointを参照

            MinePrefab = WMineAsset.MinePrefab;
            Magazine = WMineAsset.Magazine;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Z)) // テスト用
            {
                AttackDown();
            }
        }
        public void AttackDown()
        {
            MinePut();
        }
        public virtual void AttackUp()
        {

        }
        public virtual void Attack()
        {

        }
        void MinePut()
        {
            GameObject Bullets = Instantiate(MinePrefab) as GameObject; // 弾を生成

            Bullets.transform.position = PutPoint.transform.position; // 「Muzzle」の位置に移動
        }
    }
}
