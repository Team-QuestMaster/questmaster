using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShowQuest : MonoBehaviour
{
    public MiniCharacterUI MiniUI;
    public event Action QuestAppearShow;
    public event Action QuestDisappearShow;
    private void Start()
    {
        QuestAppearShow += UIManager.Instance.QuestUI.Initialize;
        QuestAppearShow += QuestAppear;
        QuestDisappearShow += QuestDisappear;
        MiniUI.MoveToTarvenInEvent += Appear;
        Appear();
    }
    public void Appear()
    {
        QuestAppearShow?.Invoke();
    }
    public void Disappear()
    {
        QuestDisappearShow?.Invoke();
    }
    private void QuestAppear()
    {
        UIManager.Instance.QuestUI.SmallQuestGO.GetComponent<Image>().DOFade(1, 1f);
        UIManager.Instance.QuestUI.BigQuestPaperGO.GetComponent<CanvasGroup>().DOFade(1, 1f);
    }
    private void QuestDisappear()
    {
        UIManager.Instance.QuestUI.SmallQuestGO.GetComponent<Image>().DOFade(0, 1f)
            .OnComplete(() => UIManager.Instance.QuestUI.SmallQuestGO.SetActive(false));

        UIManager.Instance.QuestUI.BigQuestPaperGO.GetComponent<CanvasGroup>().DOFade(0, 1f)
            .OnComplete(() =>
            {
                UIManager.Instance.QuestUI.BigQuestPaperGO.SetActive(false);
                UIManager.Instance.QuestUI.ChangeQuest();
            });
    }
}
