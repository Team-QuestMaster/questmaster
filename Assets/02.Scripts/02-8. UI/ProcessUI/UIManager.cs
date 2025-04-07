using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public GuideBookUI GuideBookUI;
    public StampUI StampUI;
    public ReportUI ReportUI;
    public CharacterUI CharacterUI;
    public SettingUI SettingUI;
    public CalenderManager CalenderManager;
    public DayChangeUI DayChangeUI;
   public OneDayStartAndEnd oneDayStartAndEnd;
    
    [SerializeField] private Image _cursorBox;
    [SerializeField] private TextMeshProUGUI _cursorText;
    

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GuideBookUI.Initialize();
        StampUI.Initialize();
        ReportUI.Initialize();
        CharacterUI.Initialize();
        SettingUI.Initialize();
        _cursorBox.gameObject.SetActive(false);
    }

    public void ShowCursorBox(string text)
    {
        Debug.Log("Cursor Box Showing");
        if (!ReferenceEquals(_cursorBox, null))
        {
            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _cursorBox.canvas.transform as RectTransform,   // 기준이 되는 캔버스
                Input.mousePosition,                             // 마우스 위치 (스크린 좌표)
                _cursorBox.canvas.worldCamera,                   // 카메라 (Screen Space - Camera일 때)
                out localPos                                     // 변환된 로컬 좌표
            );

            _cursorBox.gameObject.SetActive(true);
            _cursorBox.rectTransform.localPosition = localPos;
            _cursorText.text = text;
        }
        else
        {
            Debug.LogError("커서박스가 할당돼지 않음");
        }
    }

    public void HideCursorBox()
    {
        Debug.Log("Cursor Box Hiding");
        _cursorBox.gameObject.SetActive(false);
    }
}