using UnityEditor.PackageManager.Requests;
using UnityEngine;

public class NightEventManager : Singleton<NightEventManager>
{
    [SerializeField]
    private GameObject _dealer;
    public GameObject Dealer { get => _dealer; set => _dealer = value; }

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
            StageShowManager.Instance.ShowCharacter.Disappear(() =>
            {
                DateManager.Instance.ChangeDateInNight();
            });
        });
    }
}
