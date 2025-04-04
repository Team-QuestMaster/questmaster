using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimePeriod
{
    Morning, // 2
    Afternoon, // 2
    Evening, // 1
    Night,


    Count
}


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
    private TimePeriod _currentTimePeriod = TimePeriod.Morning;
    public TimePeriod CurrentTimePeriod { get => _currentTimePeriod; }
    private List<int> _nightEventDates = new List<int> { 5, 10, 15, 20, 25 };
    public List<int> NightEventDates { get => _nightEventDates; set => _nightEventDates = value; }

    private List<List<QuestResult>> _questResults = new List<List<QuestResult>>(_maxDate + 1);

    public Action OnDateChanged; // 날짜가 바뀌었을 때 이벤트 처리를 위한 Action
    public Action OnTimePeriodChanged; // 오전, 낮, 저녁, 밤과 같이 시간대가 바뀌었을 때, Background 등의 변경을 위한 Action

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < _maxDate + 1; i++)
        {
            _questResults.Add(new List<QuestResult>());
        }
    }
    public void AddQuestResultToList(Adventurer adventurer, Quest quest, bool isSuccess, float probability)
    {
        int endDay = quest.QuestData.Days + _currentDate;
        if (_maxDate < endDay)
        {
            return;
        }
        _questResults[endDay].Add(new QuestResult(ref adventurer, ref quest, isSuccess, probability));
    }
    public void ChangeDate(int currentRequestCount, int requestCountMax)
    {
        if (currentRequestCount != requestCountMax)
        {
            return;
        }
        if (CheckNightEventDate())
        {
            // 알파 단계에서 NightEventHandler를 통해서 제어해주는게 좋을 것 같다.
            BeginNightEvent();
        }
        else
        {
            _currentDate++;
            _currentTimePeriod = TimePeriod.Morning;
            OnDateChanged?.Invoke();
        }
    }

    public void ChangeDateInNight()
    {
        _currentDate++;
        _currentTimePeriod = TimePeriod.Morning;
        OnDateChanged?.Invoke();
    }

    public void ChangeTimePeriod(int currentRequestCount, int requestCountMax)
    {
        if (currentRequestCount <= 2)
        {
            _currentTimePeriod = TimePeriod.Morning;
        }
        else if (currentRequestCount <= 4)
        {
            _currentTimePeriod = TimePeriod.Afternoon;
        }
        else
        {
            _currentTimePeriod = TimePeriod.Evening;
        }
        OnTimePeriodChanged?.Invoke();
    }

    private bool CheckNightEventDate()
    {
        return _nightEventDates.Contains(_currentDate);
    }
    public void BeginNightEvent()
    {
        _currentTimePeriod = TimePeriod.Night;
        OnTimePeriodChanged?.Invoke();
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
}
