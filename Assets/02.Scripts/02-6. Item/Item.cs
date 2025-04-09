using System;
using Coffee.UIEffects;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ItemStateType
{
    UnBuy,
    ReadyToBuy,
    Bought,
    ReadyToUse
}

public abstract class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // 아이템 추상 클래스
{
    

    [SerializeField]
    private string _name;
    public string Name { get => _name; }
    [SerializeField]
    private string _description;
    public string Description { get => _description; }
    [SerializeField]
    private int _price;
    public int Price { get => _price; }
    public abstract void Use(Adventurer adventurer, Quest quest); //퀘스트 수주 시 아이템 사용
    public abstract void Rollback(Adventurer adventurer, Quest quest); // 퀘스트 종료시 아이템 효과 삭제

    [SerializeField]
    private ItemStateType _itemState;

    public ItemStateType ItemState
    {
        get => _itemState;
        set
        {
            _itemState = value;
            _readyToUseEffect.enabled = _itemState == ItemStateType.ReadyToUse;
        }
    }

    private UIEffect _readyToUseEffect;

    private void Awake()
    {
        _readyToUseEffect = GetComponent<UIEffect>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //UIManager.Instance.ShowTooltip(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //UIManager.Instance.HideTooltip();
    }
}
