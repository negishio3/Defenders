using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;

public class ResultSystem : MonoBehaviour
{
    public Image backImage_Panel;//背景のパネル
    Color panelColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);// パネルの色情報

    public Text currentScore;// 現在スコアのテキスト
    public GameObject[] addScore_GameObj = new GameObject[3] ;// 加算スコアの詳細のテキスト
    Text[] addScore_Text = new Text[3];//テキスト情報を入れる

    public GameObject[] ranking_GameObj = new GameObject[3];//ランキング用のテキスト
    public Text[] ranking_Text = new Text[3];//スコアのテキスト情報を入れる

    public GameObject highScore;// ハイスコアのテキスト

    int waveScore = 100;// wave毎のスコア

    int colonyValue;// 拠点の耐久値
    int colonyScore = 100;//倍率

    float score;// 加算前のスコア

    bool isFade = false;// フェード中のフラグ
    bool isResult = false;// リザルト中のフラグ
    bool isRanking = false;// ランキング処理中のフラグ

    public GameObject Ybutton;// ボタンのイメージ

    public GameObject resultPanel;// リザルトのパネル
    public GameObject rankingPnael;// ランキングのパネル

    public Color rankingColor;

    SceneFader sceneFader;

    public enum ResultState
    {
        FADE,
        RESULT,
        RANKING
    }
    ResultState resultState;

    // Start is called before the first frame update
    void Start()
    {
        sceneFader = FindObjectOfType<SceneFader>();
        for (int i = 0; i < addScore_GameObj.Length; i++)
        { 
            addScore_Text[i] = addScore_GameObj[i].GetComponent<Text>();// スコアの詳細のテキストコンポーネントを取得
            ActivStateChenge(addScore_GameObj[i]);// 非表示に切り替え
        }

        score = 0;//現在スコアをリセット

        currentScore.text = "Score : " + (int)score;// スコアのテキストを更新
        addScore_Text[0].text = "撃破 : " + Data.score+"体";// 撃破数のテキストを更新

        colonyValue = (int)(Data.colonyValue / Data.COLONY_VALUE_MAX * 100);// 拠点の耐久値を計算
        addScore_Text[1].text = "拠点(耐久値) : " 
            + colonyValue + "%";//  耐久値のテキストを更新

        addScore_Text[2].text = "WAVE数 : " + Data.waveCout;// Wavw数のテキストを更新
        resultState = ResultState.FADE;
        isResult = true;// 現在の進行状態をリザルト中に変更
        ScoreSystem.Instance.RankingImport();
    }

    // Update is called once per frame
    void Update()
    {
        if (!SceneFader.isFade && !isFade)
        { 
            StartCoroutine(PanelFade(1.0f));//パネルのフェードを呼び出す
        }
        if (!isResult && GamePad.GetButtonDown(GamePad.Button.Y, GamePad.Index.Any) && resultState == ResultState.RESULT)
        {
            ActivStateChenge(resultPanel);
            StartCoroutine(RankingProgram());
        }
        if (!isRanking && GamePad.GetButtonDown(GamePad.Button.Y, GamePad.Index.Any) && resultState == ResultState.RANKING)
        {
            sceneFader.SceneChange("Entry");
        }
    }

    /// <summary>
    /// GameObjectの表示・非表示を切り替えてくれるやつ
    /// </summary>
    /// <param name="gameObj">切り替えたいGameobject</param>
    void ActivStateChenge(GameObject gameObj)
    {
        gameObj.SetActive(!gameObj.activeSelf);// 引数のオブジェクトの表示非表示を切り替え
    }

    /// <summary>
    /// スコア加算コルーチン
    /// </summary>
    /// <param name="addPoint">加算するポイント</param>
    /// <returns></returns>
    public IEnumerator AddScore(int addPoint)
    {
        float startScore = score;// 加算の開始位置を現在のスコアに
        float endScore = score + addPoint;//加算完了位置を計算

        float t = 0.0f;// 時間経過をリセット

        while (t < 1.0f)
        {
            t += Time.deltaTime;//時間経過の計算
            score = Mathf.Lerp(startScore, endScore, t);// スコアを少しづつ加算
            currentScore.text = "Score : " + (int)score;// 表示
            yield return null;
        }
    }

    /// <summary>
    /// フェード用コルーチン
    /// </summary>
    /// <param name="seconds">何秒かけてフェードさせるのか</param>
    /// <returns></returns>
    public IEnumerator PanelFade(float seconds)
    {
        ActivStateChenge(resultPanel);

        isFade = true;
        float t = 0.0f;//時間をリセット
        
        //Debug.Log("フェードイン");
        while (t < 1.0f)//時間になるまで繰り返す
        {
            t += Time.deltaTime / seconds;//時間経過
            panelColor.a = Mathf.Lerp(0.0f, 0.4f, t);//アルファ値を徐々に上げる
            backImage_Panel.color = panelColor;
            yield return null;//1フレーム待つ
        }

        StartCoroutine(ResultProgram());// リザルトを開始
    }
    /// <summary>
    /// リザルトのコルーチン
    /// </summary>
    /// <returns></returns>
    public IEnumerator ResultProgram()
    {
        resultState = ResultState.RESULT;
        ActivStateChenge(addScore_GameObj[0]);
        StartCoroutine(AddScore(Data.score * 100));// 撃破数から計算して加算

        yield return new WaitForSeconds(1.5f);

        ActivStateChenge(addScore_GameObj[1]);
        StartCoroutine(AddScore(colonyValue * colonyScore));// 残り体力分加算

        yield return new WaitForSeconds(1.5f);

        ActivStateChenge(addScore_GameObj[2]);
        StartCoroutine(AddScore(waveScore * Data.waveCout));// wave数から計算して加算

        yield return new WaitForSeconds(1.5f);

        if (Data.ranking[0] < score)
        {
            ActivStateChenge(highScore);// ハイスコアを更新していたら表示
        }

        if(Data.ranking[2] < score)
        {
            ScoreSystem.Instance.RankingSort((int)score);// ランキングの更新
            ScoreSystem.Instance.RankingSave();//ランキングデータを保存
        }

        ActivStateChenge(Ybutton);// 操作の表示

        isResult = false;// 現在の進行状態を変更
    }

    /// <summary>
    /// ランキングデータ表示
    /// </summary>
    /// <returns></returns>
    public IEnumerator RankingProgram()
    {
        isRanking = true;
        resultState = ResultState.RANKING;
        ActivStateChenge(rankingPnael);

        for (int i = 0; i < ranking_GameObj.Length; i++)
        {
            ranking_Text[i].text = "" + Data.ranking[i];
            if (i == ScoreSystem.Instance.RankingNum())
            {
                ranking_Text[i].color = rankingColor;
            }
            ActivStateChenge(ranking_GameObj[i]);

            yield return new WaitForSeconds(1.0f);
        }

        DataReset();

        isRanking = false;
    }

    /// <summary>
    /// データのリセット
    /// </summary>
    void DataReset()
    {
        Data.score = 0;
        Data.colonyValue = 0;
        Data.waveCout = 0;
    }
}