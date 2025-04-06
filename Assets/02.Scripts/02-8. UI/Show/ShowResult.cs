using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowResult : MonoBehaviour
{
    public event Action ResultShowEvent;
    public  event Action ResultHideEvent;
 
    public Quest QuestData;
    public Adventurer AdventurerData;
    [SerializeField] private bool _isApprove;
    [SerializeField] private Image _resultImage;
    [SerializeField] private TextMeshProUGUI _approveResultText;
    [SerializeField] private TextMeshProUGUI _questNameText;
    [SerializeField] private TextMeshProUGUI _adventurerNameText;
    [SerializeField] private TextMeshProUGUI _timeText;
    [SerializeField] private TextMeshProUGUI _questProbabilityText;
    [SerializeField] private TextMeshProUGUI _rewardText;
    [SerializeField] private TextMeshProUGUI _penalityText;

    
    [SerializeField]  private Image _backgroundImage;
    
    void Start()
    {
        UIManager.Instance.StampUI.UIApproveEvent += Approved;
        UIManager.Instance.StampUI.UIRejectEvent += Rejected;
        Debug.Log("ShoResult EventAdded");
    }
    
    public void Show()
    {
        ResultShowEvent?.Invoke();
        _resultImage.gameObject.SetActive(true);
        _backgroundImage.gameObject.SetActive(true);
        _backgroundImage.DOFade(0.7f, 0.5f);
        _resultImage.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
    }

    public void Hide()
    {
        ResultHideEvent?.Invoke();
        _backgroundImage.DOFade(0, 0.5f).OnComplete(() =>  _backgroundImage.gameObject.SetActive(false));
        _resultImage.transform.DOLocalMove(new Vector3(-1470f,0, 0), 0.5f).OnComplete(() =>
        {
            
            _resultImage.gameObject.SetActive(false);
            UIManager.Instance.StampUI.StampInteract();
            StageShowManager.Instance.ShowCharacter.Disappear();
        });
        
    }

    void ResultText()
    {
        _resultImage.gameObject.SetActive(true);
        float probability = CalculateManager.Instance.CalculateProbability(AdventurerData, QuestData);
        
        _approveResultText.text = _isApprove ? "<color=green>승낙함</color>" : "<color=red>거절됨</color>";
        _questNameText.text = QuestData.QuestData.QuestName;
        _adventurerNameText.text = AdventurerData.AdventurerData.AdventurerName;
        _timeText.text = QuestData.QuestData.Days.ToString();
        _questProbabilityText.text = $"{probability}%";
        _rewardText.text = $"{QuestData.QuestData.GoldReward}골드";
        _penalityText.text = $"{QuestData.QuestData.GoldPenalty}골드";
        Show();
    }
    
    
    void Approved()
    {
        Debug.Log("ShowResult Approved");
        _isApprove = true;
        ResultText();
    }

    void Rejected()
    {
        _isApprove = false; 
        ResultText();
    }
    
}
