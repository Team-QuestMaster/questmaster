using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class OneCycleStartAndEnd : MonoBehaviour
{
    [SerializeField]
    private TaxPayment _taxPayment;

    [SerializeField]
    private Image _fadeImage;

    private event Action NextCycleEvent; // 하루 끝나고 이벤트

    private Tweener _fadeInTween; // 페이드 인 (밝아짐)
    private Tweener _fadeOutTween; // 페이드 아웃 (어두워짐)

    private void Awake()
    {
        // 기본 알파값 설정 (완전히 어두운 상태에서 시작)
        _fadeImage.color = new Color(0, 0, 0, 1);
        _fadeImage.gameObject.SetActive(true);

        // 트윈 초기화
        _fadeInTween = _fadeImage.DOFade(0, 0.5f)
            .SetAutoKill(false)
            .Pause();

        _fadeOutTween = _fadeImage.DOFade(1, 0.5f)
            .SetAutoKill(false)
            .Pause()
            .OnComplete(FadeIn);

        SetNextCycleEvent(FirstCycle); // 첫날 싸이클로 초기화
    }

    private void FadeIn()
    {
        _fadeImage.gameObject.SetActive(true);
        _fadeInTween.Restart();
        NextCycleEvent?.Invoke(); // 다음 Cycle실행
        SetNextCycleEvent(null); // 기본 싸이클로 초기화
    }

    private void FadeOut()
    {
        Debug.Log("Cycle 끝");
        _fadeImage.gameObject.SetActive(true);
        _fadeOutTween.Restart();
    }

    private void StartDayCycle()
    {
        Debug.Log("Cycle이 시작된다");
        // 아침이면
        // 세금 나가고
        int currentDate = DateManager.Instance.CurrentDate;
        string taxMessage = _taxPayment.TryPayTax(currentDate)
            ? $"오늘 나가는 세금: {_taxPayment.Tax}"
            : $"세금 납부일까지 {_taxPayment.GetPaymentTerm(currentDate)}일, 납부할 세금 {_taxPayment.Tax}Gold";
        UIManager.Instance.ReportUI.SpecialCommentText(taxMessage);
        // 레포트 보여주고
        UIManager.Instance.ReportUI.ShowReportUI();
        // 레포트 닫으면 모험가 이벤트 시작
    }

    private void FirstCycle()
    {
        StageShowManager.Instance.Appear(); // 낮 등장 이벤트
    }
    
    // 처음 싸이클 시작
    public void StartCycle()
    {
        FadeIn();
    }

    public void EndCycle()
    {
        FadeOut();
    }

    /// <summary>
    /// Fade out/in이후 발생할 이벤트 등록 = 1개만 등록 (ADD X, Set O)
    /// null이 오면 다음날 이벤트 자동 등록
    /// </summary>
    public void SetNextCycleEvent(Action action)
    {
        if (action != null)
        {
            NextCycleEvent = action;
            return;
        }

        NextCycleEvent = StartDayCycle; // 기본 Cycle인 다음날 Cycle
    }
}