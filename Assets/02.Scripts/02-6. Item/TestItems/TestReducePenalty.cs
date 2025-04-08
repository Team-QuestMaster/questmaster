using UnityEngine;

public class TestReducePenalty : Item
{
    // 보상 증가 및 패널티 감소 아이템은 중복 적용하면 안됩니다.
    // float에서 int로 형변환을 했기 때문에 역연산으로 복구하지 않고 원본값을 저장합니다.
    // 순서가 꼬일 수 있는 구조이므로 유의하시기 바랍니다. 

    private int _originalFamePenalty;
    private int _originalGoldPenalty;

    public override void Use(Adventurer adventurer, Quest quest) // 재구현
    {
        Debug.Log($"{quest.name}의 패널티 감소 : {Name}");
        _originalFamePenalty = quest.QuestData.FamePenalty; // 원본값 저장
        _originalGoldPenalty = quest.QuestData.GoldPenalty; // 원본값 저장
        quest.QuestData.FamePenalty = (int)(0.5f * quest.QuestData.FamePenalty); // 패널티 감소
        quest.QuestData.GoldPenalty = (int)(0.5f * quest.QuestData.GoldPenalty); 
    }

    public override void Rollback(Adventurer adventurer, Quest quest) // 재구현
    {
        Debug.Log($"{quest.name}의 성공 확률 복구 : {Name}");
        quest.QuestData.FamePenalty = _originalFamePenalty; // 패널티 복구
        quest.QuestData.GoldPenalty = _originalGoldPenalty; 
    }
}

