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
    public override void StatUse(Adventurer adventurer) // Àç±¸Çö
    {
        Debug.Log($"{adventurer.AdventurerData.AdventurerName}ÀÇ ½ºÅÈ »ó½Â");
        adventurer.AdventurerData.CurrentSTR += _buffSTR;
        adventurer.AdventurerData.CurrentMAG += _buffMAG;
        adventurer.AdventurerData.CurrentINS += _buffINS;
        adventurer.AdventurerData.CurrentDEX += _buffDEX;
    }
}

