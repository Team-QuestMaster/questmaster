using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    [SerializeField]
    private QuestSO _questSO;
    public QuestSO QuestSO { get => _questSO; }
    private QuestData _questData;
    public QuestData QuestData { get => _questData; set => _questData = value; }
    [SerializeField]
    private List<int> _questTierMinCombatPowers = new List<int>();


    private void Awake()
    {
        InitQuestData();
        gameObject.SetActive(false);
    }
    private void InitQuestData()
    {
        _questSO.QuestTierImageColor = InitQuestTierImageColor();
        _questData = new QuestData(
            _questSO.QuestName,
            _questSO.QuestDescription,
            _questSO.QuestTier,
            _questSO.QuestTierImageColor,
            _questSO.STRWeight,
            _questSO.MAGWeight,
            _questSO.INSWeight,
            _questSO.DEXWeight,
            _questSO.PowerForClear,
            _questSO.FameReward,
            _questSO.GoldReward,
            _questSO.FamePenalty,
            _questSO.GoldPenalty,
            _questSO.StateAfterFail,
            _questSO.Days,
            _questSO.QuestHint
        );
    }
    public void ChangeQuestData(QuestSO questSO)
    {
        _questSO = questSO;
        InitQuestData();
    }
    private Color InitQuestTierImageColor()
    {
        Color tierImageColor = Color.white;
        switch (_questSO.QuestTier)
        {
            case QuestTierType.Green:
                tierImageColor = Color.green;
                break;
            case QuestTierType.Blue:
                tierImageColor = Color.blue;
                break;
            case QuestTierType.Yellow:
                tierImageColor = Color.yellow;
                break;
            case QuestTierType.Orange:
                tierImageColor = new Color(1f, 0.5f, 0f);
                break;
            case QuestTierType.Red:
                tierImageColor = Color.red;
                break;
            default:
                tierImageColor = Color.green;
                break;
        }
        return tierImageColor;
    }
}
