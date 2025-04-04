using UnityEngine;

public class NightEventManager : Singleton<NightEventManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void MarketEvent() // ���� �Ǹ� ȣ��
    {
        ItemManager.Instance.SellingItems();
        //UIManager.Instance.MarketOpen();
    }

    public void AfterMarketEvent() // UI���� ���� Ȯ���� ȣ��
    {
        ItemManager.Instance.ReturnItems();
        //UIManager.Instance.MarketClose();
        DateManager.Instance.ChangeDateInNight();
    }
}
