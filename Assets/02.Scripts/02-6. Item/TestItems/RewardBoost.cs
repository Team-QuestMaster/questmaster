using UnityEngine;

public enum RewardQuestType
{
    Fame,
    Gold
}

public class RewardBoost : QuestItem
{

    [SerializeField]
    private float _boostPersent;
    [SerializeField]
    private RewardQuestType _rewardQuestType;

    public override float QuestUse(Quest quest) // 재구현
    {
        Debug.Log($"{quest.name}의 보상 증가");
        if(_rewardQuestType == RewardQuestType.Fame)
        {
            quest.QuestData.FameReward = (int)((1+(_boostPersent/100)) * quest.QuestData.FameReward); // 보상 증가
        }
        else if (_rewardQuestType == RewardQuestType.Gold)
        {
            quest.QuestData.GoldReward = (int)((1+(_boostPersent/100)) * quest.QuestData.GoldReward); // 보상 증가
        }
        return 0f;
    }
}

