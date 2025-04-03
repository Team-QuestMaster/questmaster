using UnityEngine;

public enum AdventurerTierType
{
    A,//가장 강함
    B,
    C,
    D // 가장 약함
}

public enum AdventurerStateType
{
    Idle,// 기본 상태
    Injured, // 부상
    Dead // 사망
}

public enum AdventurerType
{
    Major, // 주요 모험가
    Minor // 보조 모험가
}


[CreateAssetMenu(fileName = "AdventurerSO", menuName = "ScriptableObject/AdventurerSO")]
public class AdventurerSO : ScriptableObject
{
    public AdventurerType AdventurerType; // 모험가 타입
    public int OriginalSTR; // 근력
    public int OriginalMAG; // 마력
    public int OriginalINS; //통찰력
    public int OriginalDEX; // 손재주
}
