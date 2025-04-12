using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShowQuest : MonoBehaviour
{
    public event Action QuestAppearShow;
    public event Action QuestDisappearShow;
    [Header("Audio")]
    [SerializeField]
    private AudioClip _onAppearAudioClip;
    
    private void Start()
    {
        QuestDisappearShow += QuestDisappear;
    }

    public void AppearEventSet()
    {
        QuestAppearShow += UIManager.Instance.QuestUI.Initialize;
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
        UIManager.Instance.QuestUI.SmallQuestGO.GetComponent<Image>().DOFade(1, 0.5f);
        UIManager.Instance.QuestUI.BigQuestPaperGO.GetComponent<CanvasGroup>().DOFade(1, 0.5f);
    }
    private void QuestDisappear()
    {

        UIManager.Instance.QuestUI.SmallQuestGO.GetComponent<Image>().rectTransform.DOShakeRotation(0.3f, new Vector3(0,0,0.3f)).SetEase(Ease.InBack);
        UIManager.Instance.QuestUI.SmallQuestGO.GetComponent<Image>().DOFade(0, 0.5f)
            .OnComplete(() => UIManager.Instance.QuestUI.SmallQuestGO.SetActive(false));


        UIManager.Instance.QuestUI.BigQuestPaperGO.GetComponent<Image>().rectTransform.DOShakeRotation(0.3f, new Vector3(0,0,0.3f)).SetEase(Ease.InBack);
        UIManager.Instance.QuestUI.BigQuestPaperGO.GetComponent<CanvasGroup>().DOFade(0, 0.5f)
            .OnComplete(() =>
            {
                UIManager.Instance.QuestUI.BigQuestPaperGO.SetActive(false);
                UIManager.Instance.QuestUI.ChangeQuest();
            });
    }
}
