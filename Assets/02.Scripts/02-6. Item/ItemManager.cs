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
    private List<GameObject> _remainItemList = new List<GameObject>(); // 미보유 아이템 리스트
    public List<GameObject> RemainItemList { get => _remainItemList; }
    [SerializeField]
    private List<GameObject> _havingItemList = new List<GameObject>(); // 보유 아이템 리스트
    public List<GameObject> HavingItemList { get => _havingItemList;}

    private List<GameObject> _shoppingList = new List<GameObject>(); // 상점 아이템 리스트
    public List<GameObject> ShoppingList { get => _shoppingList; set => _shoppingList = value; }
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

    public void BuyItem(GameObject item) // 상점에서 제거, 보유 아이템에 추가
    {
        Item itemComponent = item.GetComponent<Item>();
        if (IsInventoryFull())
        {
            Debug.Log("인벤토리가 가득 찼습니다.");
            return;
        }
        if(!GuildStatManager.Instance.TryConsumeGold(itemComponent.Price))
        {
            Debug.Log("골드가 부족합니다.");
            return;
        }
        _havingItemList.Add(item);
        if(itemComponent.ItemState != ItemStateType.ReadyToBuy)
        {
            Debug.Log("아이템 상태가 ReadyToBuy가 아닙니다.에러.");
            return;
        }
        Sequence seq = DOTween.Sequence();

        seq.Append(item.transform.DOScale(transform.localScale * 1.4f, 0.25f).SetEase(Ease.OutQuad));
        seq.Append(item.transform.DOScale(transform.localScale, 0.25f).SetEase(Ease.InQuad));
        itemComponent.ItemState = ItemStateType.Bought; // 아이템 상태 변경
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
            Debug.Log("골드가 부족합니다.");
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

    public void StatItemUse(Adventurer adventurer, Quest quest)
    {
        List<GameObject> removeList = new List<GameObject>(); // 사용한 아이템 리스트
        foreach (GameObject item in _havingItemList)
        {
            if (item.GetComponent<Item>().ItemState == ItemStateType.ReadyToUse && item.GetComponent<Item>().ItemEffectType == ItemEffectType.StatChange)
            {
                item.GetComponent<StatItem>().StatUse(adventurer);
                item.GetComponent<DraggingObjectSwap>().SwapTargetObject.gameObject.GetComponent<Image>().DOFade(0, 1).SetAutoKill(false);
                item.SetActive(false); // 아이템 비활성화
                item.GetComponent<Item>().ItemState = ItemStateType.UnBuy; // 아이템 상태 변경
                removeList.Add(item); // 보유 아이템 리스트에서 제거
                RemainItemList.Add(item); // 미보유 아이템 리스트에 추가
            }
        }
        foreach(GameObject item in removeList)
        {
            _havingItemList.Remove(item); // 보유 아이템 리스트에서 제거
         }
    }

    public float QuestItemUse(Adventurer adventurer, Quest quest)
    {
        float sum = 0;

        List<GameObject> removeList = new List<GameObject>(); // 사용한 아이템 리스트
        foreach (GameObject item in _havingItemList)
        {
            if (item.GetComponent<Item>().ItemState == ItemStateType.ReadyToUse && item.GetComponent<Item>().ItemEffectType == ItemEffectType.QuestChange)
            {
                sum += item.GetComponent<QuestItem>().QuestUse(quest);
                item.GetComponent<DraggingObjectSwap>().SwapTargetObject.gameObject.GetComponent<Image>().DOFade(0, 1).SetAutoKill(false);
                item.SetActive(false); // 아이템 비활성화
                item.GetComponent<Item>().ItemState = ItemStateType.UnBuy; // 아이템 상태 변경
                removeList.Add(item); // 보유 아이템 리스트에서 제거
                RemainItemList.Add(item); // 미보유 아이템 리스트에 추가
            }
        }
        foreach (GameObject item in removeList)
        {
            _havingItemList.Remove(item); // 보유 아이템 리스트에서 제거
        }
        return sum;
    }

    public void SellingItems()
    {
        Debug.Log("SellingItems 호출됨\n" + Environment.StackTrace);
        StartCoroutine(SellingItemsCoroutine());
    }

    private IEnumerator SellingItemsCoroutine()
    {

        yield return new WaitForSeconds(1f); // 1초 대기
        for (int i = 0; i < SHOP_ITEM_COUNT; i++)
        {
            if (_remainItemList.Count == 0) // 미보유 아이템 리스트가 비어있으면 종료
            {
                Debug.Log("미보유 아이템 리스트가 비었습니다.");
                yield break;
            }
            GameObject item = _remainItemList[UnityEngine.Random.Range(0, _remainItemList.Count)];
            GameObject smallItem = item.GetComponent<DraggingObjectSwap>().SwapTargetObject.gameObject;
            _shoppingList.Add(item); // 상점 아이템 리스트에 추가
            _remainItemList.Remove(item); // 미보유 아이템 리스트에서 제거   

            smallItem.GetComponent<RectTransform>().position = new Vector3(-8 + i * 1.5f, -4, 0); // 상점에 아이템 위치 초기화
            smallItem.SetActive(true);
            smallItem.GetComponent<Image>().DOFade(1, 1).SetAutoKill(false);
            yield return new WaitForSeconds(0.5f); // 1초 대기
        }
    }

    public void ReturnItems(Action onComplete = null) // 상점에 남은 아이템 미보유아이템에 추가
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
            smallItem.GetComponent<Image>().DOFade(0, 1).SetAutoKill(false); // 아이템 페이드 아웃
            item.GetComponent<Image>().DOFade(0, 1).SetAutoKill(false); // 아이템 페이드 아웃

            yield return new WaitForSeconds(1f); // 1초 대기
            _remainItemList.Add(item); 
            smallItem.SetActive(false);
            item.SetActive(false); // 아이템 비활성화
            if(item.GetComponent<Item>().ItemState == ItemStateType.ReadyToBuy)
            {
                item.GetComponent<Item>().ItemState = ItemStateType.UnBuy; // 아이템 상태 변경
            }
        }
            _shoppingList.Clear(); // 상점 아이템 리스트 초기화

        onComplete?.Invoke(); // 모든 아이템이 반환된 후 호출
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
