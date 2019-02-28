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
            if (other.GetComponent<Laser>()) // 相手に「Laser」scriptがついているとき
            {
                power = other.GetComponent<Laser>().power; // script「Laser」のpowerを参照

                Debug.Log(power + "ダメージ");
            }
            else if (other.GetComponent<Bullet>()) // 相手に「Bullet」scriptがついているとき
            {
                power = other.GetComponent<Bullet>().power; // script「Bullet」のpowerを参照

                Debug.Log(power + "ダメージ");
            }
            else if (other.GetComponent<WeaponMelee>()) // 相手に「WeaponMelee」scriptがついているとき
            {
                power = other.GetComponent<WeaponMelee>().power; // script「WeaponMelee」のpowerを参照

                Debug.Log(power + "ダメージ");
            }
        }
    }
}
