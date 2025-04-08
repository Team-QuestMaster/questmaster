using UnityEngine;

public class TestReducePenalty : Item
{
    // ���� ���� �� �г�Ƽ ���� �������� �ߺ� �����ϸ� �ȵ˴ϴ�.
    // float���� int�� ����ȯ�� �߱� ������ ���������� �������� �ʰ� �������� �����մϴ�.
    // ������ ���� �� �ִ� �����̹Ƿ� �����Ͻñ� �ٶ��ϴ�. 

    private int _originalFamePenalty;
    private int _originalGoldPenalty;

    public override void Use(Adventurer adventurer, Quest quest) // �籸��
    {
        Debug.Log($"{quest.name}�� �г�Ƽ ���� : {Name}");
        _originalFamePenalty = quest.QuestData.FamePenalty; // ������ ����
        _originalGoldPenalty = quest.QuestData.GoldPenalty; // ������ ����
        quest.QuestData.FamePenalty = (int)(0.5f * quest.QuestData.FamePenalty); // �г�Ƽ ����
        quest.QuestData.GoldPenalty = (int)(0.5f * quest.QuestData.GoldPenalty); 
    }

    public override void Rollback(Adventurer adventurer, Quest quest) // �籸��
    {
        Debug.Log($"{quest.name}�� ���� Ȯ�� ���� : {Name}");
        quest.QuestData.FamePenalty = _originalFamePenalty; // �г�Ƽ ����
        quest.QuestData.GoldPenalty = _originalGoldPenalty; 
    }
}

