using UnityEngine;

public class ReducePenalty : QuestItem
{
    [SerializeField]
    private float _penaltyRate = 0.5f; // �г�Ƽ ���� ����
    public override float QuestUse(Quest quest) // �籸��
    {
        Debug.Log($"{quest.name}�� �г�Ƽ ����");
        quest.QuestData.FamePenalty = (int)(0.5f * quest.QuestData.FamePenalty); // �г�Ƽ ����
        quest.QuestData.GoldPenalty = (int)(0.5f * quest.QuestData.GoldPenalty);
        return 0f;
    }
}

