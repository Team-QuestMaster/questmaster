using UnityEngine;

public class QuestData
{
    private string _questName; // 퀘스트 이름
    public string QuestName { get => _questName; set => _questName = value; }

    private string _questDescription; // 퀘스트 설명
    public string QuestDescription { get => _questDescription; set => _questDescription = value; }

    private QuestTierType _questTier; // 퀘스트 등급
    public QuestTierType QuestTier { get => _questTier; set => _questTier = value; }

    private float _strWeight; // 퀘스트 근력 가중치
    public float STRWeight { get => _strWeight; set => _strWeight = value; }

    private float _magWeight; // 퀘스트 MAG 비율
    public float MAGWeight { get => _magWeight; set => _magWeight = value; }

    private float _insWeight; // 퀘스트 INS 비율
    public float INSWeight { get => _insWeight; set => _insWeight = value; }

    private float _dexWeight; // 퀘스트 DEX 비율
    public float DEXWeight { get => _dexWeight; set => _dexWeight = value; }

    private float _powerForClear; // 기준 전투력
    public float PowerForClear { get => _powerForClear; set => _powerForClear = value; }

    private int _fameReward; // 명성 보상
    public int FameReward { get => _fameReward; set => _fameReward = value; }

    private int _goldReward; // 골드 보상
    public int GoldReward { get => _goldReward; set => _goldReward = value; }

    private int _famePenalty; // 명성 페널티
    public int FamePenalty { get => _famePenalty; set => _famePenalty = value; }

    private int _goldPenalty; // 골드 페널티
    public int GoldPenalty { get => _goldPenalty; set => _goldPenalty = value; }

    private AdventurerStateType _stateAfterFail; // 실패 후 모험가 부상 상태
    public AdventurerStateType StateAfterFail { get => _stateAfterFail; set => _stateAfterFail = value; }

    private int _days; // 퀘스트 소요 일수
    public int Days { get => _days; set => _days = value; }

    private bool _isQuesting; // 퀘스트 진행 중 여부
    public bool IsQuesting { get => _isQuesting; set => _isQuesting = value; }

    public QuestData(string questName, string questDescription, QuestTierType questTier, 
        float strWeight, float magWeight, float insWeight, float dexWeight, 
        float powerForClear, int fameReward, int goldReward, int famePenalty, int goldPenalty, 
        AdventurerStateType stateAfterFail, int days)
    {
        _questName = questName;
        _questDescription = questDescription;
        _questTier = questTier;
        _strWeight = strWeight;
        _magWeight = magWeight;
        _insWeight = insWeight;
        _dexWeight = dexWeight;
        _powerForClear = powerForClear;
        _fameReward = fameReward;
        _goldReward = goldReward;
        _famePenalty = famePenalty;
        _goldPenalty = goldPenalty;
        _stateAfterFail = stateAfterFail;
        _days = days;
    }
}
