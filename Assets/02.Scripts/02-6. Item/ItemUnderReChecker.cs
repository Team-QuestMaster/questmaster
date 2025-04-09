using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemUnderReChecker : MonoBehaviour
{
    // 마우스 버튼을 UI 위에서 뗐을 때 호출됨
    public void OnPointerUp()
    {
        Debug.Log("ItemUnderReChecker - 마우스 버튼 뗌");

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
                Debug.LogWarning($"{item.name} 에 UnderChecker 컴포넌트가 없음");
            }
        }
    }
}
