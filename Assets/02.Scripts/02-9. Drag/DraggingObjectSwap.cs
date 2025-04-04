using System;
using UnityEngine;

[RequireComponent(typeof(DraggableObject))]
public class DraggingObjectSwap : MonoBehaviour
{
    // 영역에 따른 객체 변환 -> 스몰이미지 <-> 빅이미지
    // 변환할 객체를 저장
    // 기준은 커서
    // 영역과 이미지

    private DraggableObject _draggableObject;

    [SerializeField] private RectTransform _smallImageArea;
    [SerializeField] private GameObject _smallGameObject;
    [SerializeField] private RectTransform _bigImageArea;
    [SerializeField] private GameObject _bigGameObject;

    private Camera _camera;

    private void Awake()
    {
        _draggableObject = GetComponent<DraggableObject>();
        _camera = Camera.main;
    }

    private void Start()
    {
        _draggableObject.OnDraggingEvent += AreaChecking;
    }

    private void AreaChecking()
    {
        // 커서 위치를 검사해서 이미지 변경
        Vector2 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        if (_smallImageArea.rect.Contains(_smallImageArea.InverseTransformPoint(mouseWorldPosition)))
        {
            Debug.Log("DraggingImageSwap To Small");
        }
        else if (_bigImageArea.rect.Contains(_bigImageArea.InverseTransformPoint(mouseWorldPosition)))
        {
            Debug.Log("DraggingImageSwap To Big");
        }
    }
}