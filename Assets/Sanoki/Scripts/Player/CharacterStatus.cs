using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// キャラクターとかのステータス
/// </summary>
public class CharacterStatus
{

    public StatusParameter<int> HP;
    public StatusParameter<float> MoveSpeed;
    public StatusParameter<float> Power;

    public CharacterStatus(int hp, float moveSpeed, float power)
    {
        //適当に初期化
        //最終ステータス計算式は(BaseValue + AddValue) * MultiplierValue
        HP = new StatusParameter<int>(hp, 0, 1, p => (int)((p.BaseValue + p.AddValue) * p.MultiplierValue));
        MoveSpeed = new StatusParameter<float>(moveSpeed, 0, 1, p => (p.BaseValue + p.AddValue) * p.MultiplierValue);
        Power = new StatusParameter<float>(power, 0, 1, p => (p.BaseValue + p.AddValue) * p.MultiplierValue);
    }
}
