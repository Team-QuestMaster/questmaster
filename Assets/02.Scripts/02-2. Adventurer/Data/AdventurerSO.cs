using System.Collections.Generic;
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
    Idle,// �⺻ ����(PickManager���� ���� �� ����)
    TodayCome, // ���� ����Ʈ�� �����Ϸ� ��
    Questing, // ����Ʈ ��
    Injured, // �λ�
    Dead // ���
}

public enum AdventurerType
{
    Major, // �ֿ� ���谡
    Minor // ���� ���谡
}

[System.Serializable]
public class DialogSet
{
    public List<string> Dialog;
}


[CreateAssetMenu(fileName = "AdventurerSO", menuName = "ScriptableObject/AdventurerSO")]
public class AdventurerSO : ScriptableObject
{
    public AdventurerType AdventurerType; // ���谡 Ÿ��
    public int OriginalSTR; // �ٷ�
    public int OriginalMAG; // ����
    public int OriginalINS; //������
    public int OriginalDEX; // ������
    public List<DialogSet> DialogList = new List<DialogSet>(); // ���
}
