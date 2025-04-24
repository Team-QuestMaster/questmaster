using System;
using System.Collections.Generic;
using UnityEngine;

public class PickManager : Singleton<PickManager>
{
    [SerializeField]
    private List<Adventurer> _adventurers = new List<Adventurer>();
    public List<Adventurer> Adventurers { get => _adventurers; set => _adventurers = value; }
    [SerializeField]
    private List<QuestData> _questDatas = new List<QuestData>();
    public List<QuestData> QuestDatas { get => _questDatas; set => _questDatas = value; }

    private AdventurerDataPool _adventurerDataPool;
    public AdventurerDataPool AdventurerDataPool { get => _adventurerDataPool; set => _adventurerDataPool = value; }

    [SerializeField]
    private MinorASO _adventurerSO;
    public MinorASO AdventurerSO { get => _adventurerSO; set => _adventurerSO = value; }

    private Dictionary<QuestTierType, (int start, int end)> tierDateRange = new Dictionary<QuestTierType, (int, int)>{
            { QuestTierType.Green, (1, 8) },
            { QuestTierType.Blue, (7, 13) },
            { QuestTierType.Yellow, (12, 18) },
            { QuestTierType.Orange, (17, 25) },
            { QuestTierType.Red, (24, 30) }};
    private Dictionary<QuestTierType, (float min, float max)> tierMinMaxProbability = new Dictionary<QuestTierType, (float min, float max)>{
            { QuestTierType.Green, (50f, 95f) },
            { QuestTierType.Blue, (50f, 95f) },
            { QuestTierType.Yellow, (45f, 90f) },
            { QuestTierType.Orange, (40f, 90f) },
            { QuestTierType.Red, (30f, 90f) }};
    protected override void Awake()
    {
        base.Awake();
        _questDatas = LoadQuest();
        _adventurerDataPool = LoadAdventurder();
        foreach (QuestData questData in _questDatas)
        {
            questData.IsQuesting = false;
        }
    }

    private List<QuestData> LoadQuest()
    {
        List<QuestData> questList = new List<QuestData>();

        List<Dictionary<string, string>> csv = CSVReader.Read("QuestData");

        for(int i = 0; i < csv.Count; i++)
        {
            string questName = csv[i]["QuestName"];
            string questDescription = csv[i]["QuestDescription"];
            QuestTierType questTier = (QuestTierType)Enum.Parse(typeof(QuestTierType), csv[i]["QuestTier"]);

            float str = float.Parse(csv[i]["STRWeight"]);
            float mag = float.Parse(csv[i]["MAGWeight"]);
            float ins = float.Parse(csv[i]["INSWeight"]);
            float dex = float.Parse(csv[i]["DEXWeight"]);
            float power = float.Parse(csv[i]["PowerForClear"]);
            int fameReward = int.Parse(csv[i]["FameReward"]);
            int goldReward = int.Parse(csv[i]["GoldReward"]);
            int famePenalty = int.Parse(csv[i]["FamePenalty"]);
            int goldPenalty = int.Parse(csv[i]["GoldPenalty"]);
            AdventurerStateType failState = (AdventurerStateType)Enum.Parse(typeof(AdventurerStateType), csv[i]["QuestTier"]);
            int days = int.Parse(csv[i]["Days"]);
            string questHint = csv[i]["QuestHint"];

            QuestData questData = new QuestData(
                questName, questDescription, questTier,
                str, mag, ins, dex, power,
                fameReward, goldReward, famePenalty, goldPenalty,
                failState, days, questHint
            );
            questList.Add(questData);
        }
        return questList;
    }

    private AdventurerDataPool LoadAdventurder()
    {
        AdventurerDataPool adventurerDataPool = new AdventurerDataPool();
        List<Dictionary<string, string>> csv = CSVReader.Read("AdventurerData");
        for (int i = 0; i < csv.Count; i++)
        {
            if (string.IsNullOrEmpty(csv[i]["AdventurerName"]))
            {
                break;
            }
            adventurerDataPool.AdventurerNameList.Add(csv[i]["AdventurerName"]);
        }
        for (int i = 0; i < csv.Count; i++)
        {
            if (string.IsNullOrEmpty(csv[i]["AdventurerClass"]))
            {
                break;
            }
            adventurerDataPool.AdventurerClassList.Add(csv[i]["AdventurerClass"]);
        }
        for (int i = 0; i < csv.Count; i++)
        {
            if (string.IsNullOrEmpty(csv[i]["AdventurerTitle"]))
            {
                break;
            }
            adventurerDataPool.AdventurerTitleList.Add(csv[i]["AdventurerTitle"]);
        }
        for (int i = 0; i < csv.Count; i++)
        {
            if (string.IsNullOrEmpty(csv[i]["Dialog"]))
            {
                break;
            }
            string dialogSet = csv[i]["Dialog"];
            DialogSet dialog = new DialogSet();
            dialog.Dialog = new List<string>(dialogSet.Split("|"));
            adventurerDataPool.DialogList.Add(dialog);
        }

        adventurerDataPool.OrignSTR = _adventurerSO.OriginalSTR;
        adventurerDataPool.OrignMAG = _adventurerSO.OriginalMAG;
        adventurerDataPool.OrignINS = _adventurerSO.OriginalINS;
        adventurerDataPool.OrignDEX = _adventurerSO.OriginalDEX;

        for(int i = 0; i < _adventurerSO.AdventurerSpriteLDList.Count; i++)
        {
            adventurerDataPool.AdventurerSpriteLDList.Add(_adventurerSO.AdventurerSpriteLDList[i]);
        }



        return adventurerDataPool;
    }

    public (Adventurer, QuestData) Pick()
    {
        Adventurer currentAdventurer = PickAdventurer();
        QuestData currentQuestData = PickQuest();
        if (ReferenceEquals(currentAdventurer, null) || ReferenceEquals(currentQuestData, null))
        {
            Debug.Log("현재 뽑아올 수 있는 유효한 모험가 또는 퀘스트가 없습니다.");
            return (null, null);
        }
        return (currentAdventurer, currentQuestData);
    }
    private Adventurer PickAdventurer()
    {
        if (_adventurers.Count == 0)
        {
            return null;
        }

        List<bool> isChecked = new List<bool>();
        for (int i = 0; i < _adventurers.Count; i++)
        {
            isChecked.Add(false);
        }

        int count = 0;
        while (count < _adventurers.Count)
        {
            int randomIndex = UnityEngine.Random.Range(0, _adventurers.Count);
            Adventurer adventurer = _adventurers[randomIndex];
            if (adventurer.AdventurerData.AdventurerState == AdventurerStateType.Idle)
            {
                return adventurer;
            }
            else if (!isChecked[randomIndex])
            {
                isChecked[randomIndex] = true;
                count++;
            }
        }
        return null;
    }
    private QuestData PickQuest()
    {
        if (_questDatas.Count == 0)
        {
            return null;
        }

        List<bool> isChecked = new List<bool>(_questDatas.Count);
        for (int i = 0; i < _questDatas.Count; i++)
        {
            isChecked.Add(false);
        }

        int currentDate = DateManager.Instance.CurrentDate;
        float todayAdventurerPower = 400 * currentDate;

        int count = 0;
        while (count < _questDatas.Count)
        {
            int randomIndex = UnityEngine.Random.Range(0, _questDatas.Count);

            if (isChecked[randomIndex])
            {
                continue;
            }

            isChecked[randomIndex] = true;
            count++;
            QuestData questData = _questDatas[randomIndex];
            if (isQuestPickValid(currentDate, todayAdventurerPower, questData))
            {
                return questData;
            }
        }
        return null;
    }
    private bool isQuestPickValid(int currentDate, float todayAdventurerPower, QuestData questData)
    {
        var range = tierDateRange[questData.QuestTier];
        var minimumProbability = tierMinMaxProbability[questData.QuestTier].min;
        var maximumProbability = tierMinMaxProbability[questData.QuestTier].max;
        float probability = CalculateManager.Instance.CalculateProbability
            (todayAdventurerPower, questData.PowerForClear);
        return (!questData.IsQuesting && range.start <= currentDate && currentDate <= range.end
            && minimumProbability <= probability && probability <= maximumProbability
            && todayAdventurerPower < questData.PowerForClear);
    }
}
