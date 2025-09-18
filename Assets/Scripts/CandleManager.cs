using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class CandleManager : MonoBehaviour
{
    public static CandleManager instance;
    public List<CandleToggle> allCandles = new List<CandleToggle>();
    public GameObject flameObject;

    void Awake()
    {
        instance = this;

        if (flameObject != null)
            flameObject.SetActive(false);
    }


    public void RegisterCandle(CandleToggle candle)
    {
        if (!allCandles.Contains(candle))
            allCandles.Add(candle);
    }

    //check if all candles are litted
    public void CheckAllCandles()
    {
        bool allOn = true;
        foreach (var candle in allCandles)
        {
            if (!candle.isLit)
            {
                allOn = false;
                break;
            }
        }

        if (flameObject != null)
            flameObject.SetActive(allOn);
    }
}
