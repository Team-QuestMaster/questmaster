using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private Adventurer _adventurer;
    [SerializeField] private Animator _characterAnimator;
    [SerializeField] private Button _characterButton;
    [SerializeField] private TextMeshProUGUI _characterText;
    [SerializeField] private Image _speechBubble;
    [SerializeField] private Button _speechButton;

    private AdventurerData _characterData;
    private int _dialogIndex = 0;

    public void Initialize()
    {
        _characterData = _adventurer.AdventurerData;
        _characterText.text = "";
        _characterButton.onClick.AddListener(ShowSpeechBubbleUI);
        _speechButton.onClick.AddListener(NextDialogue);
    }

    void ShowSpeechBubbleUI()
    {
        _speechBubble.gameObject.SetActive(true);
        _speechButton.interactable = true;
        _speechBubble.DOFade(1f, 0.1f);
        ShowDialogueUI(_dialogIndex);
    }

    void ShowButtonSpeechBubbleUI()
    {
        
    }

    void HideSpeechBubbleUI()
    {
        _speechBubble.DOFade(0f, 0.1f);
        _speechBubble.gameObject.SetActive(false);
        _speechButton.interactable = false;
    }

    void ShowDialogueUI(int i)
    {
        if (_dialogIndex < _characterData.DialogSet.Dialog.Count)
        {
            _characterText.text = _characterData.DialogSet.Dialog[i];
        }
        else
        {
            _characterText.text = "(더 이상 할 말이 없어 보인다.)";
        }
    }

    void NextDialogue()
    {
        if (_dialogIndex < _characterData.DialogSet.Dialog.Count)
        {
            _dialogIndex++;
            ShowDialogueUI(_dialogIndex);
        }
        else
        {
            HideSpeechBubbleUI();
        }
    }
}