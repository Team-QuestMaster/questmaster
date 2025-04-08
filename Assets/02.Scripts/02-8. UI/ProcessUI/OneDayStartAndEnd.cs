using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OneDayStartAndEnd : MonoBehaviour
{
    [SerializeField] private Image _dayEndFade;

    public event Action OnDayStart;
    public event Action OnDayEnd;

    private Tweener _fadeInTween;   // 페이드 인 (밝아짐)
    private Tweener _fadeOutTween;  // 페이드 아웃 (어두워짐)

    void Start()
    {
        // 기본 알파값 설정 (완전히 어두운 상태에서 시작)
        _dayEndFade.color = new Color(0, 0, 0, 1);
        _dayEndFade.gameObject.SetActive(true);

        // 트윈 초기화
        _fadeInTween = _dayEndFade.DOFade(0, 0.5f)
            .SetAutoKill(false)
            .Pause()
            .OnComplete(() =>
            {
                StageShowManager.Instance.ShowCharacter.Appear();
            });

        _fadeOutTween = _dayEndFade.DOFade(1, 0.5f)
            .SetAutoKill(false)
            .Pause();

        // 이벤트 연결
        OnDayStart = DayFadeIn;

        OnDayEnd = DayFadeOut;

        StartDay(); // 예시: 게임 시작과 동시에 낮 시작
    }

    void DayFadeIn()
    {
        _dayEndFade.gameObject.SetActive(true);
        _fadeInTween.Restart();
    }

    public void DayFadeOut()
    {
        Debug.Log("끝");
        _dayEndFade.gameObject.SetActive(true);
        _fadeOutTween.Restart();
    }
    
    public void StartDay()
    {
        Debug.Log("하루가 시작된다");
        OnDayStart?.Invoke();
        StageShowManager.Instance.MiniCharacter.MakeMiniCharacters();
        
    }

    public void EndDay()
    {
        Debug.Log("EndDay() 호출됨");

        if (_dayEndFade == null)
        {
            Debug.LogError("_dayEndFade가 null입니다!");
        }

        if (OnDayEnd == null)
        {
            Debug.LogWarning("OnDayEnd 이벤트 연결 안 됨");
        }

        OnDayEnd?.Invoke();
    }

}