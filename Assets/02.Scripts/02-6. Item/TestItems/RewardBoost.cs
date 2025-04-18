using UnityEngine;

public class RewardBoost : QuestItem
{
    [SerializeField]
    private float _boostPersent;

    public override float QuestUse(Quest quest) // 재구현
    {
        quest.QuestData.FameReward = (int)((1 + (_boostPersent / 100)) * quest.QuestData.FameReward); // 보상 증가
        quest.QuestData.GoldReward = (int)((1 + (_boostPersent / 100)) * quest.QuestData.GoldReward); // 보상 증가
        return 0f;
    }
}

