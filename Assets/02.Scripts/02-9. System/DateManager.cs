using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TimePeriod
{
    Morning, // 2
    Afternoon, // 2
    Evening, // 1
    Night
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

    private List<List<QuestResult>> _questResults = new List<List<QuestResult>>(_maxDate + 1);

    public Action OnTimePeriodChanged; 
    // 오전, 낮, 저녁, 밤과 같이 시간대가 바뀌었을 때, Background 등의 변경을 위한 Action

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < _maxDate + 1; i++)
        {
            _questResults.Add(new List<QuestResult>());
        }
    }
    private void Start()
    {
        PickManager.Instance.OnVisitedAdventurerCountIncreased += ChangeDate;
        PickManager.Instance.OnVisitedAdventurerCountIncreased += ChangeTimePeriod;
    }
    public void AddQuestResultToList(ref Adventurer adventurer, ref Quest quest, bool isSuccess)
    {
        int endDay = quest.QuestData.Days + _currentDate;
        if (_maxDate < endDay)
        {
            return;
        }
        _questResults[endDay].Add(new QuestResult(ref adventurer, ref quest, isSuccess));
    }
    public List<QuestResult> GetTodayQuestResults()
    {
        return _questResults[_currentDate];
    }
    private void ChangeDate()
    {
        if (PickManager.Instance.IsVisitedAdventuererCountMax())
        {
            _currentDate++;
            _currentTimePeriod = TimePeriod.Morning;
            PickManager.Instance.VisitedAdventurerCount = 0;
        }
    }
    private void ChangeTimePeriod()
    {
        int currentVisitedAdventurerCount = PickManager.Instance.VisitedAdventurerCount;
        if (currentVisitedAdventurerCount <= 2)
        {
            _currentTimePeriod = TimePeriod.Morning;
        }
        else if (currentVisitedAdventurerCount <= 4)
        {
            _currentTimePeriod = TimePeriod.Afternoon;
        }
        else
        {
            _currentTimePeriod = TimePeriod.Evening;
        }
    }
    private void BeginNightEvent()
    {
        _currentTimePeriod = TimePeriod.Night;
    }
}
