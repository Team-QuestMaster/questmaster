using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OneDayStartAndEnd : MonoBehaviour
{

    [SerializeField] private Image _dayEndFade;

    private void Start()
    {
        _dayEndFade.DOFade(0, 0.5f);
    }

    void EndDay()
    {
        _dayEndFade.DOFade(1, 0.5f);
    }
}
