using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIGenerator : MonoBehaviour
{
    public GameObject obj;//生成するオブジェクト
    private float lastAttackTime;
    private float attackInterval = 1f;
    public float timeout;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySummons());
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > lastAttackTime + attackInterval)
        {
            Instantiate(obj, (this.transform.position), Quaternion.identity);

            lastAttackTime = Time.time;
        }
    }

    //AI召喚
    IEnumerator EnemySummons()
    {

        yield return new WaitForSeconds(timeout);
    }
}
