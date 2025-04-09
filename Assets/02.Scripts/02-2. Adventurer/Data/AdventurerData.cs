using UnityEngine;

public class AdventurerData
{
    private AdventurerType _adventurerType; // 모험가 타입 (Major, Minor)
    public AdventurerType AdventurerType { get => _adventurerType; set => _adventurerType = value; }

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

    private DialogSet _dialogSet; // 대사
    public DialogSet DialogSet { get => _dialogSet; set => _dialogSet = value; } // 대사

    private Sprite _spriteSD; // 모험가의 SD 스프라이트
    public Sprite SpriteSD { get => _spriteSD; set => _spriteSD = value; }

    private Sprite _spriteLD; // 모험가의 LD 스프라이트
    public Sprite SpriteLD { get => _spriteLD; set => _spriteLD = value; }


    public AdventurerData(AdventurerType adventurerType, string adventurerName, string adventurerClass, string adventurerTitle,
        AdventurerTierType adventurerTier, int originalSTR, int originalMAG, int originalINS, int originalDEX,
        AdventurerStateType adventurerState, DialogSet dialogSet, Sprite spriteSD, Sprite spriteLD)
    {
        _adventurerType = AdventurerType;
        _adventurerName = adventurerName;
        _adventurerClass = adventurerClass;
        _adventurerTitle = adventurerTitle;
        _adventurerTier = adventurerTier;
        _currentSTR = originalSTR;
        _currentMAG = originalMAG;
        _currentINS = originalINS;
        _currentDEX = originalDEX;
        _adventurerState = adventurerState;
        _dialogSet = dialogSet;
        _spriteSD = spriteSD;
        _spriteLD = spriteLD;
    }

    public AdventurerData Clone()
    {
        return new AdventurerData(
            _adventurerType,
            _adventurerName,
            _adventurerClass,
            _adventurerTitle,
            _adventurerTier,
            _currentSTR,
            _currentMAG,
            _currentINS,
            _currentDEX,
            _adventurerState,
            _dialogSet,
            _spriteSD,
            _spriteLD
        );
    }
}
