using UnityEngine;

public class AdventurerData
{
    private int _currentSTR;
    public int CurrentSTR { get => _currentSTR; set => _currentSTR = value; } // ���� �ٷ�
    private int _currentMAG;
    public int CurrentMAG { get => _currentMAG; set => _currentMAG = value; } // ���� ����
    private int _currentINS;
    public int CurrentINS { get => _currentINS; set => _currentINS = value; } // ���� ������
    private int _currentDEX;
    public int CurrentDEX { get => _currentDEX; set => _currentDEX = value; } // ���� ������
    private string _adventurerName;  // �̸�
    public string AdventurerName { get => _adventurerName; set => _adventurerName = value; }

    private string _adventurerClass; // ����
    public string AdventurerClass { get => _adventurerClass; set => _adventurerClass = value; }

    private string _adventurerTitle; //  Īȣ
    public string AdventurerTitle { get => _adventurerTitle; set => _adventurerTitle = value; }

    private AdventurerTierType _adventurerTier; // ���谡 ���
    public AdventurerTierType AdventurerTier { get => _adventurerTier; set => _adventurerTier = value; }

    private AdventurerStateType _adventurerState; //���谡 ����
    public AdventurerStateType AdventurerState { get => _adventurerState; set => _adventurerState = value; }

}
