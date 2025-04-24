using UnityEngine;
using System;

public enum QuestTierType
{
    Red,
    Orange,
    Yellow,
    Blue,
    Green
}

public class QuestData
{
    private string _questName; // Ʈ ̸
    public string QuestName { get => _questName; set => _questName = value; }

    private string _questDescription; // Ʈ 
    public string QuestDescription { get => _questDescription; set => _questDescription = value; }

    private QuestTierType _questTier; // Ʈ 
    public QuestTierType QuestTier { get => _questTier; set => _questTier = value; }

    private float _strWeight; // Ʈ ٷ ġ
    public float STRWeight { get => _strWeight; set => _strWeight = value; }

    private float _magWeight; // Ʈ MAG 
    public float MAGWeight { get => _magWeight; set => _magWeight = value; }

    private float _insWeight; // Ʈ INS 
    public float INSWeight { get => _insWeight; set => _insWeight = value; }

    private float _dexWeight; // Ʈ DEX 
    public float DEXWeight { get => _dexWeight; set => _dexWeight = value; }

    private float _powerForClear; //  
    public float PowerForClear { get => _powerForClear; set => _powerForClear = value; }

    private int _fameReward; //  
    public int FameReward { get => _fameReward; set => _fameReward = value; }

    private int _goldReward; //  
    public int GoldReward { get => _goldReward; set => _goldReward = value; }

    private int _famePenalty; //  Ƽ
    public int FamePenalty { get => _famePenalty; set => _famePenalty = value; }

    private int _goldPenalty; //  Ƽ
    public int GoldPenalty { get => _goldPenalty; set => _goldPenalty = value; }

    private AdventurerStateType _stateAfterFail; //   谡λ 
    public AdventurerStateType StateAfterFail { get => _stateAfterFail; set => _stateAfterFail = value; }

    private int _days; // Ʈ ҿ ϼ
    public int Days { get => _days; private set => _days = value; }

    private bool _isQuesting; // Ʈ   
    public bool IsQuesting { get => _isQuesting; set => _isQuesting = value; }

    private string _questHint; // Ʈ Ʈ
    public string QuestHint { get => _questHint; set => _questHint = value; }

    public QuestData(string questName, string questDescription, QuestTierType questTier, 
        float strWeight, float magWeight, float insWeight, float dexWeight, 
        float powerForClear, int fameReward, int goldReward, int famePenalty, int goldPenalty, 
        AdventurerStateType stateAfterFail, int days, string questHint)
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
        _questHint = questHint;
    }
    public QuestData Clone()
    {
        return new QuestData
            (_questName, 
            _questDescription, 
            _questTier,
            _strWeight, 
            _magWeight,
            _insWeight, 
            _dexWeight, 
            _powerForClear, 
            _fameReward, 
            _goldReward,
            _famePenalty, 
            _goldPenalty, 
            _stateAfterFail, 
            _days, 
            _questHint);
    }
}
