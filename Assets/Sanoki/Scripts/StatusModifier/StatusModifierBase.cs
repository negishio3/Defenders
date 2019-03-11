using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusModifierBase : IStatusModifiable
{
    public float RemainingTime { get; protected set; }

    public StatusModifierBase( float time)
    {
        RemainingTime = time;
    }

    public virtual void OnAttach(CharacterStatus status) { }
    public virtual void OnDetach(CharacterStatus status) { }

    public virtual void Update(CharacterStatus status)
    {
        RemainingTime = Mathf.Max(RemainingTime - Time.deltaTime, 0.0f);
    }
}
