using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowResult : MonoBehaviour
{
    public event Action ResultShowEvent;
    public  event Action ResultHideEvent;
 
    private Quest _questData;
    private Adventurer _adventurerData;
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
        /*
        UIManager.Instance.StampUI.UIApproveEvent += Approved;
        UIManager.Instance.StampUI.UIRejectEvent += Rejected;
        */
    }

    public void Initialize(Adventurer adventurer, Quest questData)
    {
        _adventurerData = adventurer;
        _questData = questData;
    }
    
    public void Show()
    {
        //ResultShowEvent?.Invoke();
        _resultImage.gameObject.SetActive(true);
        _backgroundImage.gameObject.SetActive(true);
        _backgroundImage.DOFade(0.7f, 0.5f);
        _resultImage.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
    }

    public void Hide()
    {
        //ResultHideEvent?.Invoke();
        _backgroundImage.DOFade(0, 0.5f).OnComplete(() =>  _backgroundImage.gameObject.SetActive(false));
        _resultImage.transform.DOLocalMove(new Vector3(-1470f,0, 0), 0.5f).OnComplete(() =>
        {
            
            _resultImage.gameObject.SetActive(false);
            UIManager.Instance.StampUI.StampInteract();
            StageShowManager.Instance.ShowCharacter.Disappear();
            StageShowManager.Instance.ShowQuest.Disappear();
            StageShowManager.Instance.ShowIDCard.Disappear();
        });
        
    }

    public void ResultText(bool isApproved, float probability, float reward, float penalty)
    {
        _resultImage.gameObject.SetActive(true);
        _isApprove = isApproved;
        _approveResultText.text = _isApprove ? "<color=green>승낙함</color>" : "<color=red>거절됨</color>";
        _questNameText.text = _questData.QuestData.QuestName;
        _adventurerNameText.text = _adventurerData.AdventurerData.AdventurerName;
        _timeText.text = _questData.QuestData.Days.ToString();
        _questProbabilityText.text = $"{probability:N1}%";
        _rewardText.text = $"{reward}";
        _penalityText.text = $"{penalty}";
        Show();
    }
    
    /*
    public void Approved()
    {
        Debug.Log("ShowResult Approved");
        _isApprove = true;
        ResultText();
    }

    public void Rejected()
    {
        _isApprove = false; 
        ResultText();
    }
    */
}
