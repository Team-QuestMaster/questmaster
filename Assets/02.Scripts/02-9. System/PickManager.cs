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
    private List<Quest> _quests = new List<Quest>();
    public List<Quest> Quests { get => _quests; set => _quests = value; }
    protected override void Awake()
    {
        base.Awake();
    }
    public (Adventurer, Quest) Pick()
    {
        Adventurer currentAdventurer = PickAdventurer();
        Quest currentQuest = PickQuest();
        if (ReferenceEquals(currentAdventurer, null) || ReferenceEquals(currentQuest, null))
        {
            Debug.Log("현재 뽑아올 수 있는 유효한 모험가 또는 퀘스트가 없습니다.");
            return (null, null);
        }
        return (currentAdventurer, currentQuest);
    }
    private Adventurer PickAdventurer()
    {
        if (_adventurers.Count == 0)
        {
            return null;
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
            count++;
        }
        return null;
    }

    private Quest PickQuest()
    {
        if (_quests.Count == 0)
        {
            return null;
        }

        int count = 0;
        while (count < _quests.Count)
        {
            int randomIndex = UnityEngine.Random.Range(0, _quests.Count);
            Quest quest = _quests[randomIndex];
            if (!quest.QuestData.IsQuesting)
            {
                return quest;
            }
            count++;
        }
        return null;
    }
}
