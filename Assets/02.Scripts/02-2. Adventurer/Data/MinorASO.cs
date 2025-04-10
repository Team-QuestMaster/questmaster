using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class AdventurerSpritePair
{
    public Sprite SDSprite;
    public Sprite LDSprite;
}

[CreateAssetMenu(fileName = "MinorASO", menuName = "ScriptableObject/MinorASO")]
public class MinorASO : AdventurerSO
{
    // �Ʒ� �����ϴ� ����Ʈ����, ��� ���̳� ���谡�� 
    public List<string> AdventurerNameList = new List<string>();  // �̸�
    public List<string> AdventurerClassList = new List<string>();  // ����
    public List<string> AdventurerTitleList = new List<string>();  // Īȣ ����Ʈ
    public List<AdventurerSpritePair> AdventurerSpritePairList = new List<AdventurerSpritePair>();
}
