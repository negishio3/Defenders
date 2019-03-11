using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    public NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        //追う？向かう？対象の位置を取得
        target = GameObject.FindWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.destination = target.position;
    }
}
