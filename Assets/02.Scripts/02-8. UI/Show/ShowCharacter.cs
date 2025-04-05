using System;
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
        CharacterAppearShow+= CharacterAppear;
        CharacterDisappearShow += CharacterDisappear;
        
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
        
    }

    void CharacterDisappear()
    {
        
    }
    
}
