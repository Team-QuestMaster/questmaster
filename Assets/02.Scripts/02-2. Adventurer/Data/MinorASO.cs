using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "MinorASO", menuName = "ScriptableObject/MinorASO")]
public class MinorASO : AdventurerSO
{
    // �Ʒ� �����ϴ� ����Ʈ����, ��� ���̳� ���谡�� 
    public List<string> AdventurerNameList = new List<string>();  // �̸�
    public List<string> AdventurerClassList = new List<string>();  // ����
    public List<string> AdventurerTitleList = new List<string>();  // Īȣ ����Ʈ
    public List<Sprite> AdventurerSpriteLDList = new List<Sprite>(); // ���� ���谡�� LD ��������Ʈ
}
