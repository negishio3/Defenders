using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using UnityEngine.UI;

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

    public static int[] playerNumber = { 1, -1, -1, -1 };//エントリーされているコントローラーの番号を保存するやつ

    public static int playerCount = 0;//エントリー完了済みコントローラーのカウント

    public GameObject[] stage;//各キャラごとの地面

    public GameObject[] playerPre;//各キャラのプレハブ

    GameObject[] player_Obj = new GameObject[4];//生成されたプレハブを保存しとくやつ

    SceneFader sceneFader;//シーンフェーダー

    public GameObject gameStart_Image;//ゲームスタートの操作のテキスト

    public GameObject[] playerCam;//各キャラごとのカメラ

    public Image[] joinIcon;//各プレイヤーのエントリー操作Image
    public Sprite[] buttonSprite;//↑用のスプライト

    // Start is called before the first frame update
    void Start()
    {
        playerCount = 0;//エントリーのカウントをリセット
        sceneFader = FindObjectOfType<SceneFader>();//SceneFaderの読み込み
        for (int i = 0; i < playerNumber.Length; i++)//すべてのエントリーをAnyに
        {
            playerNumber[i] = -1;
        }
        gameStart_Image.SetActive(false);//ゲームスタートのテキストを非表示に
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
                    AddPlayer((PLAYERNUM)i);//入力のあったコントローラーをエントリー完了に設定
                }
            }
        }
        if (playerCount > 0)
        {
            for(int i = 1; i < 5; i++)
            {
                if (GamePad.GetButtonDown(GamePad.Button.B, (GamePad.Index)i)&&!SceneFader.isFade)//入力のあったコントローラーの
                {
                    RemovePlayer((PLAYERNUM)i);//エントリーを取り消し
                }

                if (GamePad.GetButtonDown(GamePad.Button.Y, (GamePad.Index)i))//エントリー済みのコントローラーいずれかがYボタンを押したなら
                {
                    GameStart((PLAYERNUM)i);//ゲームスタート
                }
            }
        }
    }

    /// <summary>
    /// エントリーの処理をしてくれるやつ
    /// </summary>
    /// <param name="_player">エントリーしたいコントローラーの番号</param>
    void AddPlayer(PLAYERNUM _player)
    {
        bool flg = false;
        int addPlayerNum = playerNumber.Length;

        foreach (int _number in playerNumber)
        {
            if (_number == (int)_player)
            {
                flg = true;

                Debug.Log("登録できません");
            }
        }

        if (!flg)
        {
            for(int i = playerNumber.Length - 1; i >= 0; i--)
            {
                if (playerNumber[i] == -1)
                {
                    addPlayerNum = i;
                    
                }
            }
            playerNumber[addPlayerNum] = (int)_player;

            Debug.Log((PLAYERNUM)playerCount + " player 登録完了");
            Debug.Log(playerNumber[0] + ":" + playerNumber[1] + ":" + playerNumber[2] + ":" + playerNumber[3]);

            Vector3 instansPos = new Vector3(stage[addPlayerNum].transform.position.x, 1.6f, stage[addPlayerNum].transform.position.z);

            player_Obj[addPlayerNum] = Instantiate(
                playerPre[addPlayerNum],
                instansPos,
                Quaternion.identity);

            player_Obj[addPlayerNum].transform.parent = stage[addPlayerNum].transform;

            playerCam[addPlayerNum].SetActive(true);
            joinIcon[addPlayerNum].sprite = buttonSprite[1];

            playerCount++;
        }
        if (playerCount == 1)
        {
            gameStart_Image.SetActive(true);
        }
        Debug.Log("<color=blue>In</color>" + "playerCount: "+ playerCount);
    }

    void RemovePlayer(PLAYERNUM _player)
    {
        for (int i = 0; i < playerNumber.Length; i++)
        {
            if ((int)_player == playerNumber[i])
            {
                playerNumber[i] = -1;
                Destroy(player_Obj[i]);
                playerCam[i].SetActive(false);
                joinIcon[i].sprite = buttonSprite[0];
                playerCount--;
            }
        }
        Debug.Log((PLAYERNUM)playerCount + " player 登録解除");
        Debug.Log(playerNumber[0] + ":" + playerNumber[1] + ":" + playerNumber[2] + ":" + playerNumber[3]);
       
        if (playerCount == 0)
        {
            gameStart_Image.SetActive(false);
        }
        Debug.Log("<color=red>OUT</color>" + "playerCount: " + playerCount);
    }

    void GameStart(PLAYERNUM _player)
    {
        foreach(int _number in playerNumber)
        {
            if (_number == (int)_player)
            {
                sceneFader.SceneChange("Game");
            }
        }
    }

}
