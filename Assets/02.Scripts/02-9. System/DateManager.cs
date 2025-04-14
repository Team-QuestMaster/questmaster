using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateManager : Singleton<DateManager>
{
    private const int _maxDate = 30;
    public static int MaxDate => _maxDate;
    private int _currentDate = 1;
    public int CurrentDate
    {
        get => _currentDate;
        set => _currentDate = Mathf.Clamp(value, 1, _maxDate);
    }
    private List<int> _nightEventDates = new List<int> { 3, 5, 10, 15, 20, 25 };
    public List<int> NightEventDates { get => _nightEventDates; set => _nightEventDates = value; }

    private List<List<QuestResult>> _questResults = new List<List<QuestResult>>(_maxDate + 1);

    [SerializeField]
    private int _endingDate = 30; // ���� ��¥, �ش� ��¥ ���������� �����մϴ�. �� _endingDate������ ������� �ʽ��ϴ�.
    public int EndingDate { get => _endingDate; set => _endingDate = value; }


    public Action OnDateChanged; // ��¥�� �ٲ���� �� �̺�Ʈ ó���� ���� Action

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < _maxDate + 1; i++)
        {
            _questResults.Add(new List<QuestResult>());
        }
        OnDateChanged += (() => CheckEndingDate());
    }
    public void AddQuestResultToList(Adventurer adventurer, Quest quest, bool isSuccess, float probability)
    {
        int endDay = quest.QuestData.Days + _currentDate;
        if (_maxDate < endDay)
        {
            return;
        }
        _questResults[endDay].Add(new QuestResult
            (adventurer.AdventurerData.Clone(), quest.QuestData.Clone(), isSuccess, probability));
    }
    public void ChangeDate(int currentRequestCount, int requestCountMax)
    {
        if (currentRequestCount != requestCountMax)
        {
            return;
        }
        if (!CheckNightEventDate()) // ���� �� �̺�Ʈ���� üũ
        {
            _currentDate++;
            OnDateChanged?.Invoke();
        }
    }

    public void ChangeDateInNight()
    {
        _currentDate++;
        OnDateChanged?.Invoke();
        AudioManager.Instance.PlayBGM(AudioManager.Instance.BgmClip);
    }

    private bool CheckNightEventDate()
    {
        bool isNightEventDay = _nightEventDates.Contains(_currentDate);
        NightEventManager.Instance.IsNightEventDay = isNightEventDay;
        return isNightEventDay;
    }
    // ���� �Ϸ�� ����Ʈ ��� ����Ʈ�� ��ȯ�ϴ� �޼���
    public List<QuestResult> GetTodayQuestResults()
    {
        return _questResults[_currentDate];
    }
    // ������ �������� �������� ����Ʈ ����Ʈ(2����)�� ��ȯ�ϴ� �޼���
    public List<List<QuestResult>> GetAllQuestResultsInProgress()
    {
        List<List<QuestResult>> questResultsInProgress = new List<List<QuestResult>>();

        for (int i = 0; i < _maxDate + 1; i++)
        {
            questResultsInProgress.Add(new List<QuestResult>());
        }

        for (int i = _currentDate + 1; i <= _maxDate; i++)
        {
            if (0 < _questResults[i].Count)
            {
                foreach (QuestResult questResult in _questResults[i])
                {
                    questResultsInProgress[i].Add(questResult);
                }
            }
        }
        return questResultsInProgress;
    }
    private void CheckEndingDate()
    {
        if (_currentDate == _endingDate)
        {
            SceneChangeManager.Instance.LoadScene(nameof(SceneNameEnum.EndingScene));
        }
    }
}
