using UnityEngine;

public class CalculateManager : Singleton<CalculateManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public float CalculateProbability(ref Adventurer adventurer, ref Quest quest)
    {
        // Ȯ�� : (���谡 ���ȿ� ���� ��� ������ / ����Ʈ�� �ʿ� ������) * 100; 
        float calculatedPower =
            adventurer.AdventurerData.CurrentSTR * quest.QuestData.STRWeight +
            adventurer.AdventurerData.CurrentMAG * quest.QuestData.MAGWeight +
            adventurer.AdventurerData.CurrentINS * quest.QuestData.INSWeight +
            adventurer.AdventurerData.CurrentDEX * quest.QuestData.DEXWeight;
        float powerForClear = quest.QuestData.PowerForClear;

        float probability = Mathf.Clamp((calculatedPower / powerForClear) * 100f, 0f, 100f);

        JudgeQuestResult(ref adventurer, ref quest, probability);

        return probability;
    }

    private void JudgeQuestResult(ref Adventurer adventurer, ref Quest quest, float probability)
    {
        float randomValue = Random.Range(0f, 100f);
        bool isQuestSuccess = true;
        if (randomValue <= probability)
        {
            isQuestSuccess = true;
        }
        else
        {
            isQuestSuccess = false;
        }

        DateManager.Instance.AddQuestResultToList(ref adventurer, ref quest, isQuestSuccess);
    }
}
