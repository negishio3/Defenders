﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    public class WeaponRanged : Weapon
    {
        [SerializeField]
        private WeaponRangedAsset WRAsset = null;

        GameObject BulletPrefab;
        GameObject Muzzle;
        public GameObject Cartridge;
        GameObject CartridgeExit;

        float Rate;
        int Magazine;
        int ShotSpeed;

        float CoolTime;

        // Start is called before the first frame update
        void Start()
        {
            Muzzle = transform.Find("Muzzle").gameObject; // 子のMuzzleを参照
            CartridgeExit = transform.Find("CartridgeExit").gameObject;

            BulletPrefab = WRAsset.BulletPrefab;
            Rate = WRAsset.Rate;
            Magazine = WRAsset.Magazine;
            ShotSpeed = WRAsset.ShotSpeed;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.Z)) // テスト用
            {
                Attack();
            }

            if(CoolTime > 0)
            {
                CoolTime -= Time.deltaTime;
            }
        }
        public override void Attack()
        {
            BulletCreate();
        }
        void BulletCreate()
        {
            if (CoolTime <= 0)
            {
                GameObject Bullets = Instantiate(BulletPrefab) as GameObject; // 弾を生成
                GameObject Cartridges = Instantiate(Cartridge) as GameObject; // 薬莢を生成

                Bullets.transform.position = Muzzle.transform.position; // 「Muzzle」の位置に移動
                Cartridges.transform.position = CartridgeExit.transform.position; // 「CartridgeExit」の位置に移動
                
                Vector3 BulletForce;
                Vector3 CartridgeForce;

                BulletForce = this.gameObject.transform.forward * ShotSpeed;
                CartridgeForce = this.gameObject.transform.right * 100;

                // Rigidbodyに力を加えて発射
                Bullets.GetComponent<Rigidbody>().AddForce(BulletForce);
                Cartridges.GetComponent<Rigidbody>().AddForce(CartridgeForce);

                CoolTime = Rate;
            }
        }
    }
}
