using UnityEngine;

public class TestItem : Item
{
    public override void Use() // Àç±¸Çö
    {
        Debug.Log($"Using item: {Name}");
        ItemManager.Instance.RemoveItem(this);
    }
}

