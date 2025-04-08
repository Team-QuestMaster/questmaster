using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OneDayStartAndEnd : MonoBehaviour
{

    [SerializeField] private Image _dayEndFade;

    void Start()
    {
        StartDay();
    }
    
    public void StartDay()
    {
        _dayEndFade.DOFade(0, 0.5f);
    }

    public void EndDay()
    {
        _dayEndFade.DOFade(1, 0.5f);
    }
}
