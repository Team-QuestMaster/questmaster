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

    public override float QuestUse(Quest quest) // �籸��
    {
        Debug.Log($"{quest.name}�� ���� ����");
        if(_rewardQuestType == RewardQuestType.Fame)
        {
            quest.QuestData.FameReward = (int)((1+(_boostPersent/100)) * quest.QuestData.FameReward); // ���� ����
        }
        else if (_rewardQuestType == RewardQuestType.Gold)
        {
            quest.QuestData.GoldReward = (int)((1+(_boostPersent/100)) * quest.QuestData.GoldReward); // ���� ����
        }
        return 0f;
    }
}

