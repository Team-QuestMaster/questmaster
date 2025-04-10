using UnityEngine;

public class ProbabilityBoost : QuestItem
{
    [SerializeField]
    private float _increasePersentPoint;
    public override float QuestUse(Quest quest) // 재구현
    {
        Debug.Log($"{quest.QuestData.QuestName}의 성공 확률 {_increasePersentPoint}% 증가");
        return _increasePersentPoint;
    }

}

