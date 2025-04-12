using System;
using UnityEngine;

public class NightEventManager : Singleton<NightEventManager>
{
    [SerializeField]
    private GameObject _dealer;
    public GameObject Dealer { get => _dealer; set => _dealer = value; }
    private bool _isIsNightEventDay = false;
    private bool _tryBuy;
    public bool TryBuy { get => _tryBuy; set => _tryBuy = value; }
    private bool _selling = false;
    public bool Selling { get => _selling; set => _selling = value; }
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
        UIManager.Instance.CharacterUI.PositiveButtonEvent += () => AfterMarketEvent();
        UIManager.Instance.CharacterUI.NegativeButtonEvent += () => AfterMarketEvent();
    }

    public void MarketEvent() // 밤이 되면 호출
    {
        Debug.Log("MarketEvent");
        UIManager.Instance.CharacterUI.Characters.Clear();
        UIManager.Instance.CharacterUI.Characters.Add(_dealer);
        StageShowManager.Instance.ShowCharacter.Appear();        
        AudioManager.Instance.PlayBGM(AudioManager.Instance.DealerClip);
        //ItemManager.Instance.SellingItems();
        
    }

    [ContextMenu("TestMarketEvent")]
    public void AfterMarketEvent() 
    {
        if (_selling == false)
        {
            return;
        }
        if (_tryBuy && !ItemManager.Instance.TryBuyall())
        {
            return;
        }
        _selling = false;
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
