using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public class DayChangeUI : MonoBehaviour
{
    //Morning -> Afternoon -> Night
    public Image[] SkyImages;

    private int _currentStep;
    private int _characterCount;

    [SerializeField]
    private int _step = 3;

    [SerializeField]
    private float _duration = 0.5f;


    void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        DateManager.Instance.OnDateChanged += ReturnDay;
        StageShowManager.Instance.MiniCharacter.MinisMoveEvent += TimeFlow;
        _currentStep = 0;
        _characterCount = UIManager.Instance.CharacterUI.Characters.Count;
    }

    private void ReturnDay()
    {
        for (int i = 1; i < SkyImages.Length; i++)
        {
            Color color = SkyImages[i].color;
            color.a = 0;
            SkyImages[i].color = color;
        }

        _currentStep = 0;
    }

    public void Night()
    {
        Color color = SkyImages[SkyImages.Length - 1].color;
        color.a = 1;
        SkyImages[SkyImages.Length - 1].color = color;
    }

    public void TimeFlow()
    {
        float duration = 1f; // 알파 변화 애니메이션 시간 (원하는 값으로 조절)

        if (_currentStep + _step <= _characterCount * 1)
        {
            _currentStep += _step;

            float targetAlpha = (float)_currentStep / (float)_characterCount;
            SkyImages[1].DOFade(targetAlpha, duration);
        }
        else if (_currentStep + _step <= _characterCount * 2)
        {
            _currentStep += _step;

            float targetAlpha = (float)(_currentStep - _characterCount) / (float)_characterCount;
            SkyImages[2].DOFade(targetAlpha, duration);
        }
        else
        {
            SkyImages[2].DOFade(1f, duration);
        }
    }
}