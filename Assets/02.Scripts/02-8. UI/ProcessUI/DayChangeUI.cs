using System;
using System.Collections.Generic;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public class DayChangeUI : MonoBehaviour
{
    //Morning -> Afternoon -> Night
    public Image[] SkyImages;

    private float _currentSkyStep;
    
    public event Action TimePassing;

    void Start()
    {
        Initialize();
        Debug.Log($"{UIManager.Instance.CharacterUI.Characters.Length} |{SkyImages.Length} |  {_currentSkyStep}");
    }
    public void Initialize()
    {
        TimePassing = TimeFlow;
        //_step = (float)1/(float)(UIManager.Instance.CharacterUI.Characters.Length*(float)SkyImages.Length); //한 스텝당 증가될 사항
        _currentSkyStep = 0;
    }
    
    public void ReturnDay()
    {
        for (int i = 1; i < SkyImages.Length; i++)
        {
            Color color = SkyImages[i].color;
            color.a = 0;
            SkyImages[i].color = color;
        }
    }

    public void Night()
    {
        Color color = SkyImages[SkyImages.Length-1].color;
        color.a = 1;
        SkyImages[SkyImages.Length - 1].color = color;
    }

    public void TimeFlow()
    {
        if (_currentSkyStep < (float)1 / (float)SkyImages.Length)
        {
            Color color = SkyImages[1].color;

            SkyImages[1].color = color;
            
        }else if (_currentSkyStep < (float)2 / (float)SkyImages.Length)
        {
            Color color = SkyImages[2].color;

            SkyImages[2].color = color;
        }
        Debug.Log(_currentSkyStep);
        
        
    }
    
    
}
