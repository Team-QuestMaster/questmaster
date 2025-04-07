using System;
using System.Collections.Generic;
using TMPro;
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


public class QuestUI : MonoBehaviour
{
    private int _currentQuestIndex = 0;
    private List<Quest> _quests = new List<Quest>();
    public List<Quest> Quests { get => _quests; set => _quests = value; }
    private Quest _currentQuest;
    public Quest CurrentQuest { get => _currentQuest; set => _currentQuest = value; }

    [SerializeField]
    private GameObject _smallQuestGO;
    public GameObject SmallQuestGO { get => _smallQuestGO; set => _smallQuestGO = value; }
    [SerializeField]
    private GameObject _bigQuestPaperGO;
    public GameObject BigQuestPaperGO { get => _bigQuestPaperGO; set => _bigQuestPaperGO = value; }
    [SerializeField]
    private BigQuestPaperContent bigQuestPaperContent;
    [SerializeField]
    private Transform _smallQuestActivateTransform;

    public void Initialize()
    {
        _currentQuest = _quests[_currentQuestIndex];
        _smallQuestGO.transform.position = _smallQuestActivateTransform.position;
        _smallQuestGO.SetActive(true);
        _bigQuestPaperGO.SetActive(false);
        InitializeBigQuestPaperContent();
    }
    private void InitializeBigQuestPaperContent()
    {
        // SealingImage의 경우, QuestData의 QuestTier에 따라 다르게 설정할 필요
        // 그냥 Image도 QuestData에 넣어버릴까?
        bigQuestPaperContent.TitleTMP.text = _currentQuest.QuestData.QuestName;
        bigQuestPaperContent.MainBodyTMP.text = _currentQuest.QuestData.QuestDescription;
        bigQuestPaperContent.RewardTMP.text = _currentQuest.QuestData.GoldReward.ToString();
        bigQuestPaperContent.RewardTMP.text += " / " + _currentQuest.QuestData.FameReward.ToString();
        bigQuestPaperContent.NeedTimeTMP.text = _currentQuest.QuestData.Days.ToString();
    }

    public void ChangeQuest()
    {
        if (_currentQuestIndex < _quests.Count)
        {
            _currentQuest.gameObject.SetActive(false);
            _currentQuestIndex++;
            if (_quests.Count <= _currentQuestIndex)
            {
                _currentQuestIndex = 0;
            }
            _currentQuest = _quests[_currentQuestIndex];
        }
        else
        {
            Debug.Log("뽑아온 퀘스트 리스트의 크기가 일일 모험가의 퀘스트 요청 수보다 적습니다.");
        }
    }
}
