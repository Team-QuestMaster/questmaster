using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : Singleton <ItemManager>
{
    [SerializeField]
    private List<Item> _itemList = new List<Item>(); // ��ü ������ ����Ʈ
    public List<Item> ItemList { get => _itemList; } 

    private List<Item> _havingItemList = new List<Item>(); // ���� ������ ����Ʈ
    public List<Item> HavingItemList { get => _havingItemList;}

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

    public void AddItem(Item item) // ���� �����ۿ� �߰�
    {
        if (IsInventoryFull())
        {
            Debug.Log("�κ��丮�� ���� á���ϴ�.");
            return;
        }
        _havingItemList.Add(item);
    }

    public void RemoveItem(Item item) // ���� �����ۿ��� ����
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

    public bool IsInventoryFull() // �κ��丮 ���� á���� Ȯ��
    {
        return _inventoryCount <= _havingItemList.Count;
    }

    public bool IsInventoryMax() // �κ��丮 Ȯ�� ���� ���� Ȯ��
    {
        return _inventoryCount == _inventoryMaxCount;
    }

}
