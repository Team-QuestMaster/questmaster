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

    // TryPayTax() ȣ�� ������ ���� ��ħ �Ϸ�� ����Ʈ�� ��ȸ�� �Ŀ� ȣ���ϴ� ���� ���� �� ����.
    // TryPayTax() ȣ�� ��, ���� ���θ� ������ �˷��ִ� UI ��Ҹ� ������� �Ѵ�.
    // �˾����� ����� ���� ���� ������?
}
