using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewAIStatusAsset", menuName = "AIStatus/Create AIStatus Asset")]

public class EnemyAsset : ScriptableObject
{
    public float Hp;
    public float Power;
    public enum EnemyType
    {
        ARIES,
        TAURUS,
        GEMINI,
        CANCER,
        LEO,
        VIRGO,
        LIBRA,
        SCORPIUS,
        SAGITTARIUS,
        CAPRICORNUS,
        AQUARIUS,
        PISCES
    }
}
