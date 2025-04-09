using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUnderReChecker : MonoBehaviour, IPointerUpHandler
{
    // ���콺 ��ư�� UI ������ ���� �� ȣ���

    public void OnPointerUp(PointerEventData eventData)
    {
        List<GameObject> itemList = ItemManager.Instance.HavingItemList;

        foreach (GameObject item in itemList)
        {
            if (item == null) continue;

            UnderChecker checker = item.GetComponent<UnderChecker>();
            if (checker != null)
            {
                checker.HandleDragEnd();
            }
            else
            {
                Debug.LogWarning($"{item.name} �� UnderChecker ������Ʈ�� ����");
            }
        }
    }
}
