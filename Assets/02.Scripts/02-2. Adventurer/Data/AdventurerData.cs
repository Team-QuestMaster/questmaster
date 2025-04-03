using UnityEngine;

public class AdventurerData
{
    private int _currentSTR;
    public int CurrentSTR { get => _currentSTR; set => _currentSTR = value; } // 현재 근력
    private int _currentMAG;
    public int CurrentMAG { get => _currentMAG; set => _currentMAG = value; } // 현재 마력
    private int _currentINS;
    public int CurrentINS { get => _currentINS; set => _currentINS = value; } // 현재 통찰력
    private int _currentDEX;
    public int CurrentDEX { get => _currentDEX; set => _currentDEX = value; } // 현재 손재주
    private string _adventurerName;  // 이름
    public string AdventurerName { get => _adventurerName; set => _adventurerName = value; }

    private string _adventurerClass; // 직업
    public string AdventurerClass { get => _adventurerClass; set => _adventurerClass = value; }

    private string _adventurerTitle; //  칭호
    public string AdventurerTitle { get => _adventurerTitle; set => _adventurerTitle = value; }

    private AdventurerTierType _adventurerTier; // 모험가 등급
    public AdventurerTierType AdventurerTier { get => _adventurerTier; set => _adventurerTier = value; }

    private AdventurerStateType _adventurerState; //모험가 상태
    public AdventurerStateType AdventurerState { get => _adventurerState; set => _adventurerState = value; }

}
