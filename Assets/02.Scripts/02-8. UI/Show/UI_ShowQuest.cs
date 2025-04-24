using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_ShowQuest : MonoBehaviour
{
    public event Action QuestAppearShow;
    public event Action QuestDisappearShow;
    [Header("Audio")]
    [SerializeField]
    private AudioClip _onAppearAudioClip;
    
    [SerializeField]
    private QuestHandler _questHandler;

    [SerializeField]
    private GameObject _smallQuestGO;
    public GameObject SmallQuestGO { get => _smallQuestGO; set => _smallQuestGO = value; }
    [SerializeField]
    private Transform _smallQuestActivateTransform;

    [SerializeField]
    private GameObject _bigQuestPaperGO;
    public GameObject BigQuestPaperGO { get => _bigQuestPaperGO; set => _bigQuestPaperGO = value; }

    private Transform _bigQuestPaperActivateTransform;
    
    private void Start()
    {
        QuestDisappearShow += QuestDisappear;
        _bigQuestPaperActivateTransform = _bigQuestPaperGO.transform;
        _smallQuestGO.transform.position = _smallQuestActivateTransform.position;
        _smallQuestGO.SetActive(true);
        _bigQuestPaperGO.transform.position = _bigQuestPaperActivateTransform.position;
    }

    public void AppearEventSet()
    {
        // QuestAppearShow += UIManager.Instance.QuestUI.InitializeUI;
        QuestAppearShow += QuestAppear;
    }

    public void Appear()
    {
        if (UIManager.Instance.CharacterUI.CurrentCharacter.GetComponent<Adventurer>().AdventurerSO.AdventurerType != AdventurerType.Dealer)
        {
            AudioManager.Instance.PlaySFX(_onAppearAudioClip);
            QuestAppearShow?.Invoke();
        }
    }

    public void Disappear()
    {
        QuestDisappearShow?.Invoke();
    }

    private void QuestAppear()
    {
        _smallQuestGO.transform.position = _smallQuestActivateTransform.position;
        _smallQuestGO.SetActive(true);
        _bigQuestPaperGO.transform.position = _bigQuestPaperActivateTransform.position;
        _smallQuestGO.GetComponent<Image>().DOFade(1, 0.5f);
        _bigQuestPaperGO.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
    }

    private void QuestDisappear()
    {
        _smallQuestGO.GetComponent<Image>().rectTransform.DOShakeRotation(0.3f, new Vector3(0,0,0.3f)).SetEase(Ease.InBack);
        _smallQuestGO.GetComponent<Image>().DOFade(0, 0.5f)
            .OnComplete(() => _smallQuestGO.SetActive(false));

        _bigQuestPaperGO.GetComponent<Image>().rectTransform.DOShakeRotation(0.3f, new Vector3(0,0,0.3f)).SetEase(Ease.InBack);
        _bigQuestPaperGO.GetComponent<CanvasGroup>().DOFade(0, 0.5f)
            .OnComplete(() =>
            {
                _bigQuestPaperGO.SetActive(false);
                _questHandler.SetQuest();
            });
    }
}
