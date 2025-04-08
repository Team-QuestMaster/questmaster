using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using System;

public class ItemManager : Singleton <ItemManager>
{
    [SerializeField]
    private List<Item> _remainItemList = new List<Item>(); // �̺��� ������ ����Ʈ
    public List<Item> RemainItemList { get => _remainItemList; }
    [SerializeField]
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

    public void UsingItems(Adventurer adventurer, Quest quest)
    {
        foreach(Item item in _havingItemList)
        {
            if (item.ItemState == ItemStateType.ReadyToUse)
            {
                item.Use(adventurer, quest);
                item.gameObject.SetActive(false); // ������ ��Ȱ��ȭ
            }
        }
    }

    public void RollbackItems(Adventurer adventurer, Quest quest)
    {
        List<Item> toRemove = new List<Item>();
        foreach (Item item in _havingItemList)
        {
            if (item.ItemState == ItemStateType.ReadyToUse)
            {
                item.Rollback(adventurer, quest);
                item.ItemState = ItemStateType.UnBuy; // ������ ���� ����
                toRemove.Add(item);
            }
        }
        foreach (Item item in toRemove)
        {
            _havingItemList.Remove(item);
        }
    }

    public void SellingItems()
    {
        StartCoroutine(SellingItemsCoroutine());
    }

    private IEnumerator SellingItemsCoroutine()
    {
        for (int i = 0; i < SHOP_ITEM_COUNT; i++)
        {
            if (_remainItemList.Count == 0) // �̺��� ������ ����Ʈ�� ��������� ����
            {
                Debug.Log("�̺��� ������ ����Ʈ�� ������ϴ�.");
                yield break;
            }

            Item item = _remainItemList[UnityEngine.Random.Range(0, _remainItemList.Count)];
            _shoppingList.Add(item); // ���� ������ ����Ʈ�� �߰�
            _remainItemList.Remove(item); // �̺��� ������ ����Ʈ���� ����   

            yield return new WaitForSeconds(1f); // 1�� ���
            item.GetComponent<RectTransform>().position = new Vector3(-8 + i * 1.5f, -4, 0); // ������ ������ ��ġ �ʱ�ȭ
            item.gameObject.SetActive(true);
        }
    }

    public void ReturnItems(Action onComplete = null) // ������ ���� ������ �̺��������ۿ� �߰�
    {
        StartCoroutine(ReturnItemsCoroutine(onComplete));
    }


    private IEnumerator ReturnItemsCoroutine(Action onComplete = null)
    {
        foreach (Item item in _shoppingList)
        {
            yield return new WaitForSeconds(1f); // 1�� ���
            _remainItemList.Add(item);
            item.gameObject.SetActive(false);
        }
            _shoppingList.Clear(); // ���� ������ ����Ʈ �ʱ�ȭ

        onComplete?.Invoke(); // ��� �������� ��ȯ�� �� ȣ��
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
