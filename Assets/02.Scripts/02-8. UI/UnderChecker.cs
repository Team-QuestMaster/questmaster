using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Type
{
    Approve,
    Reject,
    Item

}
public class UnderChecker : MonoBehaviour
{
    [SerializeField] private RectTransform _paperZone;               // 검사할 UI 영역
    [SerializeField] private RectTransform _boardZone;               // 검사할 UI 영역
    [SerializeField] private DraggableObject _draggableObject;        // 드래그 가능한 오브젝트 참조
    [SerializeField] private Transform _checkPointTransform;          // 기준 위치 (Empty GameObject 등)
    [SerializeField] private EventSystem _eventSystem;                // EventSystem 참조
public bool Interactable = true;
    //public MainProcess Process;
    [SerializeField] private Type _type;

    public event Action OnChecked;

    private void Awake()
    {
        if (TryGetComponent(out StampAnimation stampAnimation))
        {
            if (_type == Type.Approve)
            {
                stampAnimation.OnStamped += UIManager.Instance.StampUI.Approve;
            }
            else if (_type == Type.Reject)
            {
                stampAnimation.OnStamped += UIManager.Instance.StampUI.Reject;
            }
        }
    }

    private void OnEnable()
    {
        if (_draggableObject != null)
            _draggableObject.OnPointerUpEvent += HandleDragEnd;

        if (_eventSystem == null)
            _eventSystem = EventSystem.current;
    }

    private void OnDisable()
    {
        if (_draggableObject != null)
            _draggableObject.OnPointerUpEvent -= HandleDragEnd;
    }

    public void HandleDragEnd()
    {
        if (Interactable && gameObject.activeInHierarchy)
        {
            if (_type != Type.Item)
            {
                Interactable = false;
            }

            if (ReferenceEquals(_checkPointTransform, null))
            {
                Debug.LogWarning("CheckPointTransform이 할당되지 않았습니다.");
                return;
            }

            Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, _checkPointTransform.position);

            PointerEventData pointerData = new PointerEventData(_eventSystem)
            {
                position = screenPoint
            };

            List<RaycastResult> results = new List<RaycastResult>();
            GraphicRaycaster paperRaycaster = _paperZone.GetComponentInParent<GraphicRaycaster>();
            GraphicRaycaster boardRaycaster = _boardZone.GetComponentInParent<GraphicRaycaster>();

            if (paperRaycaster != null && boardRaycaster != null)
            {

                paperRaycaster.Raycast(pointerData, results);
                boardRaycaster.Raycast(pointerData, results);

                List<string> hitTags = new List<string>();
                bool hasQuestTag = false;
                bool hasBoardTag = false;

                foreach (var result in results)
                {
                    string tag = result.gameObject.tag;
                    hitTags.Add(tag);

                    if (tag == "Quest")
                    {
                        hasQuestTag = true;
                        break;
                    }
                }

                foreach (var result in results)
                {
                    string tag = result.gameObject.tag;
                    hitTags.Add(tag);

                    if (tag == "Board")
                    {
                        hasBoardTag = true;
                        break;
                    }
                }

                Debug.Log("Raycast로 감지된 태그들: " + string.Join(", ", hitTags));

                if (_type == Type.Item)
                {
                    Debug.Log($"{hasBoardTag}, {hasQuestTag}");
                    if (hasQuestTag)
                    {
                        if (this.gameObject.GetComponent<Item>().ItemState == ItemStateType.Bought)
                        {
                            this.gameObject.GetComponent<Item>().ItemState = ItemStateType.ReadyToUse;
                        }
                        else if (this.gameObject.GetComponent<Item>().ItemState == ItemStateType.UnBuy)
                        {
                            this.gameObject.GetComponent<Item>().ItemState = ItemStateType.ReadyToBuy;
                        }
                    }
                    else if (hasBoardTag)
                    {
                        if (this.gameObject.GetComponent<Item>().ItemState == ItemStateType.ReadyToUse)
                        {
                            this.gameObject.GetComponent<Item>().ItemState = ItemStateType.Bought;
                        }
                        else if (this.gameObject.GetComponent<Item>().ItemState == ItemStateType.UnBuy)
                        {
                            this.gameObject.GetComponent<Item>().ItemState = ItemStateType.ReadyToBuy;
                        }
                    }
                    else
                    {
                        if (this.gameObject.GetComponent<Item>().ItemState == ItemStateType.ReadyToBuy)
                        {
                            this.gameObject.GetComponent<Item>().ItemState = ItemStateType.UnBuy;
                        }
                    }

                }
                else if (hasQuestTag)
                {
                    Debug.Log("Quest 태그를 가진 UI 오브젝트를 찾았습니다!");
                    
                    OnChecked?.Invoke();

                    return; // 성공했으니 더 이상 진행 X
                }
                else
                {
                    Debug.Log("Quest 태그를 가진 오브젝트가 없습니다.");
                }
            }
            else
            {
                Debug.LogWarning("GraphicRaycaster가 타겟 존의 부모에 없습니다.");
            }

           
        }
        
        // 실패 시 공통 처리
        UIManager.Instance.StampUI.StampBack();
    }

}
