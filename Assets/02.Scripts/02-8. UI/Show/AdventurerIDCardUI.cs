using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class BigAdventurerIDCardContent
{
    public List<Sprite> AdventurerTierSprites;
    public GameObject TitleObject;
    public Image AdventurerImage;
    public Image AdventurerTierImage;
    public TextMeshProUGUI AdventurerNameTMP;
    public TextMeshProUGUI AdventurerTitleTMP;
    public TextMeshProUGUI LeftStatTMP;
    public TextMeshProUGUI RightStatTMP;
}


public class AdventurerIDCardUI : MonoBehaviour
{
    // 모험가의 정보를 UI로 보여줌
    [SerializeField]
    private List<Sprite> adventurerTierSprites; // 모험가 등급 이미지

    [Header("신분증 정보")]
    [SerializeField]
    private BigAdventurerIDCardContent _bigAdventurerIDCardContent;

    [Header("작은 신분증")]
    [SerializeField]
    private GameObject _smallIDCardGO;
    public GameObject SmallIDCardGO { get => _smallIDCardGO; set => _smallIDCardGO = value; }
    [SerializeField]
    private Transform _smallIDCardActivateTransform;

    [Header("큰 신분증")]
    [SerializeField]
    private GameObject _bigIDCardGO;
    public GameObject BigIDCardGO { get => _bigIDCardGO; set => _bigIDCardGO = value; }
    private Transform _bigIDCardActivateTransform;
    private void Awake()
    {
        _bigIDCardActivateTransform = _bigIDCardGO.transform;
    }
    // 모험가 데이터
    public void Initialize(Adventurer adventurer)
    {
        _smallIDCardGO.transform.position = _smallIDCardActivateTransform.position;
        _bigIDCardGO.transform.position = _bigIDCardActivateTransform.position;
        InitializeBigAdventurerIDCardContent(adventurer.AdventurerData);
    }

    private void InitializeBigAdventurerIDCardContent(AdventurerData data)
    {
        // 모험가 칭호 TMP에 반영
        if (!string.IsNullOrEmpty(data.AdventurerTitle))
        {
            _bigAdventurerIDCardContent.TitleObject.SetActive(true);
            _bigAdventurerIDCardContent.AdventurerTitleTMP.text = data.AdventurerTitle;
        }
        else
        {
            _bigAdventurerIDCardContent.AdventurerTitleTMP.text = "칭호없음";
        }
        // 모험가 이름 TMP에 반영
        if (!string.IsNullOrEmpty(data.AdventurerName))
        {
            _bigAdventurerIDCardContent.AdventurerNameTMP.text = data.AdventurerName;
        }
        else
        {
            _bigAdventurerIDCardContent.AdventurerNameTMP.text = "무명의 모험가";
        }

        // 모험가 사진 스프라이트 반영
        _bigAdventurerIDCardContent.AdventurerImage.sprite = data.SpriteLD;


        // 모험가 티어 스프라이트 반영
        _bigAdventurerIDCardContent.AdventurerTierImage.sprite = adventurerTierSprites[(int)data.AdventurerTier];
        // 모험가 스탯 TMP에 반영
        string leftStatSTRAndMAG = $"근력 {data.CurrentSTR}\n \n마력 {data.CurrentMAG}";
        _bigAdventurerIDCardContent.LeftStatTMP.text = leftStatSTRAndMAG;

        string rightStatINSAndDEX = $"통찰력 {data.CurrentINS}\n \n민첩성 {data.CurrentDEX}";
        _bigAdventurerIDCardContent.RightStatTMP.text = rightStatINSAndDEX;
    }
}