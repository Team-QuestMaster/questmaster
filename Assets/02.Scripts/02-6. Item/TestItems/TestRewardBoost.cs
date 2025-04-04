using UnityEngine;

public class TestRewardBoost : Item
{
    // 보상 증가 및 패널티 감소 아이템은 중복 적용하면 안됩니다.
    // float에서 int로 형변환을 했기 때문에 역연산으로 복구하지 않고 원본값을 저장합니다.
    // 순서가 꼬일 수 있는 구조이므로 유의하시기 바랍니다. 

    private int _originalFameReward;
    private int _originalGoldReward;

    public override void Use(Adventurer adventurer, Quest quest) // 재구현
    {
        Debug.Log($"{quest.name}의 보상 증가 : {Name}");
        _originalFameReward = quest.QuestData.FameReward; // 원본값 저장
        _originalGoldReward = quest.QuestData.GoldReward; // 원본값 저장
        quest.QuestData.FameReward = (int)(1.1f * quest.QuestData.FameReward); // 보상 증가
        quest.QuestData.GoldReward = (int)(1.1f * quest.QuestData.GoldReward); // 보상 증가
        ItemManager.Instance.RemoveItem(this);
    }

    public override void Rollback(Adventurer adventurer, Quest quest) // 재구현
    {
        Debug.Log($"{quest.name}의 성공 확률 복구 : {Name}");
        quest.QuestData.FameReward = _originalFameReward; // 보상 복구
        quest.QuestData.GoldReward = _originalGoldReward; // 보상 복구
    }
}

