using UnityEngine;

public class ReducePenalty : QuestItem
{
    [SerializeField]
    private float _penaltyRate = 0.5f; // 패널티 감소 비율
    public override float QuestUse(Quest quest) // 재구현
    {
        quest.QuestData.FamePenalty = (int)(_penaltyRate * quest.QuestData.FamePenalty); // 패널티 감소
        quest.QuestData.GoldPenalty = (int)(_penaltyRate * quest.QuestData.GoldPenalty);
        return 0f;
    }
}

