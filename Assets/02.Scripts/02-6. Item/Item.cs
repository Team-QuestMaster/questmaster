using UnityEngine;

public abstract class Item : MonoBehaviour // 아이템 추상 클래스
{
    [SerializeField]
    private string _name;
    public string Name { get => _name; }
    [SerializeField]
    private string _description;
    public string Description { get => _description; }
    public abstract void Use(Adventurer adventurer, Quest quest); //퀘스트 수주 시 아이템 사용
    public abstract void Rollback(Adventurer adventurer, Quest quest); // 퀘스트 종료시 아이템 효과 삭제

}
