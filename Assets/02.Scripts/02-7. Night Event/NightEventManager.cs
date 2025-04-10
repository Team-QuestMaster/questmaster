using System;
using UnityEngine;

public class NightEventManager : Singleton<NightEventManager>
{
    [SerializeField]
    private GameObject _dealer;
    public GameObject Dealer { get => _dealer; set => _dealer = value; }
    private bool _isIsNightEventDay = false;
    public bool IsNightEventDay
    {
        get => _isIsNightEventDay;
        set
        {
            _isIsNightEventDay = value;
            UIManager.Instance.OneCycleStartAndEnd.SetNextCycleEvent(_isIsNightEventDay ? NightEventPick : null);
        }
    }
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        UIManager.Instance.CharacterUI.PositiveButtonEvent += AfterMarketEvent;
    }

    public void MarketEvent() // 밤이 되면 호출
    {
        Debug.Log("MarketEvent");
        UIManager.Instance.CharacterUI.Characters.Clear();
        UIManager.Instance.CharacterUI.Characters.Add(_dealer);
        StageShowManager.Instance.ShowCharacter.Appear();        
        ItemManager.Instance.SellingItems();
        
    }

    [ContextMenu("TestMarketEvent")]
    public void AfterMarketEvent() 
    {
        if (!ItemManager.Instance.TryBuyall())
        {
            return;
        }
        ItemManager.Instance.ReturnItems(() =>
        {
            UIManager.Instance.OneCycleStartAndEnd.NextCycleOptionalEvent += () => {
                _dealer.SetActive(false);
                IsNightEventDay = false;
                DateManager.Instance.ChangeDateInNight();
            };
            StageShowManager.Instance.ShowCharacter.Disappear();
            
            //UIManager.Instance.OneCycleStartAndEnd.EndCycle();
        });
    }

    private void NightEventPick()
    {
        if (!_isIsNightEventDay)
        {
            return;
        }
        // IsNightEventDay이면 발생할 이벤트 픽해서 발생
        Debug.Log("nightEventPick");
        MarketEvent();
    }
}
