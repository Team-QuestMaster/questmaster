using UnityEngine;

public class TestProbabilityBoost : Item
{
    public override void Use(Adventurer adventurer, Quest quest) // �籸��
    {
        Debug.Log($"{quest.QuestData.QuestName}�� ���� Ȯ�� 10% ���� : {Name}");
        quest.QuestData.PowerForClear /= 1.1f; // ���� Ȯ�� ����
        ItemManager.Instance.RemoveItem(this);
    }

    public override void Rollback(Adventurer adventurer, Quest quest) // �籸��
    {
        Debug.Log($"{quest.QuestData.QuestName}�� ���� Ȯ�� ���� : {Name}");
        quest.QuestData.PowerForClear *= 1.1f; // ���� Ȯ�� ����
    }
}

