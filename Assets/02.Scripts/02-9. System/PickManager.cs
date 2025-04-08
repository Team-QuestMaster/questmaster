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

        List<bool> isChecked = new List<bool>();
        for (int i = 0; i < _questDatas.Count; i++)
        {
            isChecked.Add(false);
        }
        int count = 0;
        while (count < _questDatas.Count)
        {
            int randomIndex = UnityEngine.Random.Range(0, _questDatas.Count);
            QuestSO questSO = _questDatas[randomIndex];
            if (!questSO.IsQuesting)
            {
                return questSO;
            }
            else if (!isChecked[randomIndex])
            {
                isChecked[randomIndex] = true;
                count++;
            }
        }
        return null;
    }
}
