using System;
using System.Collections.Generic;
using DG.Tweening;
using DG.Tweening.Plugins.Options;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

public class DayChangeUI : MonoBehaviour
{
    //Morning -> Afternoon -> Night
    public List<List<Image>> SkyImages = new List<List<Image>>(3);
public List<Image>  MorningImages;
public List<Image> AfternoonImages;
public List<Image>  NightImages;
    private int _currentStep;
    private int _characterCount;
    [SerializeField] private Image DayFadeImage;
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

        SkyImages = new List<List<Image>> { MorningImages, AfternoonImages, NightImages };
    }

    private void ReturnDay()
    {
        for (int i = 1; i < SkyImages.Count; i++)
        {
            foreach (Image image in SkyImages[i])
            {
                Color color = image.color;
                color.a = 0;
                image.color = color;
            }
            
        }

        _currentStep = 0;
    }

    public void Night()
    {
        foreach (Image image in SkyImages[SkyImages.Count - 1])
        {
            Color color = image.color;
            color.a = 1;
            image.color = color;
        }
    }


    public void TimeFlow()
    {
        float duration = 1f;

        if (_currentStep + _step <= _characterCount * 1)
        {
            _currentStep += _step;
            float targetAlpha = (float)_currentStep / (float)_characterCount;

            foreach (Image image in SkyImages[1])
            {
                image.DOFade(targetAlpha, duration);
            }
        }
        else if (_currentStep + _step <= _characterCount * 2)
        {
            _currentStep += _step;
            float targetAlpha = (float)(_currentStep - _characterCount) / (float)_characterCount;

            foreach (Image image in SkyImages[2])
            {
                image.DOFade(targetAlpha, duration);
            }
        }
        else
        {
            foreach (Image image in SkyImages[2])
            {
                image.DOFade(1f, duration);
            }
        }
        DayFadeImage.DOFade((float)(_currentStep/_characterCount)*0.5f, duration);
    }

}