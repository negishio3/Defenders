using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
public class Test_Item : MonoBehaviour,I_Item
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UseItem(PlayerInput _player)
    {
        Debug.Log("アイテム使ったよ:" + gameObject.name);
        Destroy(gameObject);
    }
}
