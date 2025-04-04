using UnityEngine;

public class TestStatBoost : Item
{
    public override void Use(Adventurer adventurer, Quest quest) // �籸��
    {
        Debug.Log($"{adventurer.AdventurerData.AdventurerName}�� STR 1 �ӽ� ��� : {Name}");
        adventurer.AdventurerData.CurrentSTR += 1;

        ItemManager.Instance.ItemUsed(this);
    }

    public override void Rollback(Adventurer adventurer, Quest quest) // �籸��
    {
        adventurer.AdventurerData.CurrentSTR -= 1;
        Debug.Log($"ȿ�� ���� : {Name}");
    }
}

