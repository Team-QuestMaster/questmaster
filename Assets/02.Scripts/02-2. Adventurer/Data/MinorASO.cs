using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MinorASO", menuName = "ScriptableObject/MinorASO")]
public class MinorASO : AdventurerSO
{ 
    public List<string> AdventurerNameList = new List<string>();  // 이름
    public List<string> AdventurerClassList = new List<string>();  // 직업
    public List<string> AdventurerTitleList = new List<string>();  // 칭호
    public Sprite AdventurerSpriteSD; // 모든 마이너 모험가가 공유하는 SD 스프라이트
    public List<Sprite> AdventurerSpriteLDList = new List<Sprite>(); // LD 스프라이트 
}
