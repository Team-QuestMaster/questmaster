using UnityEngine;


public enum AdventurerTierType
{
    A,//가장 강함
    B,
    C,
    D // 가장 약함
}

public enum AdventurerInjuryStateType
{
    Idle,// 기본 상태
    Injured, // 부상
    Dead // 사망
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
    public AdventurerInjuryStateType AdventurerInjuryState;

}
