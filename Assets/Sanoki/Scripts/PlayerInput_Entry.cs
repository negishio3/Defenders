using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class PlayerInput_Entry : MonoBehaviour
{
    public int characterNum;// キャラクターの番号
    public GamePad.Index playerNo;// コントローラーの番号
    Vector3 player_Distans;// プレイヤーの進む方向
    Vector3 player_Pos;// プレイヤーの座標
    Vector3 SpawnPos;

    GameObject weapon;// 現在所持している武器

    float speed = 10.0f;// 移動速度

    Color panelColor;

    bool itemFlg = false;

    void Start()
    {
        playerNo = (GamePad.Index)EntrySystem.playerNumber[characterNum - 1];
        if ((int)playerNo == -1)
            gameObject.SetActive(false);
        SpawnPos = transform.position;
        player_Pos = transform.position;// 座標の更新
    }

    // Update is called once per frame
    void Update()
    {
        StickInput();
        ButtonInput();
    }
    // スティックのインプット
    void StickInput()
    {
        var gamepadState = GamePad.GetState(playerNo, false);// コントローラーからの入力を受け取る
        float LStickAxisX = gamepadState.LeftStickAxis.x;// 左スティックの横軸の値
        float LStickAxisY = gamepadState.LeftStickAxis.y;// 左スティックの縦軸の値

        if (LStickAxisX > 0 || LStickAxisX < 0 || LStickAxisY > 0 || LStickAxisY < 0)// 左スティックの入力があるなら
            player_Distans = new Vector3(LStickAxisX, 0, LStickAxisY);// 移動する方向を加算
        else player_Distans = new Vector3(0, 0, 0);// 入力がないなら移動しない

        // スティックの入力を確認
        if (LStickAxisX > 0 || LStickAxisX < 0 || LStickAxisY > 0 || LStickAxisY < 0)
        {
            Vector3 direction = new Vector3(LStickAxisX, 0, LStickAxisY);// スティックの入力
            transform.rotation = Quaternion.LookRotation(direction);//回転
        }

        player_Pos += player_Distans * Time.deltaTime * speed;// 移動
        transform.position = player_Pos;// 座標の更新
    }
    // ボタンのインプット
    void ButtonInput()
    {
        // ダッシュ？
        if (GamePad.GetButtonDown(GamePad.Button.A, playerNo))
        {

        }
        // 攻撃
        if (GamePad.GetButtonDown(GamePad.Button.B, playerNo))
        {
            if (weapon != null)
                weapon.GetComponent<Nishiwaki.iWeapon>().AttackDown();
        }
        // 武器チェンジ
        //if(GamePad.GetButtonDown(GamePad.Button.X, playerNo))
        //{
        //    if (itemFlg)
        //    {

        //    }
        //}
        // アイテム使用？
        if (GamePad.GetButtonDown(GamePad.Button.Y, playerNo))
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (itemFlg && GamePad.GetButtonDown(GamePad.Button.X, playerNo))
        {
            switch (other.tag)
            {
                case "Weapon":
                    weapon = other.gameObject;
                    break;
                default:
                    Debug.LogError("このgameobject.tagは設定されていません");
                    break;
            }
        }
    }
    ~PlayerInput_Entry()
    {
        transform.position = SpawnPos;
    }
}
