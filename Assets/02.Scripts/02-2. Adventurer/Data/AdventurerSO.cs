using System.Collections.Generic;
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
    Idle,// 기본 상태(PickManager에서 뽑을 수 있음)
    TodayCome, // 오늘 퀘스트를 수주하러 옴
    Questing, // 퀘스트 중
    Injured, // 부상
    Dead // 사망
}

public enum AdventurerType
{
    Major, // 주요 모험가
    Minor // 보조 모험가
}

[System.Serializable]
public class DialogSet
{
    public List<string> Dialog;
}


[CreateAssetMenu(fileName = "AdventurerSO", menuName = "ScriptableObject/AdventurerSO")]
public class AdventurerSO : ScriptableObject
{
    public AdventurerType AdventurerType; // 모험가 타입
    public int OriginalSTR; // 근력
    public int OriginalMAG; // 마력
    public int OriginalINS; //통찰력
    public int OriginalDEX; // 손재주
    public List<DialogSet> DialogList = new List<DialogSet>(); // 대사
}
