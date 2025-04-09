using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MajorASO", menuName = "ScriptableObject/MajorASO")]
public class MajorASO : AdventurerSO
{
    public string AdventurerName;  // 이름
    public string AdventurerClass; // 직업
    public string AdventurerTitle; //  칭호
    public AdventurerTierType AdventurerTier; // 모험가 등급
    public AdventurerStateType AdventurerState; //모험가 상태
    public Sprite SpriteLD; // 메이저 모험가의 LD 스프라이트
}
