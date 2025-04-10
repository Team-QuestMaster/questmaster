using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler
{
    private Item _item;

    private void Awake()
    {
        if (!TryGetComponent(out _item))
        {
            // 스몰인 경우 빅에서 찾아서 item정보를 가져옴
            if (TryGetComponent(out DraggingObjectSwap objectSwap))
            {
                if (!objectSwap.SwapTargetObject.TryGetComponent(out _item))
                {
                    Debug.LogError($"Item 정보를 가져올 수 없습니다.");
                }
            }
            else
            {
                Debug.LogError($"빅 오브젝트 정보를 가져올 수 없습니다. 스왑을 확인해 주세요");
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.Instance.ItemCursorBox(_item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.HideCursorBox();
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        UIManager.Instance.HideCursorBox();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UIManager.Instance.ItemCursorBox(_item);
    }
}
