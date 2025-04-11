using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(DraggableObject))]
public class DraggingObjectSwap : MonoBehaviour
{
    // 영역에 따른 객체 변환 -> 스몰이미지 <-> 빅이미지
    // 변환할 객체를 저장
    // 영역 밖으로 나가면 그 객체로 드래그 이벤트 전달(발생)
    // 기준은 커서
    // 영역과 이미지

    public enum DraggingObjectType
    {
        Big,
        Small,
    }

    [SerializeField]
    private DraggingObjectType _type = DraggingObjectType.Big;

    public DraggingObjectType Type => _type;

    [SerializeField]
    private DraggingObjectSwap _swapTargetObject;

    public DraggingObjectSwap SwapTargetObject => _swapTargetObject;

    private RectTransform _myArea;

    private DraggableObject _draggableObject;

    private Camera _camera;

    private Image _image;

    public event Action ItemSwapEvent;

    private void Awake()
    {
        _myArea = transform.parent.GetComponent<RectTransform>();
        _draggableObject = GetComponent<DraggableObject>();
        _draggableObject.OnDraggingEvent += SwapDraggingObject;
        _camera = Camera.main;
        _image = GetComponent<Image>();
        _image.alphaHitTestMinimumThreshold = 0.1f;
    }

    private void Start()
    {
        ComparePair();
        // _draggableObject.OnDraggingEvent += SwapDraggingObject;
        // 항상 small 객체 먼저 보임
        if (_type == DraggingObjectType.Big)
        {
            _draggableObject.OnPointerDownEvent += () =>
                _draggableObject.transform.SetSiblingIndex(_draggableObject.transform.GetSiblingIndex() - 1); // 최상위 레이보다 밑
            _draggableObject.OnPointerDownEvent += () => ImageShadowManager.Instance.SetTargetImage(_image);
            _draggableObject.OnPointerUpEvent += ImageShadowManager.Instance.DisableImageShadow;
            gameObject.SetActive(false);
        }
    }

    private void SwapDraggingObject(PointerEventData eventData)
    {
        // 객체가 자신의 영역 안이면 return
        Vector2 mousePosition = _camera.ScreenToWorldPoint(ClampToDragArea(eventData.position));

        if (_myArea.rect.Contains(_myArea.InverseTransformPoint(mousePosition)))
        {
            return;
        }

        // 영역을 벗어나는 경우
        gameObject.SetActive(false); // 비활성화

        _swapTargetObject.transform.SetAsLastSibling();
        // 조건은 빅 => 스몰로 갈때 동작
        if (_type == DraggingObjectType.Big)
        {
            ImageShadowManager.Instance.DisableImageShadow();
            ItemSwapEvent?.Invoke();
        }
        // 조건은 스몰 => 빅로 갈때 동작
        if (_type == DraggingObjectType.Small)
        {
            _swapTargetObject.transform.SetSiblingIndex(_swapTargetObject.transform.GetSiblingIndex() - 1); // 최상위 레이보다 밑
            ImageShadowManager.Instance.SetTargetImage(_swapTargetObject._image);
        }

        // 스왑 오브젝트 활성화
        _swapTargetObject.gameObject.SetActive(true);

        _swapTargetObject.transform.position = mousePosition;

        eventData.pointerPress = _swapTargetObject.gameObject; // PointerDown다운 이벤트 전달 => PointerUp이벤트 호출을 위함
        eventData.pointerDrag = _swapTargetObject.gameObject;

        ExecuteEvents.Execute(_swapTargetObject.gameObject, eventData, ExecuteEvents.dragHandler);
    }

    private void ComparePair()
    {
        if (!ReferenceEquals(_swapTargetObject.SwapTargetObject, this))
        {
            Debug.LogError("Big 오브젝트와 Small오브젝트 간 매치가 안맞습니다.");
        }

        if (_swapTargetObject._type == _type)
        {
            Debug.LogError("바꿀 오브젝트 간 type이 달라야합니다.");
        }
    }

    private Vector2 ClampToDragArea(Vector2 screenPosition)
    {
        RectTransform dragArea = _draggableObject.DragArea;
        Vector2 areaScreenPosition = _camera.WorldToScreenPoint(dragArea.position);
        Vector2 minBounds = areaScreenPosition + dragArea.rect.min;
        Vector2 maxBounds = areaScreenPosition + dragArea.rect.max;
        // 우측과 상단 경계선 포함하기위한 -0.1f
        float clampedX = Mathf.Clamp(screenPosition.x, minBounds.x + 0.1f, maxBounds.x - 0.1f);
        float clampedY = Mathf.Clamp(screenPosition.y, minBounds.y + 0.1f, maxBounds.y - 0.1f);
        return new Vector2(clampedX, clampedY);
    }
}