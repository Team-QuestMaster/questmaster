using System;
using System.Collections.Generic;
using DG.Tweening.Plugins.Options;
using UnityEngine;
using UnityEngine.UI;

public class DayChangeUI : MonoBehaviour
{
    //Morning -> Afternoon -> Night
    public Image[] SkyImages;

    private int _currentStep;
    private int _characterCount;
    [SerializeField] private int _step = 3;
    
    
    public event Action TimePassing;

    void Start()
    {
        Initialize();
        
    }
    public void Initialize()
    {
        TimePassing = TimeFlow;
        _currentStep = 0;
        _characterCount = UIManager.Instance.CharacterUI.Characters.Count;
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
        if (_currentStep + _step <= _characterCount * 1)
        {
            _currentStep += _step;
            
            Color color = SkyImages[1].color;
            color.a = (float)_currentStep/(float)_characterCount;
            SkyImages[1].color = color;
            //Debug.Log($"낮에 있는 놈을{(float)_currentStep/(float)_characterCount}로 바꿈");
        }
        else if (_currentStep + _step <= _characterCount * 2)
        {
            _currentStep += _step;
            
            Color color = SkyImages[2].color;
            color.a = (float)(_currentStep - _characterCount)/(float)_characterCount;
            SkyImages[2].color = color;
            //Debug.Log($"밤에 있는 놈을{(float)(_currentStep - _characterCount)/(float)_characterCount}로 바꿈");
        }
        else
        {
            Color color = SkyImages[2].color;
            color.a = 1;
            SkyImages[2].color = color;
        }
        
        //Debug.Log(_currentStep);
        
        
    }
    
    
}
