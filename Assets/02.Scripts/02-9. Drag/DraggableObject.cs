using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Image를 드래그가 가능하도록 제어
    // 조건 및 기능
    // 조건: Image는 절대 영역, 화면 밖으로 안나간다
    // 영역: 이미지가 존재하는 영역
    // 드래그 이벤트

    // 좌 하단의 위치를 기준으로 이동

    [SerializeField] 
    private RectTransform _dragArea;

    public event Action OnDraggingEvent;

    private RectTransform _rectTransform;

    private Vector2 _pointerMargin;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        if (ReferenceEquals(_dragArea, null))
            _dragArea = transform.root.GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = eventData.position;
        _pointerMargin = (Vector2)_rectTransform.localPosition - mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = eventData.position;
        Vector2 newLocalPosition = mousePosition + _pointerMargin;
        // 좌표 제한
        newLocalPosition = ClampToDragArea(newLocalPosition);
        _rectTransform.localPosition = newLocalPosition;
        OnDraggingEvent?.Invoke();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
    }

    private Vector2 ClampToDragArea(Vector2 targetPosition)
    {
        // 객체의 크기
        Vector2 halfSize = _rectTransform.sizeDelta / 2;

        // 영역의 크기를 Pivot에 맞추어 계산
        Vector2 minSideSize = _dragArea.rect.size * (Vector2.one - _dragArea.pivot);
        Vector2 maxSideSize = _dragArea.rect.size * _dragArea.pivot;
        // sprite 크기와 영역 계산
        Vector2 minBounds = (Vector2)_dragArea.localPosition - minSideSize + halfSize;
        Vector2 maxBounds = (Vector2)_dragArea.localPosition + maxSideSize - halfSize;

        // X, Y 좌표를 화면 내부로 제한
        float clampedX = Mathf.Clamp(targetPosition.x, minBounds.x, maxBounds.x);
        float clampedY = Mathf.Clamp(targetPosition.y, minBounds.y, maxBounds.y);

        return new Vector2(clampedX, clampedY);
    }
}