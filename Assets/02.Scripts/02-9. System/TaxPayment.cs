using UnityEngine;

public class TaxPayment : MonoBehaviour
{
    private const int _paymentTerm = 7;
    [SerializeField]
    private int _tax = 100;
    public int Tax { get => _tax; set => _tax = value; }

    public bool TryPayTax(int currentDate)
    {
        if (!IsPaymentDate(currentDate))
        {
            return false;
        }

        GuildStatManager.Instance.Gold -= _tax;
        IncreaseTax();
        return true;
    }
    private bool IsPaymentDate(int currentDate)
    {
        return currentDate % _paymentTerm == 0;
    }
    private void IncreaseTax()
    {
        _tax *= (DateManager.Instance.CurrentDate + 7);
    }

    // TryPayTax() 호출 시점은 매일 아침 완료된 퀘스트를 조회한 후에 호출하는 것이 좋을 것 같다.
    // TryPayTax() 호출 후, 세금 납부를 했음을 알려주는 UI 요소를 보여줘야 한다.
    // 팝업으로 만드는 것이 좋지 않을까?
}
