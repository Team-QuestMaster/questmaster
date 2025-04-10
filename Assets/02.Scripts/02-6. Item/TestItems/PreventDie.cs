using UnityEngine;

public class PreventDie : QuestItem
{
    public override float QuestUse(Quest quest) // Àç±¸Çö
    {
        quest.QuestData.StateAfterFail = AdventurerStateType.Idle;
        return 0;
    }
}

