using UnityEngine;
using UnityEngine.UI;

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
        InitAdventurerData();
        gameObject.SetActive(false);
    }
    private void InitAdventurerData()
    {
        if (_adventurerSO.AdventurerType == AdventurerType.Major || _adventurerSO.AdventurerType == AdventurerType.Dealer)
        {
            MajorASO majorAdventurerSO = (MajorASO)_adventurerSO;
            InitMajorTypeData(majorAdventurerSO);
        }
        else
        {
            MinorASO minorAdventurerSO = (MinorASO)_adventurerSO;
            InitMinorTypeData(minorAdventurerSO);
        }
    }
    private void InitMajorTypeData(MajorASO majorAdventurerSO)
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
    private void InitMinorTypeData(MinorASO minorAdventurerSO)
    {
        string adventurerName 
            = minorAdventurerSO.AdventurerNameList[Random.Range(0, minorAdventurerSO.AdventurerNameList.Count)];
        string adventurerClass
            = minorAdventurerSO.AdventurerClassList[Random.Range(0, minorAdventurerSO.AdventurerClassList.Count)];
        string adventurerTitle
            = minorAdventurerSO.AdventurerTitleList[Random.Range(0, minorAdventurerSO.AdventurerTitleList.Count)];
        DialogSet dialogSet
            = minorAdventurerSO.DialogList[Random.Range(0, minorAdventurerSO.DialogList.Count)];
        AdventurerTierType adventurerTier = AdventurerTierType.D;
        Sprite spritesLD
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
            minorAdventurerSO.AdventurerSpriteSD,
            spritesLD
        );
        GetComponent<Image>().sprite = _adventurerData.SpriteLD;
    }
    public void SetAdventurerData()
    {
        if (_adventurerSO.AdventurerType == AdventurerType.Major || _adventurerSO.AdventurerType == AdventurerType.Dealer)
        {
            MajorASO majorAdventurerSO = (MajorASO)_adventurerSO;
            SetMajorTypeData(majorAdventurerSO);
        }
        else
        {
            MinorASO minorAdventurerSO = (MinorASO)_adventurerSO;
            SetMinorTypeData(minorAdventurerSO);
        }
    }
    private void SetMajorTypeData(MajorASO majorAdventurerSO)
    {

    }
    private void SetMinorTypeData(MinorASO minorAdventurerSO)
    {
        _adventurerData.AdventurerType = AdventurerType.Minor;
        _adventurerData.AdventurerName = minorAdventurerSO.AdventurerNameList[Random.Range(0, minorAdventurerSO.AdventurerNameList.Count)];
        _adventurerData.AdventurerClass = minorAdventurerSO.AdventurerClassList[Random.Range(0, minorAdventurerSO.AdventurerClassList.Count)];
        _adventurerData.AdventurerTitle = minorAdventurerSO.AdventurerTitleList[Random.Range(0, minorAdventurerSO.AdventurerTitleList.Count)];
        _adventurerData.DialogSet = minorAdventurerSO.DialogList[Random.Range(0, minorAdventurerSO.DialogList.Count)];
        _adventurerData.SpriteLD = 
            minorAdventurerSO.AdventurerSpriteLDList[Random.Range(0, minorAdventurerSO.AdventurerSpriteLDList.Count)];
        GetComponent<Image>().sprite = _adventurerData.SpriteLD;
        _minerStatHandler.SetRandomStat(this, DateManager.Instance.CurrentDate);
        SetAdventurerTierOnPower();
    }
    private void SetAdventurerTierOnPower()
    {
        int currentPower = _adventurerData.CurrentSTR + _adventurerData.CurrentMAG + _adventurerData.CurrentINS + _adventurerData.CurrentDEX;
        AdventurerTierType adventurerTier = AdventurerTierType.D;
        if (currentPower < 3200)
        {
            adventurerTier = AdventurerTierType.D;
        }
        else if (3200 <= currentPower && currentPower < 6000)
        {
            adventurerTier = AdventurerTierType.C;
        }
        else if (6000 <= currentPower && currentPower < 8800)
        {
            adventurerTier = AdventurerTierType.C;
        }
        else
        {
            adventurerTier = AdventurerTierType.A;
        }
        _adventurerData.AdventurerTier = adventurerTier;
    }
}
