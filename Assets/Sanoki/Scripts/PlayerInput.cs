using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class PlayerInput : MonoBehaviour
{
    public GamePad.Index playerNo;// コントローラーの番号
    Vector3 player_Distans;// プレイヤーの進む方向
    Vector3 player_Pos;// プレイヤーの座標

    GameObject weapon;

    float speed = 10.0f;

    bool pauseFlg = false;

    void Start()
    {
        player_Pos = transform.position;// 座標の更新
    }

    // Update is called once per frame
    void Update()
    {
        if (GamePad.GetButtonDown(GamePad.Button.Start,playerNo)) pauseFlg = !pauseFlg;
        if (pauseFlg) return;
        StickInput();
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
        if (GamePad.GetState(playerNo, false).A)
        {

        }
        // 攻撃
        if(GamePad.GetState(playerNo, false).B)
        {
            
        }
        // 武器チェンジ？
        if(GamePad.GetState(playerNo, false).X)
        {
        }
        // アイテム使用？
        if(GamePad.GetState(playerNo, false).Y)
        {

        }
    }

    
}
