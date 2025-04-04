using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton <ItemManager>
{
    [SerializeField]
    private List<Item> _remainItemList = new List<Item>(); // 미보유 아이템 리스트
    public List<Item> RemainItemList { get => _remainItemList; } 

    private List<Item> _havingItemList = new List<Item>(); // 보유 아이템 리스트
    public List<Item> HavingItemList { get => _havingItemList;}

    private List<Item> _shopingList = new List<Item>(); // 상점 아이템 리스트
    public List<Item> ShopingList { get => _shopingList; set => _shopingList = value; }
    private const int SHOP_ITEM_COUNT = 3; // 야시장 물품 수

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

    public void BuyItem(Item item) // 상점에서 제거, 보유 아이템에 추가
    {
        if (IsInventoryFull())
        {
            Debug.Log("인벤토리가 가득 찼습니다.");
            return;
        }
        if(!GuildStatManager.Instance.TryConsumeGold(item.Price))
        {
            Debug.Log("골드가 부족합니다.");
            return;
        }
        _shopingList.Remove(item); 
        _havingItemList.Add(item);
    }

    public void ItemUsed(Item item) // 보유 아이템에서 제거
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

    public void SellingItems() // 야시장 물품 3개 추출
    {                          // List 리턴하는 식으로 구현하면 복사본이 생기는게 찝찝해서 void로 구현함
        for (int i = 0; i < SHOP_ITEM_COUNT; i++)
        {
            if(_remainItemList.Count == 0) // 미보유 아이템 리스트가 비어있으면 종료
            {
                Debug.Log("미보유 아이템 리스트가 비었습니다.");
                return;
            }
            Item item = _remainItemList[Random.Range(0, _remainItemList.Count)];
            _shopingList.Add(item); // 상점 아이템 리스트에 추가
            _remainItemList.Remove(item); // 미보유 아이템 리스트에서 제거
        }
    }

    public void ReturnItems() // 상점에 남은 아이템 미보유아이템에 추가
    {
        foreach (Item item in _shopingList)
        {
            _remainItemList.Add(item); 
            _shopingList.Remove(item);
        }
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
