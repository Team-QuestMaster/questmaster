using UnityEngine;

public class TestProbabilityBoost : Item
{
    public override void Use(Adventurer adventurer, Quest quest) // 재구현
    {
        Debug.Log($"{quest.QuestData.QuestName}의 성공 확률 10% 증가 : {Name}");
        quest.QuestData.PowerForClear /= 1.1f; // 성공 확률 증가
        ItemManager.Instance.RemoveItem(this);
    }

    public override void Rollback(Adventurer adventurer, Quest quest) // 재구현
    {
        Debug.Log($"{quest.QuestData.QuestName}의 성공 확률 복구 : {Name}");
        quest.QuestData.PowerForClear *= 1.1f; // 성공 확률 증가
    }
}

