using UnityEngine;

public class ProbabilityBoost : QuestItem
{
    [SerializeField]
    private float _increasePersentPoint;
    public override float QuestUse(QuestModel quest) // �籸��
    {
        return _increasePersentPoint;
    }

}

