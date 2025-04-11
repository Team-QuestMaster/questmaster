using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class StampAnimation : MonoBehaviour
{
    [SerializeField]
    private AudioClip _stampedSound;
    
    private Sequence _sequence;

    public event Action OnStamped;

    private void Awake()
    {
        if (TryGetComponent(out UnderChecker underChecker))
        {
            underChecker.OnChecked += PlayAnimation;
        }
        else
        {
            Debug.LogError("언더 체커가 없습니다.");
        }


    }

    [ContextMenu("Play")]
    public void PlayAnimation()
    {
        _sequence = DOTween.Sequence()
            .Append(transform.DOPunchPosition(new Vector3(0f, 200f, 0f), 0.4f, 1, 0f).SetEase(Ease.InBack))
            .InsertCallback(0.2f, () => AudioManager.Instance.PlaySFX(_stampedSound))
            .Insert(0.3f, transform.DOPunchScale(new Vector3(0.3f, -0.3f, 0f), 0.4f, 1, 0f).SetEase(Ease.OutQuad))
            .OnComplete(() => OnStamped?.Invoke());
    }
}