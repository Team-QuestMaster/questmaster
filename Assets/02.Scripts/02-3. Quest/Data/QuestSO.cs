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
    public string QuestName; // ����Ʈ �̸�
    public string QuestDescription; // ����Ʈ ����
    public QuestTierType QuestTier;
    public float STRWeight; // ����Ʈ �ٷ� ����ġ
    public float MAGWeight; // ����Ʈ MAG ����
    public float INSWeight; // ����Ʈ INS ����
    public float DEXWeight; // ����Ʈ DEX ����
    public float StatForClear; // ���� ������
    public int FameReward; // �� ����
    public int GoldReward; // ��� ����
    public int FamePenalty; // �� ���Ƽ
    public int GoldPenalty; // ��� ���Ƽ
    public AdventurerInjuryStateType StateAfterFail; // ���� �� ���谡 �λ� ����
    public int Days; // ����Ʈ �ҿ� �ϼ�

}
