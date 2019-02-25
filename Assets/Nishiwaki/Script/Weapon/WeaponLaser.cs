using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    public class WeaponLaser : MonoBehaviour, iWeapon
    {
        [SerializeField]
        private WeaponLaserAsset LAsset = null;

        //public enum BULLET_TYPE
        //{
        //    LASER,
        //    BIG,
        //    NORMAL,
        //    SMALL
        //}

        //public BULLET_TYPE type;

        GameObject Laser;

        private RaycastHit hit; //ヒットしたオブジェクト情報
        public float Range; // Assetから取得した射程
        public float LaserRange; // レーザーの長さ
        private Ray ray; //レイ
        public float move = 1.0f; // テスト用の武器の回転
        private bool flg = false; //射撃の有無

        // Start is called before the first frame update
        void Start()
        {
            Laser = transform.GetChild(0).gameObject; // 弾を探す
            Range = LAsset.Range;
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

            if (flg) Laser.transform.localScale = new Vector3(1, 1, Range);
            else
            {
                Range = 0.5f;
                Laser.transform.localScale = new Vector3(1, 1, Range);
            }
        }
        public void AttackDown()
        {
            flg = true;
        }
        public void AttackUp()
        {
            flg = false;
        }
        public virtual void Attack() { }
    }
}
