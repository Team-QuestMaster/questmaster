using UnityEngine;

public class TestRewardBoost : Item
{
    // ���� ���� �� �г�Ƽ ���� �������� �ߺ� �����ϸ� �ȵ˴ϴ�.
    // float���� int�� ����ȯ�� �߱� ������ ���������� �������� �ʰ� �������� �����մϴ�.
    // ������ ���� �� �ִ� �����̹Ƿ� �����Ͻñ� �ٶ��ϴ�. 

    private int _originalFameReward;
    private int _originalGoldReward;

    public override void Use(Adventurer adventurer, Quest quest) // �籸��
    {
        Debug.Log($"{quest.name}�� ���� ���� : {Name}");
        _originalFameReward = quest.QuestData.FameReward; // ������ ����
        _originalGoldReward = quest.QuestData.GoldReward; // ������ ����
        quest.QuestData.FameReward = (int)(1.1f * quest.QuestData.FameReward); // ���� ����
        quest.QuestData.GoldReward = (int)(1.1f * quest.QuestData.GoldReward); // ���� ����
        ItemManager.Instance.RemoveItem(this);
    }

    public override void Rollback(Adventurer adventurer, Quest quest) // �籸��
    {
        Debug.Log($"{quest.name}�� ���� Ȯ�� ���� : {Name}");
        quest.QuestData.FameReward = _originalFameReward; // ���� ����
        quest.QuestData.GoldReward = _originalGoldReward; // ���� ����
    }
}

