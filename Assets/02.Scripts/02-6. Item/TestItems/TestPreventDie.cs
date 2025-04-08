using UnityEngine;

public class TestPreventDie : Item
{
    AdventurerStateType _originAdventurerStateType;
    public override void Use(Adventurer adventurer, Quest quest) // 재구현
    {
        Debug.Log($"{adventurer.AdventurerData.AdventurerName}의 사망을 방지 : {Name}");
        _originAdventurerStateType = adventurer.AdventurerData.AdventurerState; // 원본값 저장 
        if(adventurer.AdventurerData.AdventurerState == AdventurerStateType.Dead)
        {
            adventurer.AdventurerData.AdventurerState = AdventurerStateType.Injured; // 사망 방지
        }
    }

    public override void Rollback(Adventurer adventurer, Quest quest) // 재구현
    {
        Debug.Log($"{quest.QuestData.QuestName}의 실패 후 조건 복구 : {Name}");
        adventurer.AdventurerData.AdventurerState = _originAdventurerStateType; // 사망 복구

    }
}

