using UnityEngine;

public class Adventurer : MonoBehaviour
{
    [SerializeField]
    private AdventurerSO _adventurerSO;
    private AdventurerData _adventurerData;
    public AdventurerData AdventurerData { get => _adventurerData; set => _adventurerData = value; }

    [SerializeField]
    private Sprite _idCardSprite;
    public Sprite IdCardSprite { get => _idCardSprite; set => _idCardSprite = value; }

    private void Awake()
    {
        InitAdventurerData();
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        
    }
    private void OnDisable()
    {

    }
    private void InitAdventurerData()
    {
        if (_adventurerSO.AdventurerType == AdventurerType.Major || _adventurerSO.AdventurerType == AdventurerType.Dealer)
        {
            MajorASO majorAdventurerSO = (MajorASO)_adventurerSO;
            InitMajorTypeData(ref majorAdventurerSO);
        }
        else
        {
            MinorASO minorAdventurerSO = (MinorASO)_adventurerSO;
            InitMinorTypeData(ref minorAdventurerSO);
        }
    }
    private void InitMajorTypeData(ref MajorASO majorAdventurerSO)
    {
        _adventurerData = new AdventurerData(
            majorAdventurerSO.AdventurerType,
            majorAdventurerSO.AdventurerName,
            majorAdventurerSO.AdventurerClass,
            majorAdventurerSO.AdventurerTitle,
            majorAdventurerSO.AdventurerTier,
            majorAdventurerSO.OriginalSTR,
            majorAdventurerSO.OriginalMAG,
            majorAdventurerSO.OriginalINS,
            majorAdventurerSO.OriginalDEX,
            AdventurerStateType.Idle,
            majorAdventurerSO.DialogList[0],
            majorAdventurerSO.SpriteSD,
            majorAdventurerSO.SpriteLD
        );
    }
    private void InitMinorTypeData(ref MinorASO minorAdventurerSO)
    {
        string adventurerName 
            = minorAdventurerSO.AdventurerNameList[Random.Range(0, minorAdventurerSO.AdventurerNameList.Count)];
        string adventurerClass
            = minorAdventurerSO.AdventurerClassList[Random.Range(0, minorAdventurerSO.AdventurerClassList.Count)];
        string adventurerTitle
            = minorAdventurerSO.AdventurerTitleList[Random.Range(0, minorAdventurerSO.AdventurerTitleList.Count)];
        DialogSet dialogSet
            = minorAdventurerSO.DialogList[Random.Range(0, minorAdventurerSO.DialogList.Count)];
        AdventurerTierType adventurerTier
            = (AdventurerTierType)Random.Range((int)AdventurerTierType.A, (int)AdventurerTierType.D + 1);
        Sprite spriteLD 
            = minorAdventurerSO.AdventurerSpriteLDList[Random.Range(0, minorAdventurerSO.AdventurerSpriteLDList.Count)];

        _adventurerData = new AdventurerData(
            minorAdventurerSO.AdventurerType,
            adventurerName,
            adventurerClass,
            adventurerTitle,
            adventurerTier,
            minorAdventurerSO.OriginalSTR,
            minorAdventurerSO.OriginalMAG,
            minorAdventurerSO.OriginalINS,
            minorAdventurerSO.OriginalDEX,
            AdventurerStateType.Idle,
            dialogSet,
            minorAdventurerSO.SpriteSD,
            spriteLD
        );
    }
       
}
