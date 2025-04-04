using UnityEngine;

public class TestPassiveStatBuff : Item
{
    public override void Use(Adventurer adventurer, Quest quest) // �籸��
    {
        Debug.Log($"{adventurer.AdventurerData.AdventurerName}�� STR 1 ���� ��� : {Name}");
        adventurer.AdventurerData.CurrentSTR += 1;

        ItemManager.Instance.ItemUsed(this);
    }

    public override void Rollback(Adventurer adventurer, Quest quest) // �籸��
    {
        Debug.Log($"ȿ�� ���� : {Name}");
    }
}

