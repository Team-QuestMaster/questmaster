using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MiniCharacterUI : MonoBehaviour
{

    public Image[] Minis;
    [SerializeField] private Image _tarven;
    [SerializeField] private int _moveLength;
    private int _leftMinis;
    public event Action MoveToTarvenInEvent;
    public event Action MinisMoveEvent;
    

    void Start()
    {
        MiniMove();
        _leftMinis = Minis.Length;
    }
    
    void MiniMove()
    {
        Debug.Log(Minis.Length);
        foreach (Image mini in Minis)
        {
            float targetX = mini.rectTransform.anchoredPosition.x - _moveLength;
            mini.rectTransform.DOLocalMoveX(targetX, 1f).OnComplete(() => CheckTarvenIn(mini));

            MinisMoveEvent?.Invoke();
            Debug.Log("MiniMove");
            
        }
        
    }

    void CheckTarvenIn(Image mini)
    {
        if (mini.rectTransform.position.x <= _tarven.rectTransform.position.x)
        {
            TarvenIn();
            mini.gameObject.SetActive(false);
            _leftMinis--;
            Debug.Log(_leftMinis);
        }
    }
    
    public void TarvenIn()
    {
        Debug.Log("TarvenIn");
        _tarven.rectTransform.DOShakeScale( 0.1f,new Vector3(0, 0.01f, 0));
        MoveToTarvenInEvent?.Invoke();
    }
    
    
}
