using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingUI : MonoBehaviour
{
    [SerializeField] private Image _settingBackground;
    [SerializeField] private Button _closeSettingButton;
    [SerializeField] private Button _settingButton;
    [SerializeField] private Image _fadeOutImage;
    [SerializeField] private Slider _bgmSlider;
    [SerializeField] private Slider _sfxSlider;

    public void Initialize()
    {
        _settingButton.onClick.AddListener(ShowSetting);
        _closeSettingButton.onClick.AddListener(HideSetting);

        // 볼륨 설정 0~1
        _bgmSlider.wholeNumbers = false;
        _bgmSlider.minValue = 0f;
        _bgmSlider.maxValue = 1f;
        _bgmSlider.value = 1;
        _bgmSlider.onValueChanged.AddListener(volume => AudioManager.Instance.SetBGMVolume(volume));
        
        _sfxSlider.wholeNumbers = false;
        _sfxSlider.minValue = 0f;
        _sfxSlider.maxValue = 1f;
        _sfxSlider.value = 1;
        _sfxSlider.onValueChanged.AddListener(volume => AudioManager.Instance.SetSFXVolume(volume));
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

    // 게임 종료 버튼 함수
    private void OnEndGame()
    {
        Application.Quit();
    }
}