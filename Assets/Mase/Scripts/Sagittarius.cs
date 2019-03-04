using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sagittarius : MonoBehaviour
{
    private Transform colony;//向く対象
    public GameObject bullet;
    public float bulletspeed;
    public Transform muzzle;


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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullets = Instantiate(bullet) as GameObject;
            Vector3 force;

            force = this.gameObject.transform.forward * bulletspeed;
        }
    }
}
