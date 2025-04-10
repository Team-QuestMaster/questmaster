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
    public QuestUI QuestUI;
    public AdventurerIDCardUI AdventurerIDCardUI;
    public SettingUI SettingUI;
    public CalenderManager CalenderManager;
    public DayChangeUI DayChangeUI;
   public OneCycleStartAndEnd OneCycleStartAndEnd;
    
    [SerializeField] private Image _cursorBox;
    [SerializeField] private TextMeshProUGUI _cursorText;
    [SerializeField] private AudioClip[] _audioClips;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GuideBookUI.Initialize();
        StampUI.Initialize();
        ReportUI.Initialize();
        SettingUI.Initialize();
        _cursorBox.gameObject.SetActive(false);
    }

    public void PlayInteractableSound()
    {
        AudioManager.Instance.PlaySFX(_audioClips[0]);
    }
    public void ShowCursorBox(string text)
    {
        string information = text;
        SetCursorBox(information);
    }

    public void ItemCursorBox(Item item)
    {
        string information = $"아이템 : {item.Name}\n효과   : {item.Description}\n가격   : {item.Price} 골드";
        SetCursorBox(information);
    }

    public void GuildStatCursorBox()
    {
        string information =
            $"현재 길드의 명성치 : <color=#90FFEB>{GuildStatManager.Instance.Fame}</color>\n현재 길드보유 골드 : <color=yellow>{GuildStatManager.Instance.Gold}</color>";
        SetCursorBox(information);
    }
    public void HideCursorBox()
    {
        _cursorBox.gameObject.SetActive(false);
    }

    private void SetCursorBox(string text)
    {
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
}