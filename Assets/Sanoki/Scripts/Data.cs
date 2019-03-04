using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour
{
    public static int score = 0;// 敵の撃破数スコア

    public const float COLONY_VALUE_MAX = 10000;//拠点体力初期値
    public static float colonyValue = 0;// 拠点耐久値

    public static int waveCout = 0;// 最終Wave数

    public static int[] ranking = new int[3];// ランキング用スコア配列

    public static bool pauseFlg = false;// ポーズ用のフラグ
    
}

