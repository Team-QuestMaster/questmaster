using System;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Item))]
public class ItemToolTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IEndDragHandler
{
    private Item _item;

    private void Awake()
    {
        _item = GetComponent<Item>();
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
