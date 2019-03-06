using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    public class Turret : MonoBehaviour
    {
        [SerializeField]
        private TurretAsset TurretAsset = null;

        GameObject BulletPrefab; // 弾プレハブ
        GameObject Muzzle; // 発射位置

        float Range; // 射程
        float Rate; // ファイアレート
        int ShotSpeed; // 弾の速度
        float RotaSpeed; // 回転速度
        int HP; // 耐久値
        float CoolTime;

        GameObject target;

        private GameObject nearObj;         //最も近いオブジェクト

        // Start is called before the first frame update
        void Start()
        {
            Muzzle = transform.Find("Muzzle").gameObject; // 子のMuzzleを参照

            Range = TurretAsset.Range;
            Rate = TurretAsset.Rate;
            ShotSpeed = TurretAsset.ShotSpeed;
            HP = TurretAsset.HP;

            //最も近かったオブジェクトを取得
            nearObj = serchTag(gameObject, "Enemy");
        }

        // Update is called once per frame
        void Update()
        {
            //最も近かったオブジェクトを取得
            nearObj = serchTag(gameObject, "Enemy");

            //対象の位置の方向を向く
            transform.LookAt(nearObj.transform);

            //BulletCreate();
        }
        //void BulletCreate()
        //{
        //if (CoolTime <= 0)
        //{
        //    GameObject Bullets = Instantiate(BulletPrefab) as GameObject; // 弾を生成

        //    Bullets.transform.position = Muzzle.transform.position; // 「Muzzle」の位置に移動

        //    Vector3 BulletForce;

        //    BulletForce = this.gameObject.transform.forward * ShotSpeed;

        //    // Rigidbodyに力を加えて発射
        //    Bullets.GetComponent<Rigidbody>().AddForce(BulletForce);

        //    CoolTime = Rate;
        //}
        //}
        //指定されたタグの中で最も近いものを取得
        GameObject serchTag(GameObject nowObj, string tagName)
        {
            float tmpDis = 0;           //距離用一時変数
            float nearDis = 0;          //最も近いオブジェクトの距離
                                        //string nearObjName = "";    //オブジェクト名称
            GameObject targetObj = null; //オブジェクト

            //タグ指定されたオブジェクトを配列で取得する
            foreach (GameObject obs in GameObject.FindGameObjectsWithTag(tagName))
            {
                //自身と取得したオブジェクトの距離を取得
                tmpDis = Vector3.Distance(obs.transform.position, nowObj.transform.position);

                //オブジェクトの距離が近いか、距離0であればオブジェクト名を取得
                //一時変数に距離を格納
                if (nearDis == 0 || nearDis > tmpDis)
                {
                    nearDis = tmpDis;
                    //nearObjName = obs.name;
                    targetObj = obs;
                }
            }
            //最も近かったオブジェクトを返す
            //return GameObject.Find(nearObjName);
            return targetObj;
        }
    }
}