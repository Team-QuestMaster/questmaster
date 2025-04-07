using NUnit.Framework;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class ItemManager : Singleton <ItemManager>
{
    [SerializeField]
    private List<Item> _remainItemList = new List<Item>(); // �̺��� ������ ����Ʈ
    public List<Item> RemainItemList { get => _remainItemList; } 

    private List<Item> _havingItemList = new List<Item>(); // ���� ������ ����Ʈ
    public List<Item> HavingItemList { get => _havingItemList;}

    private List<Item> _shoppingList = new List<Item>(); // ���� ������ ����Ʈ
    public List<Item> ShoppingList { get => _shoppingList; set => _shoppingList = value; }
    private const int SHOP_ITEM_COUNT = 3; // �߽��� ��ǰ ��

    [SerializeField]
    private int _inventoryMaxCount; // �κ��丮 �ִ� ĭ ��, �Һ�
    public int InventoryMaxCount { get => _inventoryMaxCount;}

    [SerializeField]
    private int _inventoryCount; // ���� �κ��丮 ĭ �� (0 �̻� MAX ����)
    public int InventoryCount { get => _inventoryCount; set => _inventoryCount = Mathf.Clamp(value, 0, _inventoryMaxCount); }

    protected override void Awake()
    {
        base.Awake();
    }

    public void BuyItem(Item item) // �������� ����, ���� �����ۿ� �߰�
    {
        if (IsInventoryFull())
        {
            Debug.Log("�κ��丮�� ���� á���ϴ�.");
            return;
        }
        if(!GuildStatManager.Instance.TryConsumeGold(item.Price))
        {
            Debug.Log("��尡 �����մϴ�.");
            return;
        }
        _shoppingList.Remove(item); 
        _havingItemList.Add(item);
        if(item.ItemState != ItemStateType.ReadyToBuy)
        {
            Debug.Log("������ ���°� ReadyToBuy�� �ƴմϴ�.����.");
            return;
        }
        item.ItemState = ItemStateType.Bought; // ������ ���� ����
    }

    public bool TryBuyall()
    {
        int goldSum = 0;
        foreach (Item item in _shoppingList)
        {
            goldSum += item.Price;
        }
        if (goldSum > GuildStatManager.Instance.Gold)
        {
            Debug.Log("��尡 �����մϴ�.");
            return false;
        }
        else
        {
            foreach (Item item in _shoppingList)
            {
                BuyItem(item);
            }
            return true;
        }
    }

    public void ItemUsed(Item item) // ���� �����ۿ��� ����
    {
        if(ReferenceEquals(item, null))
        {
            Debug.Log("�������� NULL�Դϴ�.");
            return;
        }
        if(!_havingItemList.Contains(item))
        {
            Debug.Log("�κ��丮�� �ش� �������� �����ϴ�.");
            return;
        }
        _havingItemList.Remove(item);
    }

    public void SellingItems() // �߽��� ��ǰ 3�� ����
    {                          // List �����ϴ� ������ �����ϸ� ���纻�� ����°� �����ؼ� void�� ������
        for (int i = 0; i < SHOP_ITEM_COUNT; i++)
        {
            if(_remainItemList.Count == 0) // �̺��� ������ ����Ʈ�� ��������� ����
            {
                Debug.Log("�̺��� ������ ����Ʈ�� ������ϴ�.");
                return;
            }
            Item item = _remainItemList[Random.Range(0, _remainItemList.Count)];
            _shoppingList.Add(item); // ���� ������ ����Ʈ�� �߰�
            _remainItemList.Remove(item); // �̺��� ������ ����Ʈ���� ����   

            item.gameObject.SetActive(true);
            item.GetComponent<RectTransform>().position = new Vector3(-8+i*1.5f, -4, 0); // ������ ������ ��ġ �ʱ�ȭ

        }
    }

    public void ReturnItems() // ������ ���� ������ �̺��������ۿ� �߰�
    {
        foreach (Item item in _shoppingList)
        {
            _remainItemList.Add(item); 
            _shoppingList.Remove(item);
            item.gameObject.SetActive(false);
        }
    }




    public bool IsInventoryFull() // �κ��丮 ���� á���� Ȯ��
    {
        return _inventoryCount <= _havingItemList.Count;
    }

    public bool IsInventoryMax() // �κ��丮 Ȯ�� ���� ���� Ȯ��
    {
        return _inventoryCount == _inventoryMaxCount;
    }

}
