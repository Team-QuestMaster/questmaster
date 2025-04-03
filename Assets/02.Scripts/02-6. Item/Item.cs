using UnityEngine;

public abstract class Item : MonoBehaviour // 아이템 추상 클래스
{
    [SerializeField]
    private string _name;
    public string Name { get => _name; }
    [SerializeField]
    private string _description;
    public string Description { get => _description; }
    public abstract void Use(); //추상함수

}
