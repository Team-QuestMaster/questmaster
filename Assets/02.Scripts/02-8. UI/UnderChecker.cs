using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnderChecker : MonoBehaviour
{
    [SerializeField] private RectTransform _targetZone;               // 검사할 UI 영역
    [SerializeField] private DraggableObject _draggableObject;        // 드래그 가능한 오브젝트 참조
    [SerializeField] private Transform _checkPointTransform;          // 기준 위치 (Empty GameObject 등)
    [SerializeField] private EventSystem _eventSystem;                // EventSystem 참조

    private void OnEnable()
    {
        if (_draggableObject != null)
            _draggableObject.OnEndDragEvent += HandleDragEnd;

        if (_eventSystem == null)
            _eventSystem = EventSystem.current;
    }

    private void OnDisable()
    {
        if (_draggableObject != null)
            _draggableObject.OnEndDragEvent -= HandleDragEnd;
    }

    private void HandleDragEnd()
    {
        if (_checkPointTransform == null)
        {
            Debug.LogWarning("❗ CheckPointTransform이 할당되지 않았습니다.");
            return;
        }

        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, _checkPointTransform.position);

        // 기준점이 targetZone 내부에 있는지 체크
        if (RectTransformUtility.RectangleContainsScreenPoint(_targetZone, screenPoint, Camera.main))
        {
            Debug.Log("✅ 기준 Transform은 타겟 영역 안에 있음");

            // Raycast 실행
            PointerEventData pointerData = new PointerEventData(_eventSystem)
            {
                position = screenPoint
            };

            List<RaycastResult> results = new List<RaycastResult>();
            GraphicRaycaster raycaster = _targetZone.GetComponentInParent<GraphicRaycaster>();
            if (raycaster != null)
            {
                raycaster.Raycast(pointerData, results);

                if (results.Count > 0)
                {
                    GameObject topObject = results[1].gameObject;
                    Debug.Log($"👑 가장 위에 있는 오브젝트: {topObject.name}, 태그: {topObject.tag}");

                    if (topObject.CompareTag("Quest"))
                    {
                        Debug.Log("🎯 Quest 태그를 가진 UI 오브젝트 위에 있습니다!");
                        // 여기에 원하는 성공 로직 실행
                    }
                    else
                    {
                        Debug.Log("⚠️ 위에 있는 오브젝트가 Quest 태그가 아닙니다.");
                    }
                }
                else
                {
                    Debug.Log("🕳️ Raycast 결과가 없습니다.");
                }
            }
            else
            {
                Debug.LogWarning("❗ GraphicRaycaster가 타겟 존의 부모에 없습니다.");
            }
        }
        else
        {
            Debug.Log("❌ 기준 Transform은 타겟 영역 바깥에 있음");
        }
    }
}
