using System;
using System.Collections;
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

    private Adventurer _currentAdventurer;
    public Adventurer CurrentAdventurer { get => _currentAdventurer; }
    private Quest _currentQuest;
    public Quest CurrentQuest { get => _currentQuest; }

    private const int _visitedAdventurerCountMax = 5; // �Ϸ翡 �湮 ������ �ִ� ���谡�� ��
    private int _visitedAdventurerCount = 0; // ���� �湮�� ���谡�� ��
    public int VisitedAdventurerCount
    {
        get => _visitedAdventurerCount;
        set => _visitedAdventurerCount = value;
    }

    public event Action OnVisitedAdventurerCountIncreased;

    protected override void Awake()
    {
        base.Awake();
    }
    public (Adventurer, Quest) Pick()
    {
        if (_visitedAdventurerCountMax <= _visitedAdventurerCount)
        {
            Debug.Log("�� �̻� (���谡, ����Ʈ) ���� ���� �� �����ϴ�.");
            return (null, null);

        }
        _currentAdventurer = PickAdventurer();
        _currentQuest = PickQuest();
        if (ReferenceEquals(_currentAdventurer, null) || ReferenceEquals(_currentQuest, null))
        {
            Debug.Log("���� �̾ƿ� �� �ִ� ���谡 �Ǵ� ����Ʈ�� �����ϴ�.");
            return (null, null);
        }

        _visitedAdventurerCount++;
        OnVisitedAdventurerCountIncreased?.Invoke();
        return (_currentAdventurer, _currentQuest);
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
            Adventurer adventurer = _adventurers.ElementAt(randomIndex).Value;
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
            Quest quest = _quests.ElementAt(randomIndex).Value;
            if (!quest.QuestData.IsQuesting)
            {
                return quest;
            }
            count++;
        }
        return null;
    }
    public bool IsVisitedAdventuererCountMax()
    {
        return _visitedAdventurerCount == _visitedAdventurerCountMax;
    }
}
