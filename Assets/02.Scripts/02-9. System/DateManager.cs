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
