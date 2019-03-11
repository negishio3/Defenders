using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ステータス変更ができる
/// </summary>
public interface IStatusModifiable
{
    float RemainingTime { get; }

    /// <summary>
    /// 効果を取り付けたとき
    /// </summary>
    void OnAttach(CharacterStatus status);

    /// <summary>
    /// 効果を取り外した時
    /// </summary>
    void OnDetach(CharacterStatus status);

    /// <summary>
    /// 効果中に呼ばれる
    /// </summary>
    void Update(CharacterStatus status);

}
