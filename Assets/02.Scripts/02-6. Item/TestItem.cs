using UnityEngine;

public class TestItem : MonoBehaviour, Item
{
    [SerializeField]
    private string _name;
    public string Name { get => _name; }
    [SerializeField]
    private string _description;
    public string Description { get => _description;}

    public void Use()
    {
        Debug.Log($"Using item: {Name}");
        ItemManager.Instance.RemoveItem(this);
    }
}

