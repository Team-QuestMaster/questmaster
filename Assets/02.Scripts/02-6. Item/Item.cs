using UnityEngine;

public abstract class Item : MonoBehaviour // ������ �߻� Ŭ����
{
    [SerializeField]
    private string _name;
    public string Name { get => _name; }
    [SerializeField]
    private string _description;
    public string Description { get => _description; }
    public abstract void Use(Adventurer adventurer, Quest quest); //����Ʈ ���� �� ������ ���
    public abstract void Rollback(Adventurer adventurer, Quest quest); // ����Ʈ ����� ������ ȿ�� ����

}
