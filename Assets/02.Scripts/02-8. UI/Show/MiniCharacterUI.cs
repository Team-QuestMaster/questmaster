using System;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MiniCharacterUI : MonoBehaviour
{
    public List<Image> Minis;
    
    [SerializeField] private Image _tarven;
    [SerializeField] private int _moveLength;

    public Image MiniCharacter;
    [SerializeField] private Transform _miniCharacterPivot;


    
    public event Action MoveToTarvenInEvent;
    public event Action MinisMoveEvent;

    

    public void MakeMiniCharacters()
    {
        Debug.Log("미니 캐릭터 생성 시도");
        for (int i = 1; i < UIManager.Instance.CharacterUI.Characters.Count; i++)
        {
            Image mini =  Instantiate(MiniCharacter, _miniCharacterPivot);
            mini.rectTransform.localPosition = new Vector3(_tarven.rectTransform.localPosition.x + _moveLength * (i),
                _tarven.rectTransform.localPosition.y, 0);
            Minis.Add(mini);
            Debug.Log("미니 캐릭터 생섬함");
        }
    }
    public void MiniMove()
    {
        for(int i = 0; i < Minis.Count; i++){
            Image capturedMini = Minis[i]; // 람다 안전
            capturedMini.GetComponent<Animator>().SetBool("Move",true);
            capturedMini.rectTransform.DOLocalMoveX(
                capturedMini.rectTransform.localPosition.x - _moveLength, 
                1f
            ).OnComplete(() =>
            {
  
                capturedMini.GetComponent<Animator>().SetBool("Move", false);
                CheckTarvenIn(capturedMini);
            
        });
        }

        MinisMoveEvent?.Invoke();
    }

    void CheckTarvenIn(Image mini)
    {
        if (mini.rectTransform.localPosition.x <= _tarven.rectTransform.localPosition.x && mini.enabled)
        {
            TarvenIn();
            mini.gameObject.SetActive(false);
        }
    }


     void TarvenIn()
    {
        Debug.Log("TarvenIn");

        _tarven.rectTransform.DOShakeScale(0.1f, new Vector3(0, 0.01f, 0))
            .OnComplete(() =>
            {
                StageShowManager.Instance.ShowCharacter.Appear();
                
            });


    }
}