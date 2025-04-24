using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UI_FadeInOut : MonoBehaviour
{
    [SerializeField]
    private Image _fadeInOutImage;

    public void FadeIn(float duration)
    {
        _fadeInOutImage.DOFade(1.0f, duration);
    }

    public void FadeOut(float duration)
    {
        _fadeInOutImage.DOFade(0.0f, duration);
    }
}
