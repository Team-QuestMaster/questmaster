using UnityEngine;

public class PreventDie : QuestItem
{
    public override float QuestUse(Quest quest) // �籸��
    {
        quest.QuestData.StateAfterFail = AdventurerStateType.Idle;
        return 0;
    }
}

