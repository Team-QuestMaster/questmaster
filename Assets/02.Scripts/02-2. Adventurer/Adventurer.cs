using UnityEngine;

public class Adventurer : MonoBehaviour
{
    [SerializeField]
    private AdventurerSO _adventurerSO;
    private AdventurerData _adventurerData;
    public AdventurerData AdventurerData { get => _adventurerData; set => _adventurerData = value; }

    private void Awake()
    {
        InitAdventurerData();
    }
    private void InitAdventurerData()
    {
        if (_adventurerSO.AdventurerType == AdventurerType.Major)
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
            AdventurerStateType.Idle
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

        AdventurerTierType adventurerTier
            = (AdventurerTierType)Random.Range((int)AdventurerTierType.A, (int)AdventurerTierType.D + 1);

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
            AdventurerStateType.Idle
        );
    }
       
}
