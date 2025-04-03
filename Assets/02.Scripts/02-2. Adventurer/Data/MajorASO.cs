using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MajorASO", menuName = "ScriptableObject/MajorASO")]
public class MajorASO : AdventurerSO
{
    public string AdventurerName;  // �̸�
    public string AdventurerClass; // ����
    public string AdventurerTitle; //  Īȣ
    public AdventurerTierType AdventurerTier; // ���谡 ���
    public AdventurerStateType AdventurerState; //���谡 ����
    public List<string> Dialog = new List<string>(); // ���

}
