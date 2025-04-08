using UnityEngine;

public class TestPreventDie : Item
{
    AdventurerStateType _originAdventurerStateType;
    public override void Use(Adventurer adventurer, Quest quest) // �籸��
    {
        Debug.Log($"{adventurer.AdventurerData.AdventurerName}�� ����� ���� : {Name}");
        _originAdventurerStateType = adventurer.AdventurerData.AdventurerState; // ������ ���� 
        if(adventurer.AdventurerData.AdventurerState == AdventurerStateType.Dead)
        {
            adventurer.AdventurerData.AdventurerState = AdventurerStateType.Injured; // ��� ����
        }
    }

    public override void Rollback(Adventurer adventurer, Quest quest) // �籸��
    {
        Debug.Log($"{quest.QuestData.QuestName}�� ���� �� ���� ���� : {Name}");
        adventurer.AdventurerData.AdventurerState = _originAdventurerStateType; // ��� ����

    }
}

