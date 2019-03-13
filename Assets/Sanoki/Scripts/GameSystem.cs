using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSystem : SingletonMonoBehaviour<GameSystem>
{ 
    int score;// 倒した敵の数
    int waveCout;// ウェーブのカウント
    SceneFader sceneFader;// フェードのクラス

    public Image timer_Image;// タイマーのイメージ画像
    TimerColor timerColor;// タイマーの色を変えるクラス

    public Text timer_Text;// タイマーの時間表示

    float count_Time;// 制限時間
    const float TIME_FULL = 120.0f;// ゲームの開始時間

    public Text score_Text;// スコアのテキスト
    public GameObject addScore_Prefab;// 加算スコアテキストのプレハブ
    Text addScore_Text;//↑のテキストコンポーネント

    public Text wave_Text; // ウェーブのテキスト

    public GameObject wave_Obj;
    bool waveFlg;

    public GameObject[] playerPre;
    public GameObject[] respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
        for(int i = 0; i < EntrySystem.playerNumber.Length; i++)
        {
            if (EntrySystem.playerNumber[i] != -1)
            {
                Instantiate(playerPre[i], respawnPoint[i].transform.position, Quaternion.identity);
            }
        }
        waveFlg = false;
        
        timerColor = timer_Image.GetComponent<TimerColor>();// タイマーについているTimerColorコンポーネントを取得
        ResetOption();//設定を初期化
        sceneFader = FindObjectOfType<SceneFader>();// SceneFaderを取得
        WaveCount();
        Data.pauseFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Data.pauseFlg) return;//ポーズ中なら以降の処理を飛ばす

        GameTimeCount();//タイムのカウント
        if (ColonySystem.colony_HP <= 0)
            GameOver();

        if (Input.GetKeyDown(KeyCode.Return))
            WaveCount();
    }

    /// <summary>
    /// スコアの加算処理
    /// </summary>
    /// <param name="addScore">加算するスコア</param>
    public void ScoreCount(int addScore)
    {
        GameObject addScore_Obj = Instantiate(addScore_Prefab, Vector3.zero, Quaternion.identity);//加算スコアプレハブの生成
        addScore_Obj.transform.SetParent(score_Text.transform);//親子関係を設定
        addScore_Text = addScore_Obj.GetComponent<Text>();//Textコンポーネントを取得
        addScore_Text.text = "+" + addScore;//加算スコアの表示
        score += addScore;//実際にスコアに加算
        score_Text.text = "score:" + (score);//スコアを表示
        Data.destroyEnemyCount++;//撃破数を加算
    }

    /// <summary>
    /// ウェーブのカウント
    /// </summary>
    public void WaveCount()
    {
        if (waveFlg) return;
        waveFlg = true;
        waveCout++;//ウェーブの加算
        //wave_Text.text = waveCout+"WAVE";//表示
        wave_Obj.SetActive(true);
        Text wavetext = wave_Obj.GetComponentInChildren<Text>();
        wave_Text.text = wavetext.text = waveCout + "WAVE";//表示
    }

    /// <summary>
    /// ゲーム時間のカウント
    /// </summary>
    void GameTimeCount()
    {
        count_Time -= Time.deltaTime;//タイムの減算
        timer_Image.fillAmount = count_Time / TIME_FULL;//タイマーゲージを減らす
        timer_Text.text = "" + (int)count_Time;//時間の表示
        timerColor.ColorChange();//カラーを変更

        if (count_Time < 0)//0秒以下になったとき
        {
            Data.pauseFlg = true;//ポーズ中に変更
            GameClear();//ゲームクリアの処理を呼び出し
        }
    }



    /// <summary>
    /// ゲームクリアの処理を行う
    /// </summary>
    void GameClear()
    {
        //データの更新
        Data.colonyValue = ColonySystem.colony_HP;
        Data.waveCout = waveCout;
        Data.score = score;

        sceneFader.SceneChange("Result");//リザルトシーンに遷移
    }

    /// <summary>
    /// ゲームオーバーの処理を行う
    /// </summary>
    void GameOver()
    {
        Data.pauseFlg = true;
        sceneFader.SceneChange("GameOver");//ゲームオーバーシーン
    }

    /// <summary>
    /// ゲームの初期設定
    /// </summary>
    void ResetOption()
    {
        score = 0;//スコアのリセット
        waveCout = 0;//ウェーブカウントのリセット
        count_Time = TIME_FULL;//制限時間のリセット
        timer_Text.text = "" + (int)count_Time;//制限時間の表示をリセット
        score_Text.text = "score:0";//スコアの表示をリセット
    }

    public void ChangeFlg()
    {
        waveFlg = false;
    }
}
