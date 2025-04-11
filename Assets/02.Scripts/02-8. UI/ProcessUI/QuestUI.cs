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


public class QuestUI : MonoBehaviour
{
    private int _currentQuestIndex = 0;
    private List<QuestSO> _questDatas = new List<QuestSO>();
    public List<QuestSO> QuestDatas { get => _questDatas; set => _questDatas = value; }
    private QuestSO _currentQuestData;
    private Quest _currentQuest;
    public Quest CurrentQuest { get => _currentQuest; }


    [SerializeField]
    private GameObject _smallQuestGO;
    public GameObject SmallQuestGO { get => _smallQuestGO; set => _smallQuestGO = value; }
    [SerializeField]
    private Transform _smallQuestActivateTransform;


    [SerializeField]
    private GameObject _bigQuestPaperGO;
    public GameObject BigQuestPaperGO { get => _bigQuestPaperGO; set => _bigQuestPaperGO = value; }

    private Transform _bigQuestPaperActivateTransform;
    [SerializeField]
    private BigQuestPaperContent bigQuestPaperContent;
    private void Awake()
    {
        _bigQuestPaperActivateTransform = _bigQuestPaperGO.transform;
        _currentQuest = _smallQuestGO.GetComponent<Quest>();
    }
    public void Initialize()
    {
        _currentQuestData = _questDatas[_currentQuestIndex];
        if (!ReferenceEquals(_currentQuest, null))
        {
            _currentQuest.ChangeQuestData(_currentQuestData);
        }
        _smallQuestGO.transform.position = _smallQuestActivateTransform.position;
        _smallQuestGO.SetActive(true);
        _bigQuestPaperGO.transform.position = _bigQuestPaperActivateTransform.position;
        InitializeBigQuestPaperContent();
    }
    private void InitializeBigQuestPaperContent()
    {
        // SealingImage�� ���, QuestData�� QuestTier�� ���� �ٸ��� ������ �ʿ�
        // �׳� Image�� QuestData�� �־������?
        bigQuestPaperContent.SealingImage.color = _currentQuest.QuestData.QuestTierImageColor;
        bigQuestPaperContent.TitleTMP.text = _currentQuestData.QuestName.Replace("- ", "\n");
        bigQuestPaperContent.MainBodyTMP.text = _currentQuestData.QuestDescription;
        bigQuestPaperContent.RewardTMP.text = $"<color=yellow>골드</color> {_currentQuestData.GoldReward} <color=#90FFEB>명성치</color> {_currentQuestData.FameReward}";
        bigQuestPaperContent.NeedTimeTMP.text = $"<color=#8B4513>소요시간</color> {_currentQuestData.Days}일";
    }

    public void ChangeQuest()
    {
        if (_currentQuestIndex < _questDatas.Count)
        {
            _currentQuestIndex++;
            if (_questDatas.Count <= _currentQuestIndex)
            {
                _currentQuestIndex = 0;
            }
            _currentQuestData = _questDatas[_currentQuestIndex];
        }
        else
        {
            Debug.Log("�̾ƿ� ����Ʈ ����Ʈ�� ũ�Ⱑ ���� ���谡�� ����Ʈ ��û ������ �����ϴ�.");
        }
    }
}
