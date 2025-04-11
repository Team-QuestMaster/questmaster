using System.Collections.Generic;   
using UnityEngine;

public class MinorAdventurerStatHandler : MonoBehaviour
{
    [SerializeField]
    private int _adjustmentValue = 3; // ���� ��¥�� ������ ���� +- ���� ���̿��� ������ ���� ���� ���ȿ� �ݿ��ȴ�. 
    public void SetRandomStat(Adventurer adventurer, int currentDate)
    {
        AdventurerData data = adventurer.AdventurerData;
        AdventurerSO adventurerSO = adventurer.AdventurerSO;

        List<float> weights = MakeRandomStatWeights();
        Debug.Log($"{weights[0]}, {weights[1]}, {weights[2]}, {weights[3]}");
        float strMultiplier = weights[0], magMultiplier = weights[1],
            insMultiplier = weights[2], dexMultiplier = weights[3];

        List<int> adjustments = MakeRandomStatAdjustments(currentDate);
        int strAdjustment = adjustments[0], magAdjustment = adjustments[1], 
            insAdjustment = adjustments[2], dexAdjustment = adjustments[3];

        data.CurrentSTR = (int)(adventurerSO.OriginalSTR * currentDate * strMultiplier) + strAdjustment;
        data.CurrentMAG = (int)(adventurerSO.OriginalMAG * currentDate * magMultiplier) + magAdjustment;
        data.CurrentINS = (int)(adventurerSO.OriginalINS * currentDate * insMultiplier) + insAdjustment;
        data.CurrentDEX = (int)(adventurerSO.OriginalDEX * currentDate * dexMultiplier) + dexAdjustment;
    }
    private List<float> MakeRandomStatWeights()
    {
        List<float> baseValues = new List<float>();
        float sum = 0f;
        for (int i = 0; i < 4; i++)
        {
            float val = Random.Range(0.9f, 1.1f);
            baseValues.Add(val);
            sum += val;
        }
        // ����� 1�� �ǵ��� �� ���� �������ش�.
        for (int i = 0; i < 4; i++)
        {
            baseValues[i] = baseValues[i] / sum * 4f;  
        }
        return baseValues;
    }
    private List<int> MakeRandomStatAdjustments(int currentDate)
    {
        List<int> statAdjustments = new List<int>();
        int min = -2 * currentDate;
        int max = 2 * currentDate + 1;
        for (int i = 0; i < 4; i++)
        {
            int adjustment = Random.Range(min, max);
            statAdjustments.Add(adjustment);
        }
        return statAdjustments;
    }
}
