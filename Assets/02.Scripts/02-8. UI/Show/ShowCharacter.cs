using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ShowCharacter : MonoBehaviour
{


    private Sequence _appearSequence;
    private Sequence _disappearSequence;
    public event Action CharacterAppearShow; 
    public event Action CharacterDisappearShow;
    
    public string Prefix = "!!";
    [SerializeField] private AudioClip _appearSound;
    [SerializeField] private AudioClip _walkingSound;
    private void Start()
    {
        CharacterDisappearShow += () => CharacterDisappear();
        
        
    }
    public void AppearEventSet()
    {
        CharacterAppearShow += UIManager.Instance.CharacterUI.Initialize;
        CharacterAppearShow += () => CharacterAppear(CharacterSpeak);
    }
    public void Appear()
    {
        CharacterAppearShow?.Invoke();

        
    }

    void CharacterSpeak()
    {
        Adventurer adventurer = UIManager.Instance.CharacterUI.CurrentCharacter.GetComponent<Adventurer>();
        string original = adventurer.AdventurerData.DialogSet.Dialog[0];
        
        if (original.StartsWith(Prefix))
        {
            if (adventurer.AdventurerData.AdventurerType == AdventurerType.Dealer)
            {
                UIManager.Instance.CharacterUI.ShowSpeechBubbleButtonUI();
            }
            else
            {
                UIManager.Instance.CharacterUI.ShowSpeechBubbleUIwithPrefix();
            }
        }
    }
    public void Appear(System.Action onComplete)
    {
        CharacterAppearShow += () => CharacterAppear(onComplete);
        CharacterAppearShow?.Invoke();
    }
    public void Disappear()
    {
        CharacterDisappearShow?.Invoke();
    }
    public void Disappear(System.Action onComplete)
    {
        CharacterDisappearShow += () => CharacterDisappear(onComplete);
        CharacterDisappearShow?.Invoke();
    }
    void CharacterAppear(System.Action onComplete = null)
    {
        UIManager.Instance.CharacterUI.CurrentCharacter.gameObject.SetActive(true);
        Image currentImage = UIManager.Instance.CharacterUI.CurrentCharacter.GetComponent<Image>();
        currentImage.DOColor(Color.gray, 0f);
        currentImage.DOColor(Color.white, 2.5f).SetDelay(1f);
        currentImage.DOFade(1, 2).SetAutoKill(false)
            .OnComplete(()=>onComplete?.Invoke());
        currentImage.rectTransform.DOPunchPosition(new Vector3(0, -50, 0), 2f,3,0);
        currentImage.rectTransform.DOLocalMoveX(-680f,2f).SetAutoKill(false);

        AudioManager.Instance.PlaySFX(_appearSound);
        AudioManager.Instance.PlaySFX(_walkingSound);
    }
    void CharacterDisappear(System.Action onComplete = null)
    {
        Image currentImage = UIManager.Instance.CharacterUI.CurrentCharacter.GetComponent<Image>();
        currentImage.DOColor(Color.gray, 2.5f).SetDelay(1f);
        currentImage.DOFade(0, 2).SetAutoKill(false)
            .OnComplete(()=>onComplete?.Invoke()).SetDelay(1f);
        UIManager.Instance.CharacterUI.CurrentCharacter.GetComponent<Image>().rectTransform.DOLocalMoveX(-1152f, 2f).SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                UIManager.Instance.CharacterUI.ChangeCharacter();
                onComplete?.Invoke();
            });
        AudioManager.Instance.PlaySFX(_walkingSound);
    }
}
