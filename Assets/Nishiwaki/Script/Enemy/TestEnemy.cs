using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    public class TestEnemy : MonoBehaviour
    {
        float a;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Laser>())
            {
                Debug.Log("ダメージ");

                a = other.GetComponent<Laser>().Power;
            }
            else if (other.GetComponent<Bullet>())
            {
                Debug.Log("ダメージ");

            }
        }
    }
}
