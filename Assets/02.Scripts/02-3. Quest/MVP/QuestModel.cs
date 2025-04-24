using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestModel : MonoBehaviour
{
    public Action<QuestData, Color> OnQuestDataChanged;
    private QuestData _questData;
    public QuestData QuestData 
    { 
        get => _questData;
        set
        {
            _questData = value;
            _questTierImageColor = SetQuestTierImageColor();
            OnQuestDataChanged?.Invoke(_questData, _questTierImageColor);
        }
    }
    private Color _questTierImageColor;
    public Color QuestTierImageColor { get => _questTierImageColor; set => _questTierImageColor = value; }
    private Color SetQuestTierImageColor()
    {
        Color tierImageColor = Color.white;
        switch (_questData.QuestTier)
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
