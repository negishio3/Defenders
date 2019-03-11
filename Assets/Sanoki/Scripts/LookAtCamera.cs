using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    GameObject parent_Trans;
    GameObject camera_Trans;
    void Start()
    {
        parent_Trans = transform.parent.gameObject;
        camera_Trans = GameObject.Find("Main Camera");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(camera_Trans.transform.rotation.x, -parent_Trans.transform.rotation.y, 0.0f));
    }
}
