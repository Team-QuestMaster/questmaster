using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton <ItemManager>
{
    [SerializeField]
    private List<Item> _itemList = new List<Item>(); // 전체 아이템 리스트
    public List<Item> ItemList { get => _itemList; } 

    private List<Item> _havingItemList = new List<Item>(); // 보유 아이템 리스트
    public List<Item> HavingItemList { get => _havingItemList;}

    [SerializeField]
    private int _inventoryMaxCount; // 인벤토리 최대 칸 수, 불변
    public int InventoryMaxCount { get => _inventoryMaxCount;}

    [SerializeField]
    private int _inventoryCount; // 현재 인벤토리 칸 수 (0 이상 MAX 이하)
    public int InventoryCount { get => _inventoryCount; set => _inventoryCount = Mathf.Clamp(value, 0, _inventoryMaxCount); }

    protected override void Awake()
    {
        base.Awake();
    }

    public void AddItem(Item item) // 보유 아이템에 추가
    {
        if (IsInventoryFull())
        {
            Debug.Log("인벤토리가 가득 찼습니다.");
            return;
        }
        _havingItemList.Add(item);
    }

    public void RemoveItem(Item item) // 보유 아이템에서 제거
    {
        if(ReferenceEquals(item, null))
        {
            Debug.Log("아이템이 NULL입니다.");
            return;
        }
        if(!_havingItemList.Contains(item))
        {
            Debug.Log("인벤토리에 해당 아이템이 없습니다.");
            return;
        }
        _havingItemList.Remove(item);
    }

    public bool IsInventoryFull() // 인벤토리 가득 찼는지 확인
    {
        return _inventoryCount <= _havingItemList.Count;
    }

    public bool IsInventoryMax() // 인벤토리 확장 가능 여부 확인
    {
        return _inventoryCount == _inventoryMaxCount;
    }

}
