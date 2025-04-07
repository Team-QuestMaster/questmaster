using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdventurerIDCardUI : MonoBehaviour
{
    // 모험가의 정보를 UI로 보여줌
    [SerializeField]
    private List<Sprite> adventurerTierSprites; // 모험가 등급 이미지

    [SerializeField]
    private GameObject titleObject; // 칭호 나타나는 오브젝트

    [Header("신분증 정보")]
    [SerializeField]
    private Image _adventurerImage; // 모험가 이미지

    [SerializeField]
    private Image _tierImage; // 모험가 등급 이미지

    [SerializeField]
    private TextMeshProUGUI _leftStat; // 왼쪽에 적히는 정보

    [SerializeField]
    private TextMeshProUGUI _rightStat; // 오른쪽에 적히는 정보

    [SerializeField]
    private TextMeshProUGUI _adventurerTitle; // 모험가 칭호

    [SerializeField]
    private TextMeshProUGUI _adventurerName;

    // 모험가 데이터
    public void Initialized(Adventurer adventurer)
    {
        AdventurerData data = adventurer.AdventurerData;
        //_adventurerImage.sprite = 모험가 이미지
        _tierImage.sprite = adventurerTierSprites[(int)data.AdventurerTier];

        string leftStat = $"근력 {data.CurrentSTR}\n민첩 {data.CurrentDEX}";
        _leftStat.text = leftStat;

        string rightStat = $"통찰력 {data.CurrentINS}\n손재주 {data.CurrentMAG}";
        _rightStat.text = rightStat;
        
        _adventurerName.text = data.AdventurerName;

        if (!string.IsNullOrEmpty(data.AdventurerTitle))
        {
            titleObject.SetActive(true);
            _adventurerTitle.text = data.AdventurerTitle;
        }
        else
        {
            titleObject.SetActive(false);
        }
    }
    // TODO: 신분증이 등장하는 모션 등 효과 
}