using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.Rendering;

public class CharacterUI : MonoBehaviour
{
    public List<GameObject> Characters;
    private int _currentCharacter = 0;
    public GameObject CurrentCharacter;

    
    [SerializeField] private Adventurer _adventurer;
    [SerializeField] private Animator _characterAnimator;
    [SerializeField] private Button _characterButton;
    [SerializeField] private TextMeshProUGUI _characterText;
    [SerializeField] private Image _speechBubble;
    [SerializeField] private Button _speechButton;

    [SerializeField] private Button _positiveButton;
    [SerializeField] private Button _negativeButton;

    [SerializeField] private Transform _characterActivateTransform;
    
    public event Action PositiveButtonEvent;
    public event Action NegativeButtonEvent;
    
    private AdventurerData _characterData;
    private int _dialogIndex = 0;

    public void Initialize()
    {
        _characterButton = Characters[_currentCharacter]
            .GetComponent<Button>();
        _characterAnimator = Characters[_currentCharacter]
            .GetComponent<Animator>();

        _characterData = _adventurer.AdventurerData;
        _characterText.text = "";
        _characterButton.onClick.AddListener(ShowSpeechBubbleUI);
        _speechButton.onClick.AddListener(NextDialogue);
        CurrentCharacter = Characters[_currentCharacter];
        CurrentCharacter.transform.position = _characterActivateTransform.position;
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
        _positiveButton.gameObject.SetActive(true);
        _positiveButton.interactable = true;
        
        _negativeButton.gameObject.SetActive(true);
        _negativeButton.interactable = true;
    }

    void HideButtonSpeechBubbleUI()
    {
        _positiveButton.gameObject.SetActive(false);
        _positiveButton.interactable = false;
        _negativeButton.gameObject.SetActive(false);
        _negativeButton.interactable = false;
        
    }

    public void PositiveButton()
    {
        PositiveButtonEvent?.Invoke();
    }

    public void NegativeButton()
    {
        NegativeButtonEvent?.Invoke();
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

    void ShowSpeechBubbleButtonUI()
    {
        
    }
    

    public void ChangeCharacter()
    {
        Debug.Log("ChangeCharacter 호출");
        _currentCharacter++;

        if (_currentCharacter < Characters.Count)
        {
            Characters[_currentCharacter].SetActive(false);
            CurrentCharacter = Characters[_currentCharacter];
            
            StageShowManager.Instance.MiniCharacter.MiniMove();
        }
        else
        {
            Debug.LogError("캐릭터 몇 명인지 인덱스에 에러남");
        }
    }
    
}