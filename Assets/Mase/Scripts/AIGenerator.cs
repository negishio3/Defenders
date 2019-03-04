using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGenerator : MonoBehaviour
{
    public GameObject obj;//生成するオブジェクト
    private float lastAttackTime;
    private float attackInterval = 2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastAttackTime + attackInterval)
        {
            Instantiate(obj,(this.transform.position), Quaternion.identity);

            lastAttackTime = Time.time;
        }
    }
}
