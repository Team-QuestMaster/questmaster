using UnityEngine;

public class TestPassiveStatBuff : Item
{
    public override void Use(Adventurer adventurer, Quest quest) // 재구현
    {
        Debug.Log($"{adventurer.AdventurerData.AdventurerName}의 STR 1 영구 상승 : {Name}");
        adventurer.AdventurerData.CurrentSTR += 1;

        ItemManager.Instance.ItemUsed(this);
    }

    public override void Rollback(Adventurer adventurer, Quest quest) // 재구현
    {
        Debug.Log($"효과 유지 : {Name}");
    }
}

