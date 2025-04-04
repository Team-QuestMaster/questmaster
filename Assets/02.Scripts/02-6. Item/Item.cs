using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // ������ �߻� Ŭ����
{
    [SerializeField]
    private string _name;
    public string Name { get => _name; }
    [SerializeField]
    private string _description;
    public string Description { get => _description; }
    [SerializeField]
    private string _prize;
    public string Prize { get => _prize; }
    public abstract void Use(Adventurer adventurer, Quest quest); //����Ʈ ���� �� ������ ���
    public abstract void Rollback(Adventurer adventurer, Quest quest); // ����Ʈ ����� ������ ȿ�� ����

    public void OnPointerEnter(PointerEventData eventData)
    {
        //UIManager.Instance.ShowTooltip(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //UIManager.Instance.HideTooltip();
    }

}
