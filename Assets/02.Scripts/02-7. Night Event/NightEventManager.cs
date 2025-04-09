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

    public void MarketEvent() // ���� �Ǹ� ȣ��
    {
        UIManager.Instance.CharacterUI.Characters.Clear();
        UIManager.Instance.CharacterUI.Characters.Add(_dealer);
        StageShowManager.Instance.ShowCharacter.Appear(() =>
        {
            ItemManager.Instance.SellingItems();
        });
    }

    public void AfterMarketEvent() // UI���� ���� Ȯ���� ȣ��
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
        // IsNightEventDay�̸� �߻��� �̺�Ʈ ���ؼ� �߻�
        Debug.Log("nightEventPick");
        MarketEvent();
    }
}
