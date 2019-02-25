using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    public class TestEnemy : MonoBehaviour
    {
        float power;
        // Start is called before the first frame update
        void Start()
        {
            var laser = new Laser();
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Laser>())
            {
                power = other.GetComponent<Laser>().power;

                Debug.Log(power + "ダメージ");
            }
            else if (other.GetComponent<Bullet>())
            {
                power = other.GetComponent<Bullet>().power;

                Debug.Log(power + "ダメージ");
            }
        }
    }
}
