using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShowQuest : MonoBehaviour
{
    public MiniCharacterUI MiniUI;
    public event Action QuestAppearShow;
    public event Action QuestDisappearShow;

    private void Awake()
    {
        QuestAppearShow += UIManager.Instance.QuestUI.Initialize;
        QuestAppearShow += QuestAppear;
        QuestDisappearShow += QuestDisappear;
        MiniUI.MoveToTarvenInEvent += Appear;
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
        UIManager.Instance.QuestUI.SmallQuestGO.GetComponent<Image>().DOFade(1, 1);
    }
    private void QuestDisappear()
    {
        UIManager.Instance.QuestUI.SmallQuestGO.SetActive(false);
    }
}
