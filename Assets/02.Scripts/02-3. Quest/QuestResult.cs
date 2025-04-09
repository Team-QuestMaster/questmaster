using UnityEngine;

public class QuestResult
{
    private AdventurerData _adventurerData;
    public AdventurerData AdventurerData { get => _adventurerData; set => _adventurerData = value; }
    private QuestData _questData;
    public QuestData QuestData { get => _questData; set => _questData = value; }
    private bool _isSuccess;
    public bool IsSuccess { get => _isSuccess; set => _isSuccess = value; }

    private float _probability;
    public float Probability { get => _probability; set => _probability = value; }

    public QuestResult(AdventurerData adventurerData, QuestData questData, bool isSuccess, float probability)
    {
        _adventurerData = adventurerData;
        _questData = questData;
        _isSuccess = isSuccess;
        _probability = probability;
    }
}