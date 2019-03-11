using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sagittarius : MonoBehaviour
{
    private Transform colony;//向く対象
    public GameObject bullet;
    public float bulletspeed;
    public Transform muzzle;
    private float lastAttackTime;
    private float attackInterval = 2f;


    // Start is called before the first frame update
    void Start()
    {
        // 始めにプレイヤーの位置を取得できるようにする
        colony = GameObject.FindWithTag("Colony").transform;

    }

    // Update is called once per frame
    void Update()
    {
        Quaternion targetRotation =
            Quaternion.LookRotation(colony.position - transform.position);
        transform.rotation =
            Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);

        if (Time.time> lastAttackTime + attackInterval)
        {
            // 弾丸の複製
            GameObject bullets = Instantiate(bullet) as GameObject;

            Vector3 force;

            force = this.gameObject.transform.forward * bulletspeed;

            // Rigidbodyに力を加えて発射
            bullets.GetComponent<Rigidbody>().AddForce(force);

            // 弾丸の位置を調整
            bullets.transform.position = muzzle.position;

           lastAttackTime = Time.time;
        }
    }
}
