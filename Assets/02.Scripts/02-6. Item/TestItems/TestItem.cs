using UnityEngine;

public class TestItem : Item
{
    public override void Use(Adventurer adventurer, Quest quest) // �籸��
    {
        Debug.Log($"ȿ�� ���� : {Name}");
    }

    public override void Rollback(Adventurer adventurer, Quest quest) // �籸��
    {
        Debug.Log($"ȿ�� ���� : {Name}");
    }
}

