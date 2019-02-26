using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class EntrySystem : SingletonMonoBehaviour<EntrySystem>
{
    public enum PLAYERNUM
    {
        Any,
        ONE,
        TWO,
        THREE,
        FOUR
    }

    public static int[] playerNumber = { 1, 2, 3, 4 };

    int playerCount = 0;

    float[] rotations = { 0, 0, 0, 0 };

    SceneFader sceneFader;

    // Start is called before the first frame update
    void Start()
    {
        sceneFader = FindObjectOfType<SceneFader>();
        for(int i = 0; i < playerNumber.Length; i++)
        {
            playerNumber[i] = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCount < 5)
        {
            for(int i = 1; i < 5; i++)
            {
                if (GamePad.GetButtonDown(GamePad.Button.A, (GamePad.Index)i))
                {
                    AddPlayer((PLAYERNUM)i);
                }
            }
        }
        if (playerCount > 0)
        {
            for(int i = 1; i < 5; i++)
            {
                if (GamePad.GetButtonDown(GamePad.Button.B, (GamePad.Index)i))
                {
                    RemovePlayer((PLAYERNUM)i);
                }

                if (GamePad.GetButtonDown(GamePad.Button.Y, (GamePad.Index)i))
                {
                    GameStart((PLAYERNUM)i);
                }
            }
        }
    }

    void AddPlayer(PLAYERNUM _player)
    {
        bool flg = false;

        foreach(int _number in playerNumber)
        {
            if (_number == (int)_player)
            {
                flg = true;

                Debug.Log("登録できません");
            }
        }

        if (!flg)
        {
            playerNumber[playerCount] = (int)_player;

            Debug.Log((PLAYERNUM)playerCount + " player 登録完了");
            Debug.Log(playerNumber[0] + ":" + playerNumber[1] + ":" + playerNumber[2] + ":" + playerNumber[3]);
            playerCount++;
        }
    }

    void RemovePlayer(PLAYERNUM _player)
    {
        for(int i = 0; i < playerNumber.Length; i++)
        {
            if ((int)_player == playerNumber[i])
            {
                playerNumber[i] = -1;
                playerCount--;
            }
        }

        List<int> array = new List<int>();
        int[] temp = { -1, -1, -1, -1 };

        for(int i = 0; i < playerNumber.Length; i++)
        {
            if(playerNumber[i] != -1)
            {
                array.Add(playerNumber[i]);
            }
        }

        for(int i = 0; i < array.Count; i++)
        {
            temp[i] = array[i];
        }

        for(int i = 0; i < temp.Length; i++)
        {
            playerNumber[i] = temp[i];
            Debug.Log(playerNumber[i]);
        }
    }

    void GameStart(PLAYERNUM _player)
    {
        foreach(int _number in playerNumber)
        {
            if (_number == (int)_player)
            {
                sceneFader.SceneChange("Template");
            }
        }
    }

}
