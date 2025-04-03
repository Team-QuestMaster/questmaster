using UnityEngine;

public enum QuestTierType
{
    Red,
    Orange,
    Yellow,
    Blue,
    Green
}

[CreateAssetMenu(fileName = "QuestSO", menuName = "ScriptableObject/QuestSO")]
public class QuestSO : ScriptableObject
{
    public string QuestName; // 퀘스트 이름
    public string QuestDescription; // 퀘스트 설명
    public QuestTierType QuestTier;
    public float STRWeight; // 퀘스트 근력 가중치
    public float MAGWeight; // 퀘스트 MAG 비율
    public float INSWeight; // 퀘스트 INS 비율
    public float DEXWeight; // 퀘스트 DEX 비율
    public float StatForClear; // 기준 전투력
    public int FameReward; // 명성 보상
    public int GoldReward; // 골드 보상
    public int FamePenalty; // 명성 페널티
    public int GoldPenalty; // 골드 페널티
    public AdventurerInjuryStateType StateAfterFail; // 실패 후 모험가 부상 상태
    public int Days; // 퀘스트 소요 일수

}
