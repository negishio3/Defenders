using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    public class WeaponLaser : Weapon
    {
        [SerializeField]
        private WeaponLaserAsset WLAsset = null;

        GameObject Laser;

        private RaycastHit hit; //ヒットしたオブジェクト情報
        private float LaserRange; // レーザーの長さ
        private Ray ray; //レイ
        private bool flg = false; //射撃の有無

        private float Range; // Assetから取得した射程
        private float OverHeat; // Assetから取得した
        private float CoolTime; // Assetから取得した

        // Start is called before the first frame update
        void Start()
        {
            //Laser = transform.GetChild(0).gameObject; // 子になっている弾を探す
            Laser = transform.Find("LaserBulletMuzzle").gameObject; // 子のMuzzleを参照
            Range = WLAsset.Range;
        }

        // Update is called once per frame
        void Update()
        {
            //レイの設定
            ray = new Ray(transform.position, transform.forward);

            //レイキャスト（原点, 飛ばす方向, 衝突した情報, 長さ）
            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //レイを可視化
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance,
                    Color.yellow);
                if (hit.distance <= Range) LaserRange = hit.distance + 0.5f;
            }
            else
            {
                LaserRange = Range;
            }

            if (Input.GetKeyDown(KeyCode.Z)) flg = true; // テスト用
            else if (Input.GetKeyUp(KeyCode.Z))  flg = false; // テスト用

            if (Input.GetKey(KeyCode.RightArrow)) // テスト用
            {
                transform.Rotate(new Vector3(0, 1, 0));
            }

            if (flg) Laser.transform.localScale = new Vector3(1, 1, LaserRange); 
            else
            {
                LaserRange = 0.5f;
                Laser.transform.localScale = new Vector3(1, 1, LaserRange);
            }
        }
        public override void AttackDown()
        {
            flg = true;
        }
        public override void AttackUp()
        {
            flg = false;
        }
    }
}
