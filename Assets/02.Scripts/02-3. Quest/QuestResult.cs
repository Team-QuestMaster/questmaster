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