using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShowCharacter : MonoBehaviour
{

    
    
    public event Action CharacterAppearShow; 
    public event Action CharacterDisappearShow;
    
    
    private Animator _characterAnimator;
    private Image  _characterImage;
    
    private Tweener _characterAppearTweener;
    private Tweener _characterDisappearTweener;
    
    public string Prefix = "!!";
    [SerializeField] private AudioClip _appearSound;
    private void Start()
    {
        CharacterDisappearShow += () => CharacterDisappear();
        // Appear();
    }

    public void AppearEventSet()
    {
        CharacterAppearShow += UIManager.Instance.CharacterUI.Initialize;
        CharacterAppearShow += () => CharacterAppear();
    }
    

    //모험가의 애니메이터를 등록함
    public void SetCharacter(Animator characterAnimator, Image characterImage)
    {
        _characterAnimator = characterAnimator;
        _characterImage = characterImage;
    }

    public void Appear()
    {
        CharacterAppearShow?.Invoke();

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
        Debug.Log("등장");
        UIManager.Instance.CharacterUI.CurrentCharacter.gameObject.SetActive(true);
        AudioManager.Instance.PlaySFX(_appearSound);
        UIManager.Instance.CharacterUI.CurrentCharacter.GetComponent<Image>().DOFade(1, 1).SetAutoKill(false)
            .OnComplete(()=>onComplete?.Invoke());

    }

    void CharacterDisappear(System.Action onComplete = null)
    {
        UIManager.Instance.CharacterUI.CurrentCharacter.GetComponent<Image>().rectTransform.DOLocalMove(new Vector3(-1200, 0, 0), 2f).SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                UIManager.Instance.CharacterUI.ChangeCharacter();
                onComplete?.Invoke();
            });
  
        
    }
    
}
