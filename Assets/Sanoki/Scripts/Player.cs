using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class Player : MonoBehaviour
{
    public GamePad.Index playerNo;// コントローラーの番号
    Vector3 player_Distans;// プレイヤーの進む方向
    Vector3 player_Pos;// プレイヤーの座標

    float speed = 10.0f;

    bool pauseFlg = false;

    void Start()
    {
        player_Pos = transform.position;// 座標の更新
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) pauseFlg = !pauseFlg;
        if (pauseFlg) return;
        StickInput();
    }

    void StickInput()
    {
        var gamepadState = GamePad.GetState(playerNo, false);// コントローラーからの入力を受け取る
        float LStickAxisX = gamepadState.LeftStickAxis.x;// 左スティックの横軸の値
        float LStickAxisY = gamepadState.LeftStickAxis.y;// 左スティックの縦軸の値

        if (LStickAxisX > 0 || LStickAxisX < 0 || LStickAxisY > 0 || LStickAxisY < 0)// 左スティックの入力があるなら
            player_Distans = new Vector3(LStickAxisX, 0, LStickAxisY);// 移動する方向を加算
        else player_Distans = new Vector3(0, 0, 0);// 入力がないなら移動しない

        //if (LStickAxisX > 0 || LStickAxisX < 0 || LStickAxisY > 0 || LStickAxisY < 0)
        //{
        //    Vector3 direction = new Vector3(LStickAxisX, 0, LStickAxisY);
        //    transform.rotation = Quaternion.LookRotation(direction);
        //}

        player_Pos += player_Distans * Time.deltaTime * speed;// 移動
        transform.position = player_Pos;// 座標の更新
    }

    void ButtonInput()
    {

    }
}
