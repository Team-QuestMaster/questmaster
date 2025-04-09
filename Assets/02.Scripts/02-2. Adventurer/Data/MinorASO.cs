using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "MinorASO", menuName = "ScriptableObject/MinorASO")]
public class MinorASO : AdventurerSO
{
    // 아래 존재하는 리스트들은, 모두 마이너 모험가가 
    public List<string> AdventurerNameList = new List<string>();  // 이름
    public List<string> AdventurerClassList = new List<string>();  // 직업
    public List<string> AdventurerTitleList = new List<string>();  // 칭호 리스트
    public List<Sprite> AdventurerSpriteLDList = new List<Sprite>(); // 보조 모험가의 LD 스프라이트
}
