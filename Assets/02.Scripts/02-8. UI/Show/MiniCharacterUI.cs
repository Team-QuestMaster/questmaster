using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MiniCharacterUI : MonoBehaviour
{
    public List<Image> Minis;
    
    [SerializeField] private Image _tarven;
    [SerializeField] private int _moveLength;

 
    


    public event Action MoveToTarvenInEvent;
    public event Action MinisMoveEvent;

   
    public void MiniMove()
    {
        foreach (Image mini in Minis)
        {
            Image capturedMini = mini; // 람다 안전
            capturedMini.rectTransform.DOLocalMoveX(
                capturedMini.rectTransform.localPosition.x - _moveLength, 
                1f
            ).OnComplete(() => CheckTarvenIn(capturedMini));
        }

        MinisMoveEvent?.Invoke();
    }

    void CheckTarvenIn(Image mini)
    {
        if (mini.rectTransform.localPosition.x <= _tarven.rectTransform.localPosition.x)
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

        MoveToTarvenInEvent?.Invoke();
    }
}