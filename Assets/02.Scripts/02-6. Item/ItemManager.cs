using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using UnityEngine.UI;

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

        seq.Append(item.transform.DOScale(transform.localScale * 1.4f, 0.25f).SetEase(Ease.OutQuad));
        seq.Append(item.transform.DOScale(transform.localScale, 0.25f).SetEase(Ease.InQuad));
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
            foreach (GameObject item in _shoppingList)
            {
                if (item.GetComponent<Item>().ItemState == ItemStateType.ReadyToBuy)
                {
                    Sequence shakeSeq = DOTween.Sequence();

                    shakeSeq.Append(item.transform.DORotate(new Vector3(0, 0, 15f), 0.1f).SetEase(Ease.InOutSine))
                            .Append(item.transform.DORotate(new Vector3(0, 0, -15f), 0.1f).SetEase(Ease.InOutSine))
                            .Append(item.transform.DORotate(Vector3.zero, 0.1f).SetEase(Ease.InOutSine));
                }
            }

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

    public void StatItemUse(Adventurer adventurer, QuestModel quest)
    {
        List<GameObject> removeList = new List<GameObject>(); // ����� ������ ����Ʈ
        foreach (GameObject item in _havingItemList)
        {
            if (item.GetComponent<Item>().ItemState == ItemStateType.ReadyToUse && item.GetComponent<Item>().ItemEffectType == ItemEffectType.StatChange)
            {
                item.GetComponent<StatItem>().StatUse(adventurer);
                item.GetComponent<DraggingObjectSwap>().SwapTargetObject.gameObject.GetComponent<Image>().DOFade(0, 1).SetAutoKill(false);
                item.SetActive(false); // ������ ��Ȱ��ȭ
                item.GetComponent<Item>().ItemState = ItemStateType.UnBuy; // ������ ���� ����
                removeList.Add(item); // ���� ������ ����Ʈ���� ����
                //RemainItemList.Add(item); // �̺��� ������ ����Ʈ�� �߰�
            }
        }
        foreach(GameObject item in removeList)
        {
            _havingItemList.Remove(item); // ���� ������ ����Ʈ���� ����
         }
    }

    public float QuestItemUse(Adventurer adventurer, QuestModel quest)
    {
        float sum = 0;

        List<GameObject> removeList = new List<GameObject>(); // ����� ������ ����Ʈ
        foreach (GameObject item in _havingItemList)
        {
            if (item.GetComponent<Item>().ItemState == ItemStateType.ReadyToUse && item.GetComponent<Item>().ItemEffectType == ItemEffectType.QuestChange)
            {
                sum += item.GetComponent<QuestItem>().QuestUse(quest);
                item.GetComponent<DraggingObjectSwap>().SwapTargetObject.gameObject.GetComponent<Image>().DOFade(0, 1).SetAutoKill(false);
                item.SetActive(false); // ������ ��Ȱ��ȭ
                item.GetComponent<Item>().ItemState = ItemStateType.UnBuy; // ������ ���� ����
                removeList.Add(item); // ���� ������ ����Ʈ���� ����
                //RemainItemList.Add(item); // �̺��� ������ ����Ʈ�� �߰�
            }
        }
        foreach (GameObject item in removeList)
        {
            _havingItemList.Remove(item); // ���� ������ ����Ʈ���� ����
        }
        return sum;
    }

    public void SellingItems()
    {
        Debug.Log("SellingItems ȣ���\n" + Environment.StackTrace);
        StartCoroutine(SellingItemsCoroutine());
    }

    private IEnumerator SellingItemsCoroutine()
    {

        yield return new WaitForSeconds(1f); // 1�� ���
        for (int i = 0; i < SHOP_ITEM_COUNT; i++)
        {
            if (_remainItemList.Count == 0) // �̺��� ������ ����Ʈ�� ��������� ����
            {
                Debug.Log("�̺��� ������ ����Ʈ�� ������ϴ�.");
                yield break;
            }
            GameObject item = _remainItemList[UnityEngine.Random.Range(0, _remainItemList.Count)];
            GameObject smallItem = item.GetComponent<DraggingObjectSwap>().SwapTargetObject.gameObject;
            _shoppingList.Add(item); // ���� ������ ����Ʈ�� �߰�
            _remainItemList.Remove(item); // �̺��� ������ ����Ʈ���� ����   

            smallItem.GetComponent<RectTransform>().position = new Vector3(-8 + i * 1.5f, -4, 0); // ������ ������ ��ġ �ʱ�ȭ
            smallItem.SetActive(true);
            Image smallItemImage = smallItem.GetComponent<Image>();
            smallItemImage.DOFade(1, 0.3f).SetAutoKill(false);
            smallItemImage.rectTransform.DOShakeScale(0.5f,0.3f).SetAutoKill(false);
            
            yield return new WaitForSeconds(0.5f); // 1�� ���
        }
        NightEventManager.Instance.Selling = true;
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

            GameObject smallItem = item.GetComponent<DraggingObjectSwap>().SwapTargetObject.gameObject;
            
            Image smallItemImage = smallItem.GetComponent<Image>();
            smallItemImage.DOFade(0, 0.5f).SetAutoKill(false); // ������ ���̵� �ƿ�
            smallItemImage.rectTransform.DOScale(0, 0.5f).SetAutoKill(false).SetEase(Ease.InBack);    
            
            Image itemImage = item.GetComponent<Image>();
            itemImage.DOFade(0, 0.5f).SetAutoKill(false); // ������ ���̵� �ƿ�
            itemImage.rectTransform.DOScale(0, 0.5f).SetAutoKill(false).SetEase(Ease.InBack);

            yield return new WaitForSeconds(1f); // 1�� ���
            _remainItemList.Add(item); 
            smallItem.SetActive(false);
            item.SetActive(false); // ������ ��Ȱ��ȭ

            smallItemImage.DOFade(1, 0.5f).SetAutoKill(false); // ������ ���̵� �ƿ�
            smallItemImage.rectTransform.DOScale(1, 0.5f).SetAutoKill(false).SetEase(Ease.InBack);

            itemImage.DOFade(1, 0.5f).SetAutoKill(false); // ������ ���̵� �ƿ�
            itemImage.rectTransform.DOScale(1, 0.5f).SetAutoKill(false).SetEase(Ease.InBack);

            if (item.GetComponent<Item>().ItemState == ItemStateType.ReadyToBuy)
            {
                item.GetComponent<Item>().ItemState = ItemStateType.UnBuy; // ������ ���� ����
            }
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
