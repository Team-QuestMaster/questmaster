using UnityEngine;

public class ReducePenalty : QuestItem
{
    [SerializeField]
    private float _penaltyRate = 0.5f; // �г�Ƽ ���� ����
    public override float QuestUse(QuestModel quest) // �籸��
    {
        quest.QuestData.FamePenalty = (int)(_penaltyRate * quest.QuestData.FamePenalty); // �г�Ƽ ����
        quest.QuestData.GoldPenalty = (int)(_penaltyRate * quest.QuestData.GoldPenalty);
        return 0f;
    }
}

