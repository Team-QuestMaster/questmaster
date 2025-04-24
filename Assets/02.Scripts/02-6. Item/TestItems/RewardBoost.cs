using UnityEngine;

public class RewardBoost : QuestItem
{
    [SerializeField]
    private float _boostPersent;

    public override float QuestUse(QuestModel quest) // �籸��
    {
        quest.QuestData.FameReward = (int)((1 + (_boostPersent / 100)) * quest.QuestData.FameReward); // ���� ����
        quest.QuestData.GoldReward = (int)((1 + (_boostPersent / 100)) * quest.QuestData.GoldReward); // ���� ����
        return 0f;
    }
}

