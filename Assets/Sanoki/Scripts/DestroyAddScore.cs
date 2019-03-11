using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAddScore : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyImage", 0.5f);
    }

   void DestroyImage()
    {
        Destroy(gameObject);
    }
}
