using UnityEngine;


public enum AdventurerTierType
{
    A,
    B,
    C,
    D
}

public enum AdventurerInjuryStateType
{
    Idle,
    Injured,
    Dead
}



[CreateAssetMenu(fileName = "AdventurerSO", menuName = "ScriptableObject/AdventurerSO")]
public class AdventurerSO : ScriptableObject
{
    public string AdventurerName;  // 이름
    public string AdventurerClass; // 직업
    public string AdventurerTitle; //  칭호
    public AdventurerTierType AdventurerTier; // 티어
    public int OriginalStatSTR; // 근력
    public int OriginalStatMAG; // 마력
    public int OriginalStatINS; //통찰력
    public int ORiginalStatDEX; // 손재주
    public AdventurerInjuryStateType AdventurerInjurySate;

}
