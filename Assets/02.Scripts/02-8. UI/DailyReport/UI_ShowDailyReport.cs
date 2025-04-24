using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShowDailyReport : MonoBehaviour
{
    [Header("Interaction")]
    [SerializeField] 
    private Button _closeButton;
    
    [Header("Options")]
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
