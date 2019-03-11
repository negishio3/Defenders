using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaugeColorChanger : MonoBehaviour
{
    Image gauge_HP;

    // Start is called before the first frame update
    void Start()
    {
        gauge_HP = gameObject.GetComponent<Image>();
    }

    public void ColorChange()
    {
        gauge_HP.color = gauge_HP.rectTransform.localScale.x > 0.5f ? 
            new Color(1.0f - (1.0f * ((gauge_HP.rectTransform.localScale.x - 0.5f) / 0.5f)), 1.0f, 0.0f, 1.0f) 
            : new Color(1.0f, 1.0f * (gauge_HP.rectTransform.localScale.x / 0.5f), 0.0f, 1.0f);
    }
}
