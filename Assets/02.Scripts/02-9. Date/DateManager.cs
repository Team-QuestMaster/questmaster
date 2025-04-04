using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class QuestResult
{
    private Adventurer _adventurer;
    public Adventurer Adventurer { get => _adventurer; set => _adventurer = value; }
    private Quest _quest;
    public Quest Quest { get => _quest; set => _quest = value; }
    private bool _isSuccess;
    public bool IsSuccess { get => _isSuccess; set => _isSuccess = value; }
    public QuestResult(ref Adventurer adventurer, ref Quest quest, bool isSuccess)
    {
        _adventurer = adventurer;
        _quest = quest;
        _isSuccess = isSuccess;
    }
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
    private List<List<QuestResult>> _questResults = new List<List<QuestResult>>(_maxDate + 1);


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
}
