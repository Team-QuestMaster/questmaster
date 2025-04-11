using System;
using Coffee.UIEffects;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

public abstract class Item : MonoBehaviour // ������ �߻� Ŭ����
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
            if(!ReferenceEquals(_itemEffect, null))
            {
                _itemEffect.enabled = _itemState == ItemStateType.ReadyToUse;
            }
        }
    }

    [SerializeField]
    private ItemEffectType _itemEffectType;
    public ItemEffectType ItemEffectType { get => _itemEffectType; }

    private UIEffect _itemEffect;
    private UIEffectTweener _itemEffectTweener;
    
    private const string ITEM_SHINY = "ItemShiny";
    private const string ITEM_USE = "ItemUse";

    private void Awake()
    {
        _itemEffect = GetComponent<UIEffect>();
        _itemEffectTweener = GetComponent<UIEffectTweener>();
        _itemEffect.LoadPreset(ITEM_SHINY);
        _itemEffectTweener.wrapMode = UIEffectTweener.WrapMode.Loop;
        GetComponent<DraggingObjectSwap>().ItemSwapEvent += () =>
        {
            if (ItemState == ItemStateType.ReadyToBuy)
            {
                ItemState = ItemStateType.UnBuy;
            }
        };
    }

    public void PlayUseEffect()
    {
        _itemEffectTweener.enabled = false;
        _itemEffect.LoadPreset(ITEM_USE);
        _itemEffectTweener.wrapMode = UIEffectTweener.WrapMode.Once;
        _itemEffect.enabled = true;
        _itemEffectTweener.enabled = true;
    }
}
