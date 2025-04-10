using System;
using Coffee.UIEffects;
using UnityEngine;
using UnityEngine.EventSystems;

public enum ItemStateType
{
    UnBuy,
    ReadyToBuy,
    Bought,
    ReadyToUse,
    Small
}

public enum ItemEffectType
{
    StatChange,
    QuestChange
}

public abstract class Item : MonoBehaviour // 아이템 추상 클래스
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

    [SerializeField]
    private ItemStateType _itemState;

    public ItemStateType ItemState
    {
        get => _itemState;
        set
        {
            _itemState = value;
            if(_readyToUseEffect != null)
            {
                _readyToUseEffect.enabled = _itemState == ItemStateType.ReadyToUse;
            }
        }
    }

    [SerializeField]
    private ItemEffectType _itemEffectType;
    public ItemEffectType ItemEffectType { get => _itemEffectType; }

    private UIEffect _readyToUseEffect;

    private void Awake()
    {
        _readyToUseEffect = GetComponent<UIEffect>();
        GetComponent<DraggingObjectSwap>().ItemSwapEvent += () =>
        {
            if (ItemState == ItemStateType.ReadyToBuy)
            {
                ItemState = ItemStateType.UnBuy;
            }
        };
    }
}
