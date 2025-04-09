using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;
using NUnit.Framework.Constraints;

public class ItemManager : Singleton <ItemManager>
{
    [SerializeField]
    private List<GameObject> _remainItemList = new List<GameObject>(); // �̺��� ������ ����Ʈ
    public List<GameObject> RemainItemList { get => _remainItemList; }
    [SerializeField]
    private List<GameObject> _havingItemList = new List<GameObject>(); // ���� ������ ����Ʈ
    public List<GameObject> HavingItemList { get => _havingItemList;}

    private List<GameObject> _shoppingList = new List<GameObject>(); // ���� ������ ����Ʈ
    public List<GameObject> ShoppingList { get => _shoppingList; set => _shoppingList = value; }
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

    public void BuyItem(GameObject item) // �������� ����, ���� �����ۿ� �߰�
    {
        Item itemComponent = item.GetComponent<Item>();
        if (IsInventoryFull())
        {
            Debug.Log("�κ��丮�� ���� á���ϴ�.");
            return;
        }
        if(!GuildStatManager.Instance.TryConsumeGold(itemComponent.Price))
        {
            Debug.Log("��尡 �����մϴ�.");
            return;
        }
        _havingItemList.Add(item);
        if(itemComponent.ItemState != ItemStateType.ReadyToBuy)
        {
            Debug.Log("������ ���°� ReadyToBuy�� �ƴմϴ�.����.");
            return;
        }
        Sequence seq = DOTween.Sequence();

        seq.Append(item.transform.DOScale(transform.localScale * 1.2f, 0.5f).SetEase(Ease.OutQuad));
        seq.Append(item.transform.DOScale(transform.localScale, 0.5f).SetEase(Ease.InQuad));
        itemComponent.ItemState = ItemStateType.Bought; // ������ ���� ����
    }

    public bool TryBuyall()
    {
        int goldSum = 0;
        foreach (GameObject item in _shoppingList)
        {
            if (item.GetComponent<Item>().ItemState == ItemStateType.ReadyToBuy)
            {
                goldSum += item.GetComponent<Item>().Price;
            }
        }
        if (goldSum > GuildStatManager.Instance.Gold)
        {
            Debug.Log("��尡 �����մϴ�.");
            return false;
        }
        else
        {
            foreach (GameObject item in _shoppingList)
            {
                if (item.GetComponent<Item>().ItemState == ItemStateType.ReadyToBuy)
                {
                    BuyItem(item);
                }
            }
            return true;
        }
    }

    public void UsingItems(Adventurer adventurer, Quest quest)
    {
        foreach(GameObject item in _havingItemList)
        {
            if (item.GetComponent<Item>().ItemState == ItemStateType.ReadyToUse)
            {
                item.GetComponent<Item>().Use(adventurer, quest);
                item.gameObject.SetActive(false); // ������ ��Ȱ��ȭ
            }
        }
    }

    public void RollbackItems(Adventurer adventurer, Quest quest)
    {
        List<GameObject> toRemove = new List<GameObject>();
        foreach (GameObject item in _havingItemList)
        {
            if (item.GetComponent<Item>().ItemState == ItemStateType.ReadyToUse)
            {
                item.GetComponent<Item>().Rollback(adventurer, quest);
                item.GetComponent<Item>().ItemState = ItemStateType.UnBuy; // ������ ���� ����
                toRemove.Add(item);
            }
        }
        foreach (GameObject item in toRemove)
        {
            _havingItemList.Remove(item);
        }
    }

    public void SellingItems()
    {
        Debug.Log("SellingItems ȣ���\n" + Environment.StackTrace);
        StartCoroutine(SellingItemsCoroutine());
    }

    private IEnumerator SellingItemsCoroutine()
    {

        yield return new WaitForSeconds(1.5f); // 1�� ���
        for (int i = 0; i < SHOP_ITEM_COUNT; i++)
        {
            if (_remainItemList.Count == 0) // �̺��� ������ ����Ʈ�� ��������� ����
            {
                Debug.Log("�̺��� ������ ����Ʈ�� ������ϴ�.");
                yield break;
            }
            GameObject item = _remainItemList[UnityEngine.Random.Range(0, _remainItemList.Count)];
            _shoppingList.Add(item); // ���� ������ ����Ʈ�� �߰�
            _remainItemList.Remove(item); // �̺��� ������ ����Ʈ���� ����   

            item.GetComponent<RectTransform>().position = new Vector3(-8 + i * 1.5f, -4, 0); // ������ ������ ��ġ �ʱ�ȭ
            item.SetActive(true);
            item.GetComponent<Image>().DOFade(1, 1).SetAutoKill(false);
            yield return new WaitForSeconds(0.5f); // 1�� ���
        }
    }

    public void ReturnItems(Action onComplete = null) // ������ ���� ������ �̺��������ۿ� �߰�
    {
        StartCoroutine(ReturnItemsCoroutine(onComplete));
    }


    private IEnumerator ReturnItemsCoroutine(Action onComplete = null)
    {
        foreach (GameObject item in _shoppingList)
        {

            if (item.GetComponent<Item>().ItemState == ItemStateType.Bought)
                continue;

            item.GetComponent<Image>().DOFade(0, 1).SetAutoKill(false); // ������ ���̵� �ƿ�

            yield return new WaitForSeconds(1f); // 1�� ���
            _remainItemList.Add(item); 
            item.SetActive(false);
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
