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


    [SerializeField] private RectTransform _myArea;
    [SerializeField] private GameObject _swapGameObject;
    
    private DraggableObject _draggableObject;

    private Camera _camera;
    
    private void Awake()
    {
        _draggableObject = GetComponent<DraggableObject>();
        _camera = Camera.main;
    }

    private void Start()
    {
        _draggableObject.OnDraggingEvent += SwapDraggingObject;
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
        _swapGameObject.SetActive(true);
        _swapGameObject.transform.localPosition = transform.localPosition;
        
        eventData.pointerDrag = _swapGameObject;
    }
}