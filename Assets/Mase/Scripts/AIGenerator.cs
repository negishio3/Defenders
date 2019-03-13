using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGenerator : MonoBehaviour
{
    public GameObject obj;//生成するオブジェクト
    private float lastAttackTime;
    private float attackInterval = 1f;
    public float timeout;
    private int Counter;
    public bool Create;
    //private bool CountTimer;
    //public float CountTime;
    public int CreateCount;

    // Start is called before the first frame update
    void Start()
    {

        Create = true;
        //CountTimer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Create)
        {
            if (Time.time > lastAttackTime + attackInterval)
            {
                Debug.Log("きた");

                Instantiate(obj, (this.transform.position), Quaternion.identity);

                lastAttackTime = Time.time;

                Counter++;
            }

        }

        if (Counter >= CreateCount)
        {
            Create = false;
        }


        //if (GameSystem.Instance.)
        //{

        //}
        //if (CountTime >= 5)
        //{
        //    CountTimer = true;
        //}

        //CountTime -= Time.deltaTime;

        Debug.Log(Counter);
        //Debug.Log(CountTime);
    }
}
