using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nishiwaki
{
    public class Weapon : MonoBehaviour, iWeapon
    {
        Rigidbody rigid;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public virtual void AttackDown() { }
        public virtual void AttackUp() { }
        public virtual void Attack() { }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                ColorChangeBlue();
            }
            else if (other.gameObject.tag == "Ground")
            { 
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                ColorChangeRed();
            }
        }
        public void ColorChangeRed()
        {
            GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.red);
        }
        public void ColorChangeBlue()
        {
            GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.blue);
        }
    }
}