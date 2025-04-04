using System;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private RectTransform _myArea;

    public RectTransform MyArea => _myArea;

    [SerializeField]
    private DraggingObjectSwap _swapGameObject;

    public DraggingObjectSwap SwapGameObject => _swapGameObject;

    private DraggableObject _draggableObject;

    private Camera _camera;

    private void Awake()
    {
        _draggableObject = GetComponent<DraggableObject>();
        _camera = Camera.main;
    }

    private void Start()
    {
        ComparePair();
        _draggableObject.OnDraggingEvent += SwapDraggingObject;
        // 항상 small 객체 먼저 보임
        if (_type == DraggingObjectType.Big)
        {
            gameObject.SetActive(false);
        }
    }

    private void SwapDraggingObject(PointerEventData eventData)
    {
        // 객체가 자신의 영역 안이면 return
        if (_myArea.rect.Contains(_myArea.InverseTransformPoint(transform.position)))
        {
            return;
        }

        // 영역을 벗어나는 경우
        gameObject.SetActive(false); // 비활성화

        // 스왑 오브젝트 활성화 후 커서 중앙으로
        _swapGameObject.gameObject.SetActive(true);
        _swapGameObject.transform.localPosition = transform.localPosition;

        eventData.pointerDrag = _swapGameObject.gameObject;
    }

    private void ComparePair()
    {
        if (!ReferenceEquals(_swapGameObject.SwapGameObject, this))
        {
            Debug.LogError("Big 오브젝트와 Small오브젝트 간 매치가 안맞습니다.");
        }

        if (_swapGameObject._type == _type)
        {
            Debug.LogError("바꿀 오브젝트 간 type이 달라야합니다.");
        }

        if (_swapGameObject.MyArea == _myArea)
        {
            Debug.LogError("바꿀 오브젝트 간 영역이 달라야합니다.");
        }
    }
}