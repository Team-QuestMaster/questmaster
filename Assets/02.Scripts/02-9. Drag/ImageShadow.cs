using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
public class ImageShadow : MonoBehaviour
{

    [SerializeField]
    private Color _shadowColor;

    [SerializeField]
    private Vector2 _shadowMargin;

    private Image _targetImage;
    
    private Mask _mask;
    private Image _maskImage;
    private RectTransform _maskRectTransform;

    private Image _shadowImage;

    private void Awake()
    {
        _mask = GetComponent<Mask>();
        _maskImage = GetComponent<Image>();
        _maskRectTransform = GetComponent<RectTransform>();
        _shadowImage = transform.GetChild(0).GetComponent<Image>();
    }

    private void Start()
    {
        transform.SetAsLastSibling();
        Setting();
        DisableShadow();
    }

    private void Update()
    {
        FollowTarget();
    }

    private void Setting()
    {
        if (ReferenceEquals(_targetImage, null))
        {
            return;
        }

        // 기본 셋팅
        _mask.showMaskGraphic = false;
        _maskImage.sprite = _targetImage.sprite;

        // 타겟의 크기 설정을 따라감
        _maskRectTransform.anchorMin = _targetImage.rectTransform.anchorMin;
        _maskRectTransform.anchorMax = _targetImage.rectTransform.anchorMax;
        _maskRectTransform.anchoredPosition = _targetImage.rectTransform.anchoredPosition + _shadowMargin;
        _maskRectTransform.sizeDelta = _targetImage.rectTransform.sizeDelta;
        
        // 컬러 적용
        _shadowImage.color = _shadowColor;
        
        transform.SetSiblingIndex(_targetImage.transform.GetSiblingIndex()-1);
    }

    private void FollowTarget()
    {
        if (ReferenceEquals(_targetImage, null))
        {
            return;
        }
        
        _maskRectTransform.anchoredPosition = _targetImage.rectTransform.anchoredPosition + _shadowMargin;
    }

    public void ActiveWithTarget(Image target)
    {
        gameObject.SetActive(true);
        _targetImage = target;
        Setting();
    }

    public void DisableShadow()
    {
        gameObject.SetActive(false);
    }
}