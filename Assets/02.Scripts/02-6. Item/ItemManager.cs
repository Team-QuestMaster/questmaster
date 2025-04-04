using NUnit.Framework;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class ItemManager : Singleton <ItemManager>
{
    [SerializeField]
    private List<Item> _remainItemList = new List<Item>(); // �̺��� ������ ����Ʈ
    public List<Item> RemainItemList { get => _remainItemList; } 

    private List<Item> _havingItemList = new List<Item>(); // ���� ������ ����Ʈ
    public List<Item> HavingItemList { get => _havingItemList;}

    private List<Item> _shopingList = new List<Item>(); // ���� ������ ����Ʈ
    public List<Item> ShopingList { get => _shopingList; set => _shopingList = value; }
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
        ShopingList.Remove(item); 
        HavingItemList.Add(item);
    }

    public void ItemUsed(Item item) // ���� �����ۿ��� ����
    {
        if(ReferenceEquals(item, null))
        {
            Debug.Log("�������� NULL�Դϴ�.");
            return;
        }
        if(!HavingItemList.Contains(item))
        {
            Debug.Log("�κ��丮�� �ش� �������� �����ϴ�.");
            return;
        }
        HavingItemList.Remove(item);
    }

    public void SellingItems() // �߽��� ��ǰ 3�� ����
    {                          // List �����ϴ� ������ �����ϸ� ���纻�� ����°� �����ؼ� void�� ������
        for (int i = 0; i < SHOP_ITEM_COUNT; i++)
        {
            if(RemainItemList.Count == 0) // �̺��� ������ ����Ʈ�� ��������� ����
            {
                Debug.Log("�̺��� ������ ����Ʈ�� ������ϴ�.");
                return;
            }
            Item item = RemainItemList[Random.Range(0, RemainItemList.Count)];
            ShopingList.Add(item); // ���� ������ ����Ʈ�� �߰�
            RemainItemList.Remove(item); // �̺��� ������ ����Ʈ���� ����
        }
    }

    public void ReturnItems() // ������ ���� ������ �̺��������ۿ� �߰�
    {
        foreach (Item item in ShopingList)
        {
            RemainItemList.Add(item); 
            ShopingList.Remove(item);
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
