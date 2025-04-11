using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickManager : Singleton<PickManager>
{
    [SerializeField]
    private List<Adventurer> _adventurers = new List<Adventurer>();
    public List<Adventurer> Adventurers { get => _adventurers; set => _adventurers = value; }
    [SerializeField]
    private List<QuestSO> _questDatas = new List<QuestSO>();
    public List<QuestSO> QuestDatas { get => _questDatas; set => _questDatas = value; }
    private Dictionary<QuestTierType, (int start, int end)> tierDateRange = new Dictionary<QuestTierType, (int, int)>{
            { QuestTierType.Green, (1, 8) },
            { QuestTierType.Blue, (7, 13) },
            { QuestTierType.Yellow, (12, 18) },
            { QuestTierType.Orange, (17, 25) },
            { QuestTierType.Red, (24, 30) }};
    private Dictionary<QuestTierType, float> tierMinimumProbability = new Dictionary<QuestTierType, float>{
            { QuestTierType.Green, 60f },
            { QuestTierType.Blue, 55f },
            { QuestTierType.Yellow, 50f },
            { QuestTierType.Orange, 45f },
            { QuestTierType.Red, 35f }};
    protected override void Awake()
    {
        base.Awake();
        foreach (QuestSO questSO in _questDatas)
        {
            questSO.IsQuesting = false;
        }
    }
    public (Adventurer, QuestSO) Pick()
    {
        Adventurer currentAdventurer = PickAdventurer();
        QuestSO currentQuestSO = PickQuest();
        if (ReferenceEquals(currentAdventurer, null) || ReferenceEquals(currentQuestSO, null))
        {
            Debug.Log("현재 뽑아올 수 있는 유효한 모험가 또는 퀘스트가 없습니다.");
            return (null, null);
        }
        return (currentAdventurer, currentQuestSO);
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
    private QuestSO PickQuest()
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
            QuestSO questSO = _questDatas[randomIndex];
            if (isQuestPickValid(currentDate, todayAdventurerPower, questSO))
            {

                return questSO;
            }
        }
        return null;
    }
    private bool isQuestPickValid(int currentDate, float todayAdventurerPower, QuestSO questSO)
    {
        var range = tierDateRange[questSO.QuestTier];
        var minimumProbability = tierMinimumProbability[questSO.QuestTier];
        float probability = CalculateManager.Instance.CalculateProbability
            (todayAdventurerPower, questSO.PowerForClear);
        return (!questSO.IsQuesting && range.start <= currentDate && currentDate <= range.end
            && minimumProbability <= probability 
            && todayAdventurerPower < questSO.PowerForClear);
    }
}
