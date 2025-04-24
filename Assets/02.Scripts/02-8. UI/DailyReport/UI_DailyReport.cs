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
    
    public void RefreshDailyReport(DailyReportData data)
    {
        _goldChangeText.text = $"{data.BeforeGold} -> {data.AfterGold}";
        _fameChangeText.text = $"{data.BeforeFame} -> {data.AfterFame}";
        _questResultText.text = data.QuestResult;
        _specialCommentText.text = data.SpecialComment;
    }
}