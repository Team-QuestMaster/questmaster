using UnityEditor;
using UnityEngine;

public class DataSetter : MonoBehaviour
{
    public MinorASO MinorAso;

    [ContextMenu("SetNames")]
    private void SetNames()
    {
        string[] fantasyNames = {
            // 기존 18개
            "라온", "하벨린", "카렐", "라티안", "마카", "지크란", "로키", "모레나", "세라",
            "두나스", "벨사리안", "루시아노", "세라핀느", "그레이브", "아르젠티스",
            "실바니엘라", "오르페우스", "칼리오스",

            // 새로운 82개
            "엘카드", "타르곤", "아르벨", "리세나", "베르칸",
            "엘피스", "나이렌", "크세나", "몰티안", "드레이크",
            "이셀라", "칼로넬", "제르피아", "루시엔", "카일라",
            "오르딘", "브라문", "티엘라", "살리안", "노아린",
            "에르시아", "펜릴드", "자이론", "루메스", "칼바린",
            "아리엘로", "실루엔", "세라핀", "니르엔", "오스칸",
            "린에타", "자렐란", "무르다스", "알리엔", "벨가르트",
            "키세란", "토르넬", "페일라", "레오린", "나리안",
            "다르엔", "루세피아", "헤이렐", "엘두라", "파르셀",
            "에빈", "크리모르", "모르칸", "라니센", "지오렌",
            "발리안", "에스트라", "칼레나", "티라엘", "울마르",
            "피라닐", "셀로마", "자니아", "코르딘", "아스펜",
            "네리안", "멜키르", "에일리드", "루사엘", "트라빈",
            "덴마르", "미카엔", "프레야", "릴사이", "자베르",
            "오네리아", "세란티", "브렌티스", "칼르엔", "엘론드",
            "바스켈", "레사리", "드렐란", "타이리안", "제스카",
            "실렌드", "칼두엔", "바레나", "페이린", "리델로",
            "소르킨", "루에린", "자피엘", "세일라"
        };
        MinorAso.AdventurerNameList.Clear();
        MinorAso.AdventurerNameList.AddRange(fantasyNames);
        EditorUtility.SetDirty(MinorAso);
    }

    [ContextMenu("SetClasses")]
    private void SetClasses()
    {
        string[] adventurerClasses = {
            // 탱커
            "나이트", "전사", "광전사", "창술사", "마검사",

            // 근접 딜러 (근거리)
            "몽크", "닌자", "용기사", "사무라이",

            // 원거리 딜러 (물리)
            "바드", "기공사", "무도가", "거너",

            // 원거리 딜러 (마법)
            "화염술사", "빙결술사", "번개술사", "대지술사", "풍술사", "인첸터", "소환사", "룬마법사",

            // 힐러
            "성기사", "학자", "점성술사", "현자", "성직자", "드루이드"
        };
        MinorAso.AdventurerClassList.Clear();
        MinorAso.AdventurerClassList.AddRange(adventurerClasses);
        EditorUtility.SetDirty(MinorAso);
    }
}