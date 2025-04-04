using UnityEngine;

public class NightEventManager : Singleton<NightEventManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void MarketEvent() // 밤이 되면 호출
    {
        ItemManager.Instance.SellingItems();
        //UIManager.Instance.MarketOpen();
    }

    public void AfterMarketEvent() // UI에서 구매 확정시 호출
    {
        ItemManager.Instance.ReturnItems();
        //UIManager.Instance.MarketClose();
        DateManager.Instance.ChangeDateInNight();
    }
}
