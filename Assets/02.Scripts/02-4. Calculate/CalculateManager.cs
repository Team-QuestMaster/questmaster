using UnityEngine;

public class CalculateManager : Singleton<CalculateManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public float CalculateProbability(Adventurer adventurer, Quest quest)
    {
        // Ȯ�� : (���谡 ���ȿ� ���� ��� ������ / ����Ʈ�� �ʿ� ������) * 100; 
        float calculatedPower =
            adventurer.AdventurerData.CurrentSTR * quest.QuestData.STRWeight +
            adventurer.AdventurerData.CurrentMAG * quest.QuestData.MAGWeight +
            adventurer.AdventurerData.CurrentINS * quest.QuestData.INSWeight +
            adventurer.AdventurerData.CurrentDEX * quest.QuestData.DEXWeight;
        float powerForClear = quest.QuestData.PowerForClear;

        float probability = Mathf.Clamp((calculatedPower / powerForClear) * 100f, 0f, 100f);


        return probability;
    }
    public bool JudgeQuestResult(Adventurer adventurer, Quest quest, float probability)
    {
        float randomValue = Random.Range(0f, 100f);
        if (randomValue <= probability)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
