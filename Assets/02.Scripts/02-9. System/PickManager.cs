using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickManager : Singleton<PickManager>
{
    [SerializeField]
    private Dictionary<string, Adventurer> _adventurers = new Dictionary<string, Adventurer>();
    public Dictionary<string, Adventurer> Adventurers { get => _adventurers; set => _adventurers = value; }

    [SerializeField]
    private Dictionary<string, Quest> _quests = new Dictionary<string, Quest>();
    public Dictionary<string, Quest> Quests { get => _quests; set => _quests = value; }

    protected override void Awake()
    {
        base.Awake();
    }
    public (Adventurer, Quest) Pick()
    {
        Adventurer currentAdventurer = TryPickAdventurer();
        Quest currentQuest = TryPickQuest();
        if (ReferenceEquals(currentAdventurer, null) || ReferenceEquals(currentQuest, null))
        {
            Debug.Log("현재 뽑아올 수 있는 유효한 모험가 또는 퀘스트가 없습니다.");
            return (null, null);
        }
        return (currentAdventurer, currentQuest);
    }
    private Adventurer TryPickAdventurer()
    {
        if (_adventurers.Count == 0)
        {
            return null;
        }

        int count = 0;
        while (count < _adventurers.Count)
        {
            int randomIndex = UnityEngine.Random.Range(0, _adventurers.Count);
            Adventurer adventurer = _adventurers.ElementAt(randomIndex).Value;
            if (adventurer.AdventurerData.AdventurerState == AdventurerStateType.Idle)
            {
                return adventurer;
            }
            count++;
        }
        return null;
    }
    private Quest TryPickQuest()
    {
        if (_quests.Count == 0)
        {
            return null;
        }

        int count = 0;
        while (count < _quests.Count)
        {
            int randomIndex = UnityEngine.Random.Range(0, _quests.Count);
            Quest quest = _quests.ElementAt(randomIndex).Value;
            if (!quest.QuestData.IsQuesting)
            {
                return quest;
            }
            count++;
        }
        return null;
    }
}
