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
    private void Start()
    {
        CharacterAppearShow += UIManager.Instance.CharacterUI.Initialize;
        CharacterAppearShow += CharacterAppear;
        CharacterDisappearShow += CharacterDisappear;
        Appear();
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
    }

    public void Disappear()
    {
        CharacterDisappearShow?.Invoke();
    }

    void CharacterAppear()
    {
        UIManager.Instance.CharacterUI.CurrentCharacter.gameObject.SetActive(true);

        UIManager.Instance.CharacterUI.CurrentCharacter.GetComponent<Image>().DOFade(1, 1);
    }

    void CharacterDisappear()
    {
        UIManager.Instance.CharacterUI.CurrentCharacter.GetComponent<Image>().rectTransform.DOLocalMove(new Vector3(-1200,0,0), 2f).SetEase(Ease.InBack)
            .OnComplete(UIManager.Instance.CharacterUI.ChangeCharacter);
    }
    
}
