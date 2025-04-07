using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MiniCharacterUI : MonoBehaviour
{

    public List<Image> Minis;
    [SerializeField] private Image _tarven;
    [SerializeField] private int _moveLength;
    private int _leftMinis;
    public event Action MoveToTarvenInEvent;
    public event Action MinisMoveEvent;
    

    void Start()
    {
       // _leftMinis = Minis.Count;
    }
    
    public void MiniMove()
    {
        Debug.Log(Minis.Count);

        foreach (Image mini in Minis)
        {
            
            
            if (!ReferenceEquals(mini,null)  && mini.gameObject.activeInHierarchy)
            {
                
                float targetX = mini.rectTransform.anchoredPosition.x - _moveLength;
                mini.rectTransform.DOLocalMoveX(targetX, 1f).OnComplete(() => {
                    CheckTarvenIn(mini);
                });

                MinisMoveEvent?.Invoke();
            }
        }

        
    }

    void CheckTarvenIn(Image mini)
    {
        if (mini.rectTransform.position.x <= _tarven.rectTransform.position.x)
        {
            TarvenIn();
            mini.gameObject.SetActive(false);
           // _leftMinis--;
           Minis.RemoveAt(0);
           
            Debug.Log(Minis);
        }
    }
    
    public void TarvenIn()
    {
        Debug.Log("TarvenIn");
        _tarven.rectTransform.DOShakeScale( 0.1f,new Vector3(0, 0.01f, 0)).OnComplete(()=>StageShowManager.Instance.ShowCharacter.Appear());
        MoveToTarvenInEvent?.Invoke();
    }
    
    
}
