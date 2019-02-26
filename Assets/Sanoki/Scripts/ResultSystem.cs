using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GamepadInput;

public class ResultSystem : MonoBehaviour
{
    public Image panel;
    Color panelColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);// パネルの色情報

    public Text currentScore;// 現在スコア
    public GameObject[] addScore_GameObj = new GameObject[3] ;
    Text[] addScore_Text = new Text[3];

    public Text[] scores = new Text[3];

    public GameObject highScore;

    int waveScore = 100;// wave毎のスコア

    float score;
    public int addPoint;

    bool isFade = false;
    bool isResult = false;

    public GameObject Ybutton;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < addScore_GameObj.Length; i++)
        { 
            addScore_Text[i] = addScore_GameObj[i].GetComponent<Text>();
            ActivStateChenge(addScore_GameObj[i]);
        }

        score = 0;

        currentScore.text = "Score : " + (int)score;
        addScore_Text[0].text = "撃破 : " + Data.score;
        addScore_Text[1].text = "拠点(耐久値) : " 
            + (int)(Data.colonyValue / Data.COLONY_VALUE_MAX * 100) + "%";
        addScore_Text[2].text = "WAVE数 : " + Data.waveCout;
        isResult = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFade)
            StartCoroutine(PanelFade(1.0f));
    }

    /// <summary>
    /// GameObjectの表示・非表示を切り替えてくれるやつ
    /// </summary>
    /// <param name="gameObj">切り替えたいGameobject</param>
    void ActivStateChenge(GameObject gameObj)
    {
        gameObj.SetActive(!gameObj.activeSelf);
    }

    /// <summary>
    /// スコア加算コルーチン
    /// </summary>
    /// <param name="addPoint">加算するポイント</param>
    /// <returns></returns>
    public IEnumerator AddScore(int addPoint)
    {
        float startScore = score;
        float endScore = score + addPoint;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime;
            score = Mathf.Lerp(startScore, endScore, t);
            currentScore.text = "Score : " + (int)score;
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
        isFade = true;
        float t = 0.0f;//時間をリセット
        
        //Debug.Log("フェードイン");
        while (t < 1.0f)//時間になるまで繰り返す
        {
            t += Time.deltaTime / seconds;//時間経過
            panelColor.a = Mathf.Lerp(0.0f, 0.4f, t);//アルファ値を徐々に上げる
            panel.color = panelColor;
            yield return null;//1フレーム待つ
        }

        StartCoroutine(ResultProgram());
    }
    public IEnumerator ResultProgram()
    {
        ActivStateChenge(addScore_GameObj[0]);
        StartCoroutine(AddScore(Data.score));

        yield return new WaitForSeconds(1.5f);

        ActivStateChenge(addScore_GameObj[1]);
        StartCoroutine(AddScore((int)Data.colonyValue));

        yield return new WaitForSeconds(1.5f);

        ActivStateChenge(addScore_GameObj[2]);
        StartCoroutine(AddScore(waveScore * Data.waveCout));

        yield return new WaitForSeconds(1.5f);

        if (Data.ranking[0] <= score)
        {
            ActivStateChenge(highScore);
        }

        if(Data.ranking[2] <= score)
        {
            ScoreSystem.Instance.RankingSort((int)score);
        }


    }
}
