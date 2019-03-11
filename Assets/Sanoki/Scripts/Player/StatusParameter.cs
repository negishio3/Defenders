using System.Collections;
using System;

/// <summary>
/// ステータスに使うパラメータ
/// </summary>
/// <typeparam name="T">型</typeparam>
public struct StatusParameter<T>
{

    public T BaseValue;
    public T AddValue;
    public float MultiplierValue;

    //最終結果を計算するためのデリゲート
    private Func<StatusParameter<T>, T> _calcValue;

    //最終結果を取得する
    public T Value { get { return _calcValue(this); } }

    public StatusParameter(T baseValue, T addValue, float multiplierValue = 1, Func<StatusParameter<T>, T> calcValue = null)
    {
        BaseValue = baseValue;
        AddValue = addValue;
        MultiplierValue = multiplierValue;
        _calcValue = calcValue;
    }
}
