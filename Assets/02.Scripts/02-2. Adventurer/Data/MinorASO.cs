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
    // 아래 존재하는 리스트들은, 모두 마이너 모험가가 
    public List<string> AdventurerNameList = new List<string>();  // 이름
    public List<string> AdventurerClassList = new List<string>();  // 직업
    public List<string> AdventurerTitleList = new List<string>();  // 칭호 리스트
    public List<AdventurerSpritePair> AdventurerSpritePairList = new List<AdventurerSpritePair>();
}
