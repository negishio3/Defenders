using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class GameOverSystem : MonoBehaviour
{
    SceneFader sceneFader;
    // Start is called before the first frame update
    void Start()
    {
        sceneFader = FindObjectOfType<SceneFader>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 1; i < 5; i++)
        {
            if (GamePad.GetButtonDown(GamePad.Button.Y, (GamePad.Index)i))
            {
                sceneFader.SceneChange("Title");
            }
        }
    }
}
