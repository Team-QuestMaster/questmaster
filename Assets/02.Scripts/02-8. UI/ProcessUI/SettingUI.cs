using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private Image _settingBackground;
    [SerializeField] private Button _closeSettingButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Image _fadeOutImage;

    public void Initialize()
    {
        _settingButton.onClick.AddListener(ShowSetting);
        _closeSettingButton.onClick.AddListener(HideSetting);
    }

    void ShowSetting()
    {
        _settingBackground.gameObject.SetActive(true);
        BackFadeOn(0.5f);
        _settingBackground.transform.DOLocalMove(Vector3.zero, 0.5f);
    }

    void HideSetting()
    {
        
        BackFadeOff(0.5f);
        _settingBackground.transform.DOLocalMove(new Vector3(0, 1003, 0), 0.5f).OnComplete(() => _settingBackground.gameObject.SetActive(false));
    }

    void BackFadeOn(float time)
    {
        _fadeOutImage.DOFade(0.7f, time);
        _fadeOutImage.raycastTarget = true;
    }

    void BackFadeOff(float time)
    {
        _fadeOutImage.DOFade(0f, time);
        _fadeOutImage.raycastTarget = false;
    }
}