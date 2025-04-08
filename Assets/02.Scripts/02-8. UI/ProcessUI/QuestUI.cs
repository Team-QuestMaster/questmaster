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
    private List<GameObject> _quests = new List<GameObject>();
    public List<GameObject> Quests { get => _quests; set => _quests = value; }
    private GameObject _currentQuest;
    public GameObject CurrentQuest { get => _currentQuest; set => _currentQuest = value; }

    [SerializeField]
    private GameObject _smallQuestGO;
    public GameObject SmallQuestGO { get => _smallQuestGO; set => _smallQuestGO = value; }
    [SerializeField]
    private GameObject _bigQuestPaperGO;
    public GameObject BigQuestPaperGO { get => _bigQuestPaperGO; set => _bigQuestPaperGO = value; }
    private Transform _bigQuestPaperActivateTransform;
    [SerializeField]
    private BigQuestPaperContent bigQuestPaperContent;
    [SerializeField]
    private Transform _smallQuestActivateTransform;
    private void Awake()
    {
        _bigQuestPaperActivateTransform = _bigQuestPaperGO.transform;
    }
    public void Initialize()
    {
        _currentQuest = _quests[_currentQuestIndex];
        _smallQuestGO.transform.position = _smallQuestActivateTransform.position;
        _smallQuestGO.SetActive(true);
        _bigQuestPaperGO.transform.position = _bigQuestPaperActivateTransform.position;
        InitializeBigQuestPaperContent();
    }
    private void InitializeBigQuestPaperContent()
    {
        Quest currentQuest = _currentQuest.GetComponent<Quest>();
        // SealingImage�� ���, QuestData�� QuestTier�� ���� �ٸ��� ������ �ʿ�
        // �׳� Image�� QuestData�� �־������?
        bigQuestPaperContent.TitleTMP.text = currentQuest.QuestData.QuestName;
        bigQuestPaperContent.MainBodyTMP.text = currentQuest.QuestData.QuestDescription;
        bigQuestPaperContent.RewardTMP.text = currentQuest.QuestData.GoldReward.ToString();
        bigQuestPaperContent.RewardTMP.text += " / " + currentQuest.QuestData.FameReward.ToString();
        bigQuestPaperContent.NeedTimeTMP.text = currentQuest.QuestData.Days.ToString();
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
            Debug.Log("�̾ƿ� ����Ʈ ����Ʈ�� ũ�Ⱑ ���� ���谡�� ����Ʈ ��û ������ �����ϴ�.");
        }
    }
}
