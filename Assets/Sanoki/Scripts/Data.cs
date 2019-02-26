using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static int score = 999999;// スコア

    public const float COLONY_VALUE_MAX = 10000;//拠点体力初期値
    public static float colonyValue = 8000;// 拠点耐久値

    public static int waveCout = 12;// 最終Wave数

    public static int[] ranking = new int[3];// ランキング用スコア配列

    public static bool pauseFlg = false;// ポーズ用のフラグ
    
}

