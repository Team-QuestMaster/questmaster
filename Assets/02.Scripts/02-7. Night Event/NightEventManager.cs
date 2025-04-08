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

    public void MarketEvent() // 밤이 되면 호출
    {
        UIManager.Instance.CharacterUI.Characters.Clear();
        UIManager.Instance.CharacterUI.Characters.Add(_dealer);
        StageShowManager.Instance.ShowCharacter.Appear(() =>
        {
            ItemManager.Instance.SellingItems();
        });
    }

    public void AfterMarketEvent() // UI에서 구매 확정시 호출
    {
        ItemManager.Instance.ReturnItems(() =>
        {
            StageShowManager.Instance.ShowCharacter.Disappear();
            DateManager.Instance.ChangeDateInNight();
            UIManager.Instance.OneCycleStartAndEnd.EndCycle();
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
