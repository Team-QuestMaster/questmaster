using UnityEngine;

public interface Item
{
    string Name { get; }
    string Description { get; }
    void Use(); // ������ ���
}
