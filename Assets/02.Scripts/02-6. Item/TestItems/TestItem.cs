using UnityEngine;

public class TestItem : Item
{
    public override void Use(Adventurer adventurer, Quest quest) // 재구현
    {
        Debug.Log($"효과 적용 : {Name}");
    }

    public override void Rollback(Adventurer adventurer, Quest quest) // 재구현
    {
        Debug.Log($"효과 종료 : {Name}");
    }
}

