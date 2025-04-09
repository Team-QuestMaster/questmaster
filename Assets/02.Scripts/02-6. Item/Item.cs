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
    public abstract void Use(Adventurer adventurer, Quest quest); //����Ʈ ���� �� ������ ���
    public abstract void Rollback(Adventurer adventurer, Quest quest); // ����Ʈ ����� ������ ȿ�� ����

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
