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
    private List<int> _nightEventDates = new List<int> { 1,2, 5, 10, 15, 20, 25 };
    public List<int> NightEventDates { get => _nightEventDates; set => _nightEventDates = value; }

    private List<List<QuestResult>> _questResults = new List<List<QuestResult>>(_maxDate + 1);

    [SerializeField]
    private int _endingDate = 4; // 엔딩 날짜, 해당 날짜 이전까지만 진행합니다. 즉 _endingDate일차는 진행되지 않습니다.
    public int EndingDate { get => _endingDate; set => _endingDate = value; }


    public Action OnDateChanged; // 날짜가 바뀌었을 때 이벤트 처리를 위한 Action

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
        if (!CheckNightEventDate()) // 매일 밤 이벤트인지 체크
        {
            _currentDate++;
            OnDateChanged?.Invoke();
        }
    }

    public void ChangeDateInNight()
    {
        _currentDate++;
        OnDateChanged?.Invoke();
    }

    private bool CheckNightEventDate()
    {
        bool isNightEventDay = _nightEventDates.Contains(_currentDate);
        NightEventManager.Instance.IsNightEventDay = isNightEventDay;
        return isNightEventDay;
    }
    // 오늘 완료된 퀘스트 결과 리스트를 반환하는 메서드
    public List<QuestResult> GetTodayQuestResults()
    {
        return _questResults[_currentDate];
    }
    // 오늘을 기준으로 진행중인 퀘스트 리스트(2차원)를 반환하는 메서드
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
