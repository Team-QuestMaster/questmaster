using UnityEngine;

public class StatBoost : StatItem
{
    [SerializeField]
    private int _buffSTR;
    [SerializeField]
    private int _buffMAG;
    [SerializeField]
    private int _buffINS;
    [SerializeField]
    private int _buffDEX;
    public override void StatUse(Adventurer adventurer) // �籸��
    {
        Debug.Log($"{adventurer.AdventurerData.AdventurerName}�� ���� ���");
        adventurer.AdventurerData.CurrentSTR += _buffSTR;
        adventurer.AdventurerData.CurrentMAG += _buffMAG;
        adventurer.AdventurerData.CurrentINS += _buffINS;
        adventurer.AdventurerData.CurrentDEX += _buffDEX;
    }
}

