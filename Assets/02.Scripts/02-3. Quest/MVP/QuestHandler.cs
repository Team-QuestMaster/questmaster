using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestHandler : MonoBehaviour
{
    private int _currentQuestIndex = 0;
    private List<QuestData> _questDatas = new List<QuestData>();
    public List<QuestData> QuestDatas { get => _questDatas; set => _questDatas = value; }

    private QuestModel _quest;
    public QuestModel Quest 
    { 
        get => _quest; 
        set
        {
            _quest = value; 
        }
    }
    private void Awake()
    {
        _quest = GetComponent<QuestModel>();
    }
    public void InitQuest()
    {
        _quest.QuestData = _questDatas[_currentQuestIndex];
    }

    public void SetQuest()
    {
        if (_questDatas.Count == 0) 
        {
            Debug.Log("QuestDatas가 비었습니다. Pick이 제대로 되지 않았습니다.");
            return;
        }
        _currentQuestIndex++;
        if (_questDatas.Count <= _currentQuestIndex)
        {
            _currentQuestIndex = 0;
        }
        _quest.QuestData = _questDatas[_currentQuestIndex];
    }

    public void ClearQuests()
    {
        _questDatas.Clear();
        _currentQuestIndex = 0;
    }
}
