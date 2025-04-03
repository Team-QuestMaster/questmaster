using UnityEngine;

public enum AdventurerTierType
{
    A,//���� ����
    B,
    C,
    D // ���� ����
}

public enum AdventurerStateType
{
    Idle,// �⺻ ����
    Injured, // �λ�
    Dead // ���
}

public enum AdventurerType
{
    Major, // �ֿ� ���谡
    Minor // ���� ���谡
}


[CreateAssetMenu(fileName = "AdventurerSO", menuName = "ScriptableObject/AdventurerSO")]
public class AdventurerSO : ScriptableObject
{
    public AdventurerType AdventurerType; // ���谡 Ÿ��
    public int OriginalSTR; // �ٷ�
    public int OriginalMAG; // ����
    public int OriginalINS; //������
    public int OriginalDEX; // ������
}
