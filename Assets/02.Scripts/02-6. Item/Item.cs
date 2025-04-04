using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Item : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler // 아이템 추상 클래스
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
    public abstract void Use(Adventurer adventurer, Quest quest); //퀘스트 수주 시 아이템 사용
    public abstract void Rollback(Adventurer adventurer, Quest quest); // 퀘스트 종료시 아이템 효과 삭제

    public void OnPointerEnter(PointerEventData eventData)
    {
        //UIManager.Instance.ShowTooltip(this);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        //UIManager.Instance.HideTooltip();
    }

}
