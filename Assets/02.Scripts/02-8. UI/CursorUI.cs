using System;
using UnityEngine;
using UnityEngine.UI;

public class CursorUI : MonoBehaviour
{
    private RectTransform _rectTransform;
    private RectTransform _screenArea;

    private Camera _camera;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _camera = Camera.main;
        _screenArea = transform.parent.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
        // 영역 검사
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);       // 스크린 좌표로 계산
        Vector2 clampedPosition = ClampToScreenArea(screenPosition);
        Vector2 clampedWorldPosition = _camera.ScreenToWorldPoint(clampedPosition);     // 계산 후 다시 월드 좌표로
        _rectTransform.position = clampedWorldPosition;
    }
    
    private Vector2 ClampToScreenArea(Vector2 targetPosition)
    {
        // 객체의 크기
        Vector2 halfSize = _rectTransform.rect.size / 2;
        Debug.Log(halfSize);

        // 영역의 크기를 Pivot에 맞추어 계산
        Vector2 minSideSize = _screenArea.rect.size * (Vector2.one - _screenArea.pivot);
        Vector2 maxSideSize = _screenArea.rect.size * _screenArea.pivot;
        // sprite 크기와 영역 계산
        Vector2 screenPosition = _camera.WorldToScreenPoint(_screenArea.position);
        Vector2 minBounds = screenPosition - minSideSize + halfSize;
        Vector2 maxBounds = screenPosition + maxSideSize - halfSize;
        
        // X, Y 좌표를 화면 내부로 제한
        float clampedX = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

        return new Vector2(clampedX, clampedY);
    }
}
