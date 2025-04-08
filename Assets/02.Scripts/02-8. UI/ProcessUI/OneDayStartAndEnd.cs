using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OneDayStartAndEnd : MonoBehaviour
{

    [SerializeField] private Image _dayEndFade;

    public event Action OnDayStart;
    public event Action OnDayEnd;
    
    void Start()
    {
        StartDay();
        OnDayStart = () =>
        {
            
            _dayEndFade.gameObject.SetActive(true);
            _dayEndFade.DOFade(0, 0.5f).SetAutoKill(true)
                .OnComplete(() => StageShowManager.Instance.ShowCharacter.Appear());
            
        };
        OnDayEnd = () => { 
            Debug.Log("끝");
            _dayEndFade.gameObject.SetActive(true);
           // _dayEndFade.DOFade(1, 0.5f).SetAutoKill(true);
            
            
        };
        
    }
    
    public void StartDay()
    {
        OnDayStart?.Invoke();
       
        
    }

    public void EndDay()
    {
        OnDayEnd?.Invoke();
    }
}
