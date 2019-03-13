using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public int characterNum;// キャラクターの番号
    public GamePad.Index playerNo;// コントローラーの番号
    Vector3 player_Distans;// プレイヤーの進む方向
    Vector3 player_Pos;// プレイヤーの座標

    GameObject weapon;// 現在所持している武器

    float speed = 10.0f;// 移動速度

    Color panelColor;//ポーズ時に表示されるパネルの色

    bool freezeFlg = false;//アイテムに触れているかの判定

    bool pushFlg = false;//長押しの判定

    float pushTime = 0.0f;//ボタン長押し時間の取得

    float player_HP;
    const float PLAYER_HPMAX = 1000.0f;

    public Image gauge_HP;
    public GameObject charactorModel;

    //public Text time;


    void Start()
    {
        player_HP = PLAYER_HPMAX;
        pushFlg = false;//アイテム使用中でなくする
        playerNo = (GamePad.Index)EntrySystem.playerNumber[characterNum - 1];//このキャラクターを使用するコントローラーの番号を取得
        if ((int)playerNo == -1)//もしAnyなら
            gameObject.SetActive(false);//このキャラクターを非表示に
        player_Pos = transform.position;// 座標の更新
    }

    // Update is called once per frame
    void Update()
    {
        //time.text = "Time:" + (int)pushTime;
        ButtonInput();//プレイヤーのボタンの入力を取得
        if (GamePad.GetButtonDown(GamePad.Button.Start, playerNo))//スタートボタンを押したなら
        {
            float alpha = Data.pauseFlg ? 0 : 0.5f;//アルファ値の変更
            panelColor = new Color(0, 0, 0, alpha);//パネルカラーの変更
            Data.pauseFlg = !Data.pauseFlg;//ポーズフラグを切り替え
            //Debug.Log(Data.pauseFlg);
        }
        if (Data.pauseFlg || pushFlg) return;//ポーズ中なら入力をぶっち
        if (player_HP <= 0) return;
        StickInput();//スティックのインプットを取得
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
            charactorModel.transform.rotation = Quaternion.LookRotation(direction);//回転
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
            PlayerDamage(1000);
        }
        // 攻撃
        if (GamePad.GetButtonDown(GamePad.Button.B, playerNo))
        {
            if (weapon != null)
            {
                weapon.GetComponent<Nishiwaki.iWeapon>().AttackDown();//現在所持している武器の攻撃関数を呼び出し
                weapon.GetComponent<Nishiwaki.iWeapon>().Attack();//現在所持している武器の攻撃関数を呼び出し
            }
        }
        if (GamePad.GetButtonUp(GamePad.Button.B, playerNo))
        {
            if (weapon != null)
            {
                weapon.GetComponent<Nishiwaki.iWeapon>().AttackUp();//現在所持している武器の攻撃関数を呼び出し
                weapon.GetComponent<Nishiwaki.iWeapon>().AttackUp();//現在所持している武器の攻撃関数を呼び出し
            }
        }
        // 武器チェンジ
        //if(GamePad.GetButtonDown(GamePad.Button.X, playerNo))
        //{
        //    if (itemFlg)
        //    {

        //    }
        //}
        // アイテム使用？
        if (GamePad.GetButton(GamePad.Button.Y, playerNo) && freezeFlg)
        {
            pushFlg = true;//アイテム使用中に設定
            ButtonPush();//ボタンを長押しする
        }
        if (GamePad.GetButtonUp(GamePad.Button.Y, playerNo))
        {
            pushTime = 0.0f;//長押し時間をリセット
            pushFlg = false;//指を離した
        }
    }

    private void OnTriggerStay(Collider other)
    {
        switch (other.tag)//触れているアイテムのタグ
        {
            case "Weapon"://武器なら
                if (GamePad.GetButtonDown(GamePad.Button.X, playerNo))
                {
                    BoxCollider bc;//BoxColliderを取得
                    Rigidbody rb;//Rigidbodyを取得

                    if (weapon != null)//もし武器を持っている状態なら
                    {
                        rb = weapon.GetComponent<Rigidbody>();//Rigidbodyコンポーネントを取得
                        rb.useGravity = true;//useGravityをtrueに
                        rb.constraints = RigidbodyConstraints.None;
                        weapon.transform.localPosition = new Vector3(1.0f, 0.0f, 0.0f);//武器を捨てる
                        weapon.transform.parent = null;//親子関係の切り離し
                    }
                    weapon = other.gameObject;
                    weapon.tag = "MyWeapon";
                    bc = weapon.GetComponent<BoxCollider>();
                    rb = weapon.GetComponent<Rigidbody>();
                    bc.isTrigger = true;
                    rb.useGravity = false;
                    rb.constraints = RigidbodyConstraints.FreezeAll;
                    other.transform.parent = charactorModel.transform;
                    other.transform.localPosition = Vector3.zero;
                    other.transform.localRotation = Quaternion.identity;
                    freezeFlg = false;
                }
                break;
            case "Item":
                freezeFlg = true;//アイテムに触れている判定
                if (pushTime >= 3.0f)
                {
                    //アイテムを使用する処理
                    other.GetComponent<I_Item>().UseItem(this);
                    freezeFlg = false;
                }
                break;
            case "Colony":
                freezeFlg = true;
                if (pushTime >= 5.0f)
                {
                    ColonySystem.Instance.ColonyRecovery(500);
                    pushTime = 0.0f;
                    freezeFlg = false;
                }
                break;
            case "DownPlayer":
                freezeFlg = true;
                if (pushTime >= 3.0f)
                {
                    PlayerInput pi = other.GetComponent<PlayerInput>();
                    pi.PlayerRecovery(500);
                    other.tag = "Player";
                    pushTime = 0.0f;
                    freezeFlg = false;
                }
                break;
            case "MyWeapon":
                break;
            default:
                Debug.LogError("このオブジェクトのtagは設定されていません[" + other.tag + "]");
                break;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        freezeFlg = false;
    }

    void OnGUI()
    {
        GUI.color = panelColor;//設定されている色にする
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Texture2D.whiteTexture);//が面大の幕を表示
    }

    void ButtonPush()
    {
        pushTime += Time.deltaTime;
    }

    public void PlayerDamage(int damage)
    {
        player_HP -= damage;
        if (player_HP <= 0)
        {
            player_HP = 0;
            gameObject.tag = "DownPlayer";
        }
        gauge_HP.rectTransform.localScale = new Vector2(player_HP / PLAYER_HPMAX, 1.0f);
        GaugeColorChanger gcc = gauge_HP.GetComponent<GaugeColorChanger>();
        gcc.ColorChange();
    }

    public void PlayerRecovery(int recoveryValue)
    {
        player_HP += recoveryValue;
        if (player_HP > PLAYER_HPMAX)
            player_HP = PLAYER_HPMAX;
        gauge_HP.rectTransform.localScale = new Vector2(player_HP / PLAYER_HPMAX, 1.0f);
        GaugeColorChanger gcc = gauge_HP.GetComponent<GaugeColorChanger>();
        gcc.ColorChange();
    }
}
