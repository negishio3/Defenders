using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerColor : MonoBehaviour
{
    Image gauge_Timer;

    // Start is called before the first frame update
    void Start()
    {
        gauge_Timer = gameObject.GetComponent<Image>();
    }

    private void Update()
    {
        ColorChange();
    }

    public void ColorChange()
    {
        gauge_Timer.color = gauge_Timer.fillAmount > 0.5f ? new Color(1.0f - (1.0f * ((gauge_Timer.fillAmount - 0.5f) / 0.5f)), 1.0f, 0.0f, 1.0f) : new Color(1.0f, 1.0f * (gauge_Timer.fillAmount / 0.5f), 0.0f, 1.0f);
    }
}
