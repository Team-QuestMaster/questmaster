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
    public string AdventurerName;  // �̸�
    public string AdventurerClass; // ����
    public string AdventurerTitle; //  Īȣ
    public AdventurerTierType AdventurerTier; // Ƽ��
    public int OriginalStatSTR; // �ٷ�
    public int OriginalStatMAG; // ����
    public int OriginalStatINS; //������
    public int ORiginalStatDEX; // ������
    public AdventurerInjuryStateType AdventurerInjurySate;

}
