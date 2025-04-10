using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinorASO", menuName = "ScriptableObject/MinorASO")]
public class MinorASO : AdventurerSO
{ 
    public List<string> AdventurerNameList = new List<string>();  // �̸�
    public List<string> AdventurerClassList = new List<string>();  // ����
    public List<string> AdventurerTitleList = new List<string>();  // Īȣ
    public Sprite AdventurerSpriteSD; // ��� ���̳� ���谡�� �����ϴ� SD ��������Ʈ
    public List<Sprite> AdventurerSpriteLDList = new List<Sprite>(); // LD ��������Ʈ 
}
