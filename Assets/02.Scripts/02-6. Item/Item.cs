using UnityEngine;

public interface Item
{
    string Name { get; }
    string Description { get; }
    void Use(); // 아이템 사용
}
