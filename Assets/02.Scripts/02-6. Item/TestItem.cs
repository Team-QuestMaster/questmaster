using UnityEngine;

public class TestItem : Item
{
    public override void Use() // �籸��
    {
        Debug.Log($"Using item: {Name}");
        ItemManager.Instance.RemoveItem(this);
    }
}

