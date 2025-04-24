using DG.Tweening;
using TMPro;
using UnityEngine;

public class UI_StampedReport : MonoBehaviour
{
    [SerializeField] 
    private string _adventurerName;
    public string AdventurerName {get => _adventurerName; set => _adventurerName = value; }
    [SerializeField] 
    private string _questName;
    public string QuestName { get => _questName; set => _questName = value; }
    [SerializeField]
    private float _probability;
    public float Probability{ get => _probability; set => _probability = value; }
    [SerializeField] 
    private int _needTime;
    public int NeedTime{ get => _needTime; set => _needTime = value; }
    [SerializeField] 
    private int _reward;
    public int Reward{ get => _reward; set => _reward = value; }
    [SerializeField] 
    private int _penalty;
    public int Penalty{ get => _penalty; set => _penalty = value; }
    [SerializeField] 
    private bool _approve;
    public bool Approve { get => _approve; set => _approve = value; }
    [Header("Hierarchy")]
    [SerializeField]
    private TextMeshProUGUI _questNameText;
    [SerializeField] 
    private TextMeshProUGUI _adventurerNameText;
    [SerializeField] 
    private TextMeshProUGUI _rewardText;
    [SerializeField]
    private TextMeshProUGUI _penaltyText;
    [SerializeField]
    private TextMeshProUGUI _needTimeText;
    [SerializeField]
    private TextMeshProUGUI _probabilityText;
    [SerializeField]
    private TextMeshProUGUI _approveOrNotText;
        
    [SerializeField]
    private UI_FadeInOut _fadeInOutImage;
        
    [Header("연출을 위한 파라미터")]
    [SerializeField] private float _fadeInOutDuration = 0.1f;
    [SerializeField] private float _showDuration = 0.5f;
    [SerializeField] private Vector3 _showPosition = Vector3.zero;
    [SerializeField] private Vector3 _hidePosition = new Vector3(-1470,0,0);
    
    
    public void Refresh(QuestData questData, AdventurerData adventurerData)
    {
        
        _questNameText.text = questData.QuestName;
        _adventurerNameText.text = adventurerData.AdventurerName;
        _rewardText.text = questData.GoldReward.ToString();
        _penaltyText.text = questData.GoldPenalty.ToString();
        _needTimeText.text = questData.Days.ToString();
        //probability는 Calculator가 어떻게 동작하는가에 따라 바뀔 예정
        _probabilityText.text = "30%";
        _approveOrNotText.text= _approve? "<color=#99A136>승낙함</color>" : "<color=#C4402E>거절됨</color>";
    }

    
    
    public void ShowStampedReport()
    {
        
        //AudioManager.Instance.PlaySFX(_onResultAudioClip);
        _fadeInOutImage.FadeIn(0.1f);
        this.transform.DOLocalMove(_showPosition, _showDuration);
        
    }

    public void HideStampedReport()
    {
        _fadeInOutImage.FadeOut(0.1f);
        this.transform.DOLocalMove(_hidePosition, _showDuration).OnComplete(() =>
        {
            //손님 퇴장
        });
    }
    
}
