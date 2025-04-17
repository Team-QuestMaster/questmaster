using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    private QuestData _questData;
    public QuestData QuestData { get => _questData; set => _questData = value; }

    private Color _questTierImageColor;
    public Color QuestTierImageColor { get => _questTierImageColor; set => _questTierImageColor = value; }

    private void Start()
    {
       // InitQuestData();
        gameObject.SetActive(false);
    }
    private void InitQuestData()
    {
        _questTierImageColor = InitQuestTierImageColor();
    }
    public void ChangeQuestData(QuestData questData)
    {
        _questData = questData;
        InitQuestData();
    }
    private Color InitQuestTierImageColor()
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
