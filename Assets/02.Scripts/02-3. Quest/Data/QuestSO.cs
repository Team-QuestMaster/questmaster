using UnityEngine;

public enum QuestTierType
{
    Red,//매우 어려움
    Orange, // 어려움
    Yellow, // 보통
    Blue, // 쉬움
    Green //매우 쉬움
}

[CreateAssetMenu(fileName = "QuestSO", menuName = "ScriptableObject/QuestSO")]
public class QuestSO : ScriptableObject
{
    public string QuestName; // 퀘스트 이름
    public string QuestDescription; // 퀘스트 설명
    public QuestTierType QuestTier; // 퀘스트 등급
    public float STRWeight; // 퀘스트 STR 가중치
    public float MAGWeight; // 퀘스트 MAG 가중치
    public float INSWeight; // 퀘스트 INS 가중치
    public float DEXWeight; // 퀘스트 DEX 가중치
    public float PowerForClear; // 기준 전투력, *가변
    public int FameReward; // 명성 보상, *가변
    public int GoldReward; // 골드 보상 *가변
    public int FamePenalty; // 명성 페널티 *가변
    public int GoldPenalty; // 골드 페널티 *가변
    public AdventurerStateType StateAfterFail; // 실패 후 모험가 부상 상태
    public int Days; // 퀘스트 소요 일수
    public bool IsQuesting = false; // 퀘스트 진행 중 여부
    public string QuestHint; // 퀘스트 힌트

    //가변 외 불변

}
