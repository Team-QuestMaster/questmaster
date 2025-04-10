using UnityEngine;

public class RewardBoost : QuestItem
{

    [SerializeField]
    private float _boostPersent;

    public override float QuestUse(Quest quest) // �籸��
    {
        Debug.Log($"{quest.name}�� ���� ����");
        quest.QuestData.FameReward = (int)(1.1f * quest.QuestData.FameReward); // ���� ����
        quest.QuestData.GoldReward = (int)(1.1f * quest.QuestData.GoldReward); // ���� ����
        return 0f;
    }
}

