using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MinorASO", menuName = "ScriptableObject/MinorASO")]
public class MinorASO : AdventurerSO
{
    public List<string> AdventurerNameList = new List<string>();  // 이름
    public List<string> AdventurerClassList = new List<string>();  // 직업
    public List<string> AdventurerTitleList = new List<string>();  // 칭호

}
