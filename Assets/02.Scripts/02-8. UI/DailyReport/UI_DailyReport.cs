using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_DailyReport : MonoBehaviour
{
    // 데일리 레포트 UI에 정보를 표시
    [Header("UI")]
    [SerializeField] 
    private TextMeshProUGUI _goldChangeText;        // 골드 변화량
    
    [SerializeField] 
    private TextMeshProUGUI _fameChangeText;        // 명성 변화량
    
    [SerializeField] 
    private TextMeshProUGUI _questResultText;       // 퀘스트 결과
    
    [SerializeField] 
    private TextMeshProUGUI _specialCommentText;    // 특수 텍스트
    
    [SerializeField] 
    private Button _closeButton;
    
    [SerializeField]
    private Vector3 _hidePosition;

    [Header("Audio")]
    [SerializeField]
    private AudioClip _onReportAudioClip;

    private void Start()
    {
        _closeButton.onClick.AddListener(HideDailyReport);
        // TODO: 구조 수정 필요
        _closeButton.onClick.AddListener(StageShowManager.Instance.Appear);
    }
    
    public void RefreshDailyReport(DailyReportData data)
    {
        _goldChangeText.text = $"{data.BeforeGold} -> {data.AfterGold}";
        _fameChangeText.text = $"{data.BeforeFame} -> {data.AfterFame}";
        _questResultText.text = data.QuestResult;
        _specialCommentText.text = data.SpecialComment;
    }
    
    public void ShowDailyReport()
    {
        AudioManager.Instance.PlaySFX(_onReportAudioClip);
        gameObject.SetActive(true);
        transform.DOMove(Vector3.zero, 1f);
    }
    
    private void HideDailyReport()
    {
        AudioManager.Instance.PlaySFX(_onReportAudioClip);
        // TODO: 월드 위치로 이동함 수정필요
        transform.DOMove(_hidePosition, 1f).OnComplete(() => gameObject.SetActive(false));
    }
}