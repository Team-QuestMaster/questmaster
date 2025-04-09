using UnityEngine;

public class Adventurer : MonoBehaviour
{
    [SerializeField]
    private AdventurerSO _adventurerSO;
    public AdventurerSO AdventurerSO { get => _adventurerSO; set => _adventurerSO = value; }
    private AdventurerData _adventurerData;
    public AdventurerData AdventurerData { get => _adventurerData; set => _adventurerData = value; }
    [SerializeField]
    private MinorAdventurerStatHandler _minerStatHandler;
    [SerializeField]
    private Sprite _idCardSprite;
    public Sprite IdCardSprite { get => _idCardSprite; set => _idCardSprite = value; }

    private void Awake()
    {
        SetAdventurerData(1);
        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        SetAdventurerData(DateManager.Instance.CurrentDate);
    }
    private void OnDisable()
    {

    }
    private void SetAdventurerData(int currentDate)
    {
        if (_adventurerSO.AdventurerType == AdventurerType.Major || _adventurerSO.AdventurerType == AdventurerType.Dealer)
        {
            MajorASO majorAdventurerSO = (MajorASO)_adventurerSO;
            SetMajorTypeData(majorAdventurerSO);
        }
        else
        {
            MinorASO minorAdventurerSO = (MinorASO)_adventurerSO;
            SetMinorTypeData(minorAdventurerSO, currentDate);
        }
    }
    private void SetMajorTypeData(MajorASO majorAdventurerSO)
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
    private void SetMinorTypeData(MinorASO minorAdventurerSO, int currentDate)
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
        AdventurerSpritePair spritesPair
            = minorAdventurerSO.AdventurerSpritePairList[Random.Range(0, minorAdventurerSO.AdventurerSpritePairList.Count)];

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
            spritesPair.SDSprite,
            spritesPair.LDSprite
        );
        _minerStatHandler.SetRandomStat(this, currentDate);
    }
}
