using UnityEngine;

public class ProbabilityBoost : QuestItem
{
    [SerializeField]
    private float _increasePersentPoint;
    public override float QuestUse(Quest quest) // �籸��
    {
        Debug.Log($"{quest.QuestData.QuestName}�� ���� Ȯ�� {_increasePersentPoint}% ����");
        return _increasePersentPoint;
    }

}

