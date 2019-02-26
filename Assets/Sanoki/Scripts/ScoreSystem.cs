using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using UnityEngine;

public class ScoreSystem : SingletonMonoBehaviour<ScoreSystem>
{
    private List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            RankingSort(Data.score);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            RankingSave();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            RankingImport();
        }
    }

    /// <summary>
    /// ランキングソートメソッド
    /// </summary>
    /// <param name="score">スコア</param>
    public void RankingSort(int score)
    {
        for (int i = 0; i < Data.ranking.Length; i++)
        {
            if (score > Data.ranking[i])
            {
                int x = Data.ranking[i];
                Data.ranking[i] = score;
                score = x;
            }
        }

        for (int i = 0; i < Data.ranking.Length; i++)
        {
            Debug.Log((i + 1) + "位 : " + Data.ranking[i]);

        }
    }

    /// <summary>
    /// csvファイルからランキングデータを読み込む
    /// </summary>
    public void RankingImport()
    {
        TextAsset csvFile = Resources.Load("Data/RnakingData") as TextAsset; /* Resouces/Data下のCSV読み込み */
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() > -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(',')); // リストに入れる
        }
        
        for(int i=0;i<Data.ranking.Length;i++)
        {
            Data.ranking[i] = int.Parse(csvDatas[i][0]);
            //Debug.Log((i+1)+"位 : "+Data.ranking[i]);
        }


    }

    /// <summary>
    /// csvファイルにランキングデータを保存
    /// </summary>
    public void RankingSave()
    {
        string[] ranking = new string[Data.ranking.Length];
        for(int i=0;i<Data.ranking.Length; i++)
        {
            ranking[i] = Data.ranking[i].ToString();
        }
        StreamWriter sw = new StreamWriter(Application.dataPath+@"\Resources\Data\RnakingData.csv", false, Encoding.GetEncoding("Shift_JIS"));
        
        for (int i = 0; i < ranking.Length; i++)
        {
            sw.WriteLine(ranking[i]);
        }

        sw.Flush();
        sw.Close();

        Debug.Log("セーブしました。");
    }
}
