using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class BigQuestPaperContent
{
    public Image SealingImage;
    public TextMeshProUGUI TitleTMP;
    public TextMeshProUGUI MainBodyTMP;
    public TextMeshProUGUI RewardTMP;
    public TextMeshProUGUI NeedTimeTMP;
}

public class UI_QuestView : MonoBehaviour
{
    [SerializeField]
    private BigQuestPaperContent bigQuestPaperContent;

    public void UpdateUI(QuestData questData, Color questTierImageColor)
    {
        bigQuestPaperContent.SealingImage.color = questTierImageColor;
        bigQuestPaperContent.TitleTMP.text = questData.QuestName.Replace("- ", "\n");
        bigQuestPaperContent.MainBodyTMP.text = questData.QuestDescription;
        bigQuestPaperContent.RewardTMP.text = $"골드 {questData.GoldReward}\n명성치 {questData.FameReward}";
        bigQuestPaperContent.NeedTimeTMP.text = $"소요시간 {questData.Days}일";
    }
} 