using UnityEngine;

public enum QuestTierType
{
    Red,//�ſ� �����
    Orange, // �����
    Yellow, // ����
    Blue, // ����
    Green //�ſ� ����
}

[CreateAssetMenu(fileName = "QuestSO", menuName = "ScriptableObject/QuestSO")]
public class QuestSO : ScriptableObject
{
    public string QuestName; // ����Ʈ �̸�
    public string QuestDescription; // ����Ʈ ����
    public QuestTierType QuestTier; // ����Ʈ ���
    public float STRWeight; // ����Ʈ STR ����ġ
    public float MAGWeight; // ����Ʈ MAG ����ġ
    public float INSWeight; // ����Ʈ INS ����ġ
    public float DEXWeight; // ����Ʈ DEX ����ġ
    public float PowerForClear; // ���� ������, *����
    public int FameReward; // �� ����, *����
    public int GoldReward; // ��� ���� *����
    public int FamePenalty; // �� ���Ƽ *����
    public int GoldPenalty; // ��� ���Ƽ *����
    public AdventurerStateType StateAfterFail; // ���� �� ���谡 �λ� ����
    public int Days; // ����Ʈ �ҿ� �ϼ�
    public bool IsQuesting = false; // ����Ʈ ���� �� ����
    public string QuestHint; // ����Ʈ ��Ʈ

    //���� �� �Һ�

}
