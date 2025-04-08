using System;
using UnityEngine;

public class NightEventManager : Singleton<NightEventManager>
{
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
        UIManager.Instance.OneCycleStartAndEnd.EndCycle();
    }

    private void NightEventPick()
    {
        if (!_isIsNightEventDay)
        {
            return;
        }
        // IsNightEventDay�̸� �߻��� �̺�Ʈ ���ؼ� �߻�
        Debug.Log("nightEventPick");
    }
}
