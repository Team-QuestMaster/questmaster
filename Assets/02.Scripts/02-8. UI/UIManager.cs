using System.Collections;
using System.Net.Mime;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private Adventurer _adventurer;
    
    [Header("가이드북")]
    [SerializeField] private Button _guideBook;
    [SerializeField] private Button[] _indexButton;
    [SerializeField] private Animator _guideBookAnimator;
    [SerializeField] private CanvasGroup[] _guideBookCanvasGroup;
    [SerializeField] private Image _fadeOutImage;
    private bool _isGuideBookOpen = false;
    
    [Header("도장")] 
    [SerializeField] private Button _stampZone;
    [SerializeField] private Image _approveStamp;
    [SerializeField] private Image _rejectStamp;
    [SerializeField] private Button _closeStampBtn;
    private bool _isStampShow = false;
    private Vector3 _approvePsition;
    private Vector3 _rejectPsition;
    
    [Header("결과창")]
    [SerializeField] private Image _report;
    [SerializeField] private TextMeshProUGUI _reportText;

    [Header("캐릭터")] 
    [SerializeField] private Animator _characterAnimator;
    [SerializeField] private Button _characterButton;
    [SerializeField] private TextMeshProUGUI _characterText;
    [SerializeField] private Image _speechBubble;
    [SerializeField] private Button _speechButton;
    [SerializeField]public AdventurerData CharacterData;
    
    [Header("설정창")]
    [SerializeField] private Image _settingBackground;
    [SerializeField] private Button _closeSettingButton;
    [SerializeField] private Button _settingButton;
    [SerializeField]private int _dialIndex = 0;
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        
        _guideBook.onClick.AddListener(ToggleGuideBook);
        
        _stampZone.onClick.AddListener(ShowStampPopUp);
        _closeStampBtn.onClick.AddListener(HideStampPopUp);

        _settingButton.onClick.AddListener(ShowSetting);
        _closeSettingButton.onClick.AddListener(HideSetting);
        
        _characterButton.onClick.AddListener(ShowSpeechBubbleUI);
        _speechButton.onClick.AddListener(NextDialogue);
        
        _characterText.text = "";
        foreach (Button button in _indexButton)
        {
            button.interactable = false;
        }

        GetAdventurerData();
        _approvePsition = _approveStamp.transform.localPosition;
        _rejectPsition = _rejectStamp.transform.localPosition;
        print(_approvePsition);
        print(_rejectPsition);
    }

    
    /// <summary>
    /// 설정값을 보여줄 매소드들 입니다.
    /// </summary>
    
    void ShowSetting()
    {
        Debug.Log("Show setting");
        BackFadeOn(0.5f);
        _settingBackground.transform.DOLocalMove(new Vector3(0, 0, 0), 0.5f);
    }

    void HideSetting()
    {
        Debug.Log("Hide setting");
        BackFadeOff(0.5f);
        _settingBackground.transform.DOLocalMove(new Vector3(0, 1003, 0), 0.5f);
    }

    
    
    
    /// <summary>
    /// 가이드 북에 대한 메소드들입니다.
    /// </summary>
    //이건 테스트를 위해서 하나의 버튼이 모든 걸 하고 있지만, 차후 두 개를 분리할 예정입니다.
    void ToggleGuideBook()
    {
        if (_isGuideBookOpen)
        {
            StartCoroutine(GuideBookHide());
        }
        else
        {
            GuideBookShow();
        }
    }

    
    
    void GuideBookShow()
    {
        Debug.Log("Show guidebook");

        
        _guideBook.transform.DORotate(new Vector3(0f, 0f, 0f), 1f);
        _guideBook.transform.DOLocalMove(new Vector3(0, -46, 0), 1f)
            .OnComplete(() =>
            {
                
                BackFadeOn(1f);
                _guideBookAnimator.SetTrigger("Open");
                StartCoroutine(WaitForAnimationToEnd("BookOpened", () =>
                {
                    ShowBookCanvasGroup();
                    _isGuideBookOpen = true;
                }));
            });
        
    }

    IEnumerator GuideBookHide()
    {
        Debug.Log("Hide guidebook");

        // 1️⃣ 컨텐츠 먼저 숨기기 (책 페이지 + 버튼)
        HideBookCanvasGroup();
        yield return new WaitForSeconds(0.2f); // 살짝 기다려서 부드러운 전환

        // 2️⃣ 닫힘 애니메이션 실행
        _guideBookAnimator.SetTrigger("Close");
        yield return StartCoroutine(WaitForAnimationToEnd("BookClose"));

        // 3️⃣ 닫힘 애니메이션 완료 후 Dotween 실행
        _guideBook.transform.DORotate(new Vector3(0f, 0f, 30f), 1f);
        _guideBook.transform.DOLocalMove(new Vector3(1142, -46, 0), 1f)
            .OnComplete(() =>
            {
                BackFadeOff(1f);
                _isGuideBookOpen = false;      // 가이드북 닫힘 상태로 설정
            });
        
    }

    IEnumerator WaitForAnimationToEnd(string animationName, System.Action onComplete = null)
    {
        while (!_guideBookAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName) ||
               _guideBookAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }

        Debug.Log($"{animationName} 애니메이션 완료됨!");

        onComplete?.Invoke();
    }

    public void ChangeIndex(int index)
    {
        Debug.Log($"{index}번째 인덱스");

        for (int i = 0; i < _guideBookCanvasGroup.Length; i++)
        {
            _guideBookCanvasGroup[i].alpha = (i == index) ? 1 : 0;
            _guideBookCanvasGroup[i].blocksRaycasts = (i == index);
            _guideBookCanvasGroup[i].interactable = (i == index);
        }
    }
    
    public void HideBookCanvasGroup()
    {
        Debug.Log("Hiding book canvas group...");

        // 📌 모든 컨텐츠 숨기기 (책 페이지 + 버튼)
        foreach (var canvasGroup in _guideBookCanvasGroup)
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        foreach (Button indexButton in _indexButton)
        {
            indexButton.interactable = false;
        }
    }

    public void ShowBookCanvasGroup()
    {
        Debug.Log("Show book canvasgroup");

        foreach (Button indexButton in _indexButton)
        {
            indexButton.interactable = true;
        }
        
        _guideBookCanvasGroup[0].alpha = 1f;
        _guideBookCanvasGroup[0].interactable = true;
        _guideBookCanvasGroup[0].blocksRaycasts = true;
    }


    /// <summary>
    /// 도장에 대한 메소드 입니다.
    /// </summary>

    
    
    void ShowStampPopUp()
    {
        _stampZone.transform.DOLocalMove(new Vector3(280, -320, 0), 0.5f);

    }
    void HideStampPopUp()
    {
        
        _rejectStamp.transform.localPosition = _rejectPsition;
        _approveStamp.transform.localPosition = _approvePsition;
        _stampZone.transform.DOLocalMove(new Vector3(280, -720, 0), 0.5f);

    }
    
    public void Reject()
    {
        Debug.Log("Reject");
    }

    public void Approve()
    {
        Debug.Log("Approve");
    }


    
    /// <summary>
    ///결과창을 보여줄 메소드 입니다.
    /// </summary>
    /// <param name="Text"></param>
    public void ShowReportUI(string Text)
    {
        _report.transform.DOMove(new Vector3(0, 0, 0), 1f);
        _reportText.text = Text;
    }

    /// <summary>
    /// 대화창에 대한 메소드 입니다.
    /// </summary>

    void GetAdventurerData()
    {
        CharacterData =  _adventurer.AdventurerData;
    }
    
    void ShowSpeechBubbleUI()
    {
        _speechBubble.gameObject.SetActive(true);
        _speechButton.interactable = true;


        _speechBubble.DOFade(1f, 0.1f);
        ShowDialogueUI(_dialIndex);
    }

    void HideSpeechBubbleUI()
    {
        _speechBubble.DOFade(0f, 0.1f);
        
        _speechBubble.gameObject.SetActive(false);
        _speechButton.interactable = false;
    }
    
    void ShowDialogueUI(int i)
    {
        if (_dialIndex < CharacterData.DialogSet.Dialog.Count)
        {
            _characterText.text = CharacterData.DialogSet.Dialog[i];
        } 
        else if(_dialIndex == CharacterData.DialogSet.Dialog.Count)
        {
            _characterText.text = "(더 이상 할말이 없어 보인다.)";
        }
    }
    void NextDialogue()
    {
        if (_dialIndex != CharacterData.DialogSet.Dialog.Count)
        {
            _dialIndex++;

            ShowDialogueUI(_dialIndex);
        }
        else
        {
            HideSpeechBubbleUI();
        }
    }

    ///
    ///UI연출적으로 자주 사용되는 요소들입니다.
    /// 

    void BackFadeOn(float time)
    {
        _fadeOutImage.DOFade(0.7f, time);  // 배경 페이드 효과 추가
        _fadeOutImage.raycastTarget = true;
    }

    void BackFadeOff(float time)
    {
        _fadeOutImage.DOFade(0f, time);  // 페이드 효과 해제
        _fadeOutImage.raycastTarget = false;
    }
    
}
