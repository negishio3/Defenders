using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveChange : MonoBehaviour
{
    public void ActiveFalse()
    {
        GameSystem.Instance.ChangeFlg();
        gameObject.SetActive(false);
    }
}
