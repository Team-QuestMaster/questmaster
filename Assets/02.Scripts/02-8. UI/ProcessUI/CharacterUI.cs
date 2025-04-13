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

    
    //[SerializeField] private Adventurer _adventurer;
    [SerializeField] private Animator _characterAnimator;
    [SerializeField] private Button _characterButton;
    [SerializeField] private TextMeshProUGUI _characterText;
    [SerializeField] private Image _speechBubble;
    [SerializeField] private Button _speechButton;

    [SerializeField] private Button _positiveButton;
    [SerializeField] private Button _negativeButton;

    [SerializeField] private Transform _characterActivateTransform;
    

    [SerializeField] private AdventurerIDCardUI _adventurerIDCardUI;
    private bool isCloseable = true;
 
    
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

        _characterText.text = "";
        //_characterButton.onClick.AddListener(ShowSpeechBubbleUI);
        _speechButton.onClick.RemoveAllListeners();
        _speechButton.onClick.AddListener(NextDialogue);
        CurrentCharacter = Characters[_currentCharacter];
        CurrentCharacter.transform.position = _characterActivateTransform.position;

        Adventurer adventurer = CurrentCharacter.GetComponent<Adventurer>();
        adventurer.SetAdventurerData();
        _characterData = adventurer.AdventurerData;
        _adventurerIDCardUI.Initialize(adventurer);

        StageShowManager.Instance.ShowCharacter.CharacterDisappearShow += HideSpeechBubbleUI;
        PositiveButtonEvent += () => isCloseable = true;
        NegativeButtonEvent += () => isCloseable = true;
        
       

        
    }

    public void ShowSpeechBubbleUI()
    {
        _speechBubble.gameObject.SetActive(true);
        _speechButton.interactable = true;
        _speechBubble.DOFade(1f, 0.1f);
        

        ShowDialogueUI(_dialogIndex);
    }

    public void ShowSpeechBubbleUIwithPrefix()
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

    public void HideButtonSpeechBubbleUI()
    {
        _positiveButton.gameObject.SetActive(false);
        _positiveButton.interactable = false;
        _negativeButton.gameObject.SetActive(false);
        _negativeButton.interactable = false;
        
    }

    public void PositiveButton()
    {
        NightEventManager.Instance.TryBuy = true;
        PositiveButtonEvent?.Invoke();
    }

    public void NegativeButton()
    {
        NightEventManager.Instance.TryBuy = false;
        NegativeButtonEvent?.Invoke();
    }
    
    void HideSpeechBubbleUI()
    {
        if (isCloseable)
        {
            _speechBubble.DOFade(0f, 0.1f);
            _speechBubble.gameObject.SetActive(false);
            _speechButton.interactable = false;
        }
    }

    void ShowDialogueUI(int i)
    {
        string prefix = StageShowManager.Instance.ShowCharacter.Prefix;
        if (_dialogIndex < _characterData.DialogSet.Dialog.Count)
        {
            if (_characterData.DialogSet.Dialog[i].StartsWith(prefix))
            {
                _characterText.text = _characterData.DialogSet.Dialog[i].Substring(prefix.Length);
            }
            else
            {

                _characterText.text = _characterData.DialogSet.Dialog[i];
            }
        }
        else
        {
            _characterText.text = "(더 이상 할 말이 없어 보인다.)";
        }
    }

    void NextDialogue()
    {
        if (_dialogIndex >= _characterData.DialogSet.Dialog.Count)
        {
            HideSpeechBubbleUI();
            return;
        }

        ShowDialogueUI(_dialogIndex);
        _dialogIndex++; // 대사 출력 후 증가
    }

    public void ShowSpeechBubbleButtonUI()
    {
        isCloseable = false;
        ShowSpeechBubbleUI();
        ShowButtonSpeechBubbleUI();
    }

    void HideSpeechBubbleButtonUI()
    {
        _positiveButton.gameObject.SetActive(false);
        _negativeButton.gameObject.SetActive(false);
        _negativeButton.interactable = false;
        _positiveButton.interactable = false;
        HideSpeechBubbleUI();
    }

    public void ChangeCharacter()
    {
        _currentCharacter++;
        _dialogIndex = 0;
        HideSpeechBubbleButtonUI();
        CurrentCharacter.SetActive(false);
        if (_currentCharacter < Characters.Count)
        {
            CurrentCharacter = Characters[_currentCharacter];
            StageShowManager.Instance.MiniCharacter.MiniMove();
        }
        else
        {
            _currentCharacter = 0;
            UIManager.Instance.OneCycleStartAndEnd.EndCycle(); 
        }

    }

}