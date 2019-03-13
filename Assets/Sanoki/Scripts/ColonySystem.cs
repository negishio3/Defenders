using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColonySystem : SingletonMonoBehaviour<ColonySystem>
{
    public static int colony_HP;//拠点のHP

    public Image colony_Value_Gauge;//拠点のHPゲージ
    public int damage;//ダメージ値(デバッグ用)

    GaugeColorChanger gcc;//ゲージの色を変化させてくれるやつ


    // Start is called before the first frame update
    void Start()
    {
        gcc = colony_Value_Gauge.GetComponent<GaugeColorChanger>();//GaugeColorChangerの取得
        colony_HP = (int)Data.COLONY_VALUE_MAX;//HPの最大値を取得
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
            ColonyDamage(damage);
        if (Input.GetKeyDown(KeyCode.W))
            ColonyRecovery(damage);
    }

    /// <summary>
    /// Colonyにダメージを与える
    /// </summary>
    /// <param name="damageValue">与えたいダメージ値</param>
    public void ColonyDamage(int damageValue)
    {
        colony_HP -= damageValue;//ダメージ値分体力から減算
        if (colony_HP < 0) colony_HP = 0;//体力が0以下なら体力を0に
        //Debug.Log(colony_HP /Data.COLONY_VALUE_MAX);
        colony_Value_Gauge.rectTransform.localScale = new Vector3(colony_HP/Data.COLONY_VALUE_MAX,1.0f);//計算した割合分ゲージを減らす
        gcc.ColorChange();//色の変化をする
    }

    /// <summary>
    /// Colonyの体力を回復する
    /// </summary>
    /// <param name="recoveryValue">回復量</param>
    public void ColonyRecovery(int recoveryValue)
    {
        Debug.Log("回復！");
        colony_HP += recoveryValue;//回復量分HPを回復
        if (colony_HP > Data.COLONY_VALUE_MAX) colony_HP = (int)Data.COLONY_VALUE_MAX;//最大値を超えたらHPを最大値に設定
        //Debug.Log(colony_HP /Data.COLONY_VALUE_MAX);
        colony_Value_Gauge.rectTransform.localScale = new Vector3(colony_HP/Data.COLONY_VALUE_MAX,1.0f);//計算した割合分ゲージを増やす
        gcc.ColorChange();//色の変化をする
    }
}
