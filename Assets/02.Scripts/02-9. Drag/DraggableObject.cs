using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(RectTransform))]
public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // Image를 드래그가 가능하도록 제어
    // 조건 및 기능
    // 조건: Image는 절대 영역, 화면 밖으로 안나간다
    // 영역: 이미지가 존재하는 영역
    // 드래그 이벤트
    // 모든 계산은 Screen좌표 기준 => 마지막에만 로컬 좌표로 변환
    // 좌 하단의 위치를 기준으로 이동

    [SerializeField] 
    private RectTransform _dragArea;

    public event Action<PointerEventData> OnDraggingEvent;
    public event Action OnEndDragEvent;

    private RectTransform _rectTransform;

    private Vector2 _pointerMargin;
    
    private RectTransform _parentRectTransform;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _parentRectTransform = transform.parent.GetComponent<RectTransform>();
        _rectTransform = GetComponent<RectTransform>();
        if (ReferenceEquals(_dragArea, null))
            _dragArea = transform.root.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        _pointerMargin = Vector2.zero;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = eventData.position;
        Vector2 screenPosition = _camera.WorldToScreenPoint(transform.position);
        _pointerMargin = screenPosition - mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 mousePosition = eventData.position;
        Vector2 newPosition = mousePosition + _pointerMargin;
        // 좌표 제한
        newPosition = ClampToDragArea(newPosition);
        // 최종 계산만 로컬 좌표로 계산
        _rectTransform.localPosition = ScreenToDragCoordinate(newPosition);
        OnDraggingEvent?.Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
        OnEndDragEvent?.Invoke();
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

    private Vector2 ScreenToDragCoordinate(Vector2 screenPosition)
    {
        Vector2 worldPoint = _camera.ScreenToWorldPoint(screenPosition);
        return _parentRectTransform.InverseTransformPoint(worldPoint);
    }
}