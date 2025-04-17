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
        foreach (QuestData questData in _questDatas)
        {
            questData.IsQuesting = false;
        }
    }

    private List<QuestData> LoadQuest()
    {
        List<QuestData> questList = new List<QuestData>();

        List<Dictionary<string, object>> csv = CSVReader.Read("QuestData");

        for(int i = 0; i < csv.Count; i++)
        {
            string questName = csv[i]["QuestName"].ToString();
            string questDescription = csv[i]["QuestDescription"].ToString();
            QuestTierType questTier = (QuestTierType)Enum.Parse(typeof(QuestTierType), csv[i]["QuestTier"].ToString());

            float str = float.Parse(csv[i]["STRWeight"].ToString());
            float mag = float.Parse(csv[i]["MAGWeight"].ToString());
            float ins = float.Parse(csv[i]["INSWeight"].ToString());
            float dex = float.Parse(csv[i]["DEXWeight"].ToString());
            float power = float.Parse(csv[i]["PowerForClear"].ToString());
            int fameReward = int.Parse(csv[i]["FameReward"].ToString());
            int goldReward = int.Parse(csv[i]["GoldReward"].ToString());
            int famePenalty = int.Parse(csv[i]["FamePenalty"].ToString());
            int goldPenalty = int.Parse(csv[i]["GoldPenalty"].ToString());
            AdventurerStateType failState = (AdventurerStateType)Enum.Parse(typeof(AdventurerStateType), csv[i]["QuestTier"].ToString());
            int days = int.Parse(csv[i]["Days"].ToString());
            string questHint = csv[i]["QuestHint"].ToString();

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
