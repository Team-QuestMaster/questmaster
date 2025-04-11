using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class DraggableObject : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerDownHandler,  IPointerUpHandler
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

    public RectTransform DragArea => _dragArea;

    public event Action OnPointerDownEvent;
    public event Action<PointerEventData> OnDraggingEvent;
    public event Action OnPointerUpEvent; // Pointer가 Up될때 발동

    private RectTransform _rectTransform;

    private Vector2 _pointerMargin;
    
    private RectTransform _parentRectTransform;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
        _parentRectTransform = transform.parent.GetComponent<RectTransform>();
        _rectTransform = GetComponent<RectTransform>();
        if (TryGetComponent(out Image image))
        {
            image.alphaHitTestMinimumThreshold = 0.1f;  // 이미지라면 투명 영역 클릭 방지
        }
        if (ReferenceEquals(_dragArea, null))
        {
            _dragArea = transform.root.GetComponent<RectTransform>();
        }
    }

    private void OnEnable()
    {
        _pointerMargin = Vector2.zero;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.SetAsLastSibling();
        OnPointerDownEvent?.Invoke();
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
    
    public void OnPointerUp(PointerEventData eventData)
    {
        OnPointerUpEvent?.Invoke();
    }

    private Vector2 ClampToDragArea(Vector2 targetPosition)
    {
        // 객체의 크기
        Vector2 sizeMinMargin = _rectTransform.sizeDelta * _rectTransform.pivot;
        Vector2 sizeMaxMargin = _rectTransform.sizeDelta * (Vector2.one - _rectTransform.pivot);

        // 영역의 크기를 Pivot에 맞추어 계산
        Vector2 minSideSize = _dragArea.rect.size * (Vector2.one - _dragArea.pivot);
        Vector2 maxSideSize = _dragArea.rect.size * _dragArea.pivot;
        // sprite 크기와 영역 계산
        Vector2 screenPosition = _camera.WorldToScreenPoint(_dragArea.position);
        Vector2 minBounds = screenPosition - minSideSize + sizeMinMargin;
        Vector2 maxBounds = screenPosition + maxSideSize - sizeMaxMargin;

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