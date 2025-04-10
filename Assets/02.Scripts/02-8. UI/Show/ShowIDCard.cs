using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShowIDCard : MonoBehaviour
{
    public event Action OnIDCardAppear;
    public event Action OnIDCardDisappear;
    private void Start()
    {
        OnIDCardAppear += IDCardAppear;
        OnIDCardDisappear += IDCardDisappear;
    }

    public void Appear()
    {
        OnIDCardAppear?.Invoke();
    }
    public void Disappear()
    {
        OnIDCardDisappear?.Invoke();
    }

    private void IDCardAppear()
    {
        UIManager.Instance.AdventurerIDCardUI.SmallIDCardGO.SetActive(true);
        UIManager.Instance.AdventurerIDCardUI.SmallIDCardGO
            .GetComponent<Image>().DOFade(1, 1f);
        UIManager.Instance.AdventurerIDCardUI.BigIDCardGO
            .GetComponent<CanvasGroup>().DOFade(1, 1f);
    }
    private void IDCardDisappear()
    {
        UIManager.Instance.AdventurerIDCardUI.SmallIDCardGO
            .GetComponent<Image>().DOFade(0, 1f)
            .OnComplete
            (() => UIManager.Instance.AdventurerIDCardUI.SmallIDCardGO.SetActive(false));

        UIManager.Instance.AdventurerIDCardUI.BigIDCardGO
            .GetComponent<CanvasGroup>().DOFade(0, 1f)
            .OnComplete(() =>
            {
                UIManager.Instance.AdventurerIDCardUI.BigIDCardGO.SetActive(false);
            });
    }
}
