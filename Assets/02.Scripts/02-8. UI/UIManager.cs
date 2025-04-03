using System.Collections;
using DG.Tweening;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI _dialogue;
    [SerializeField] private Button _settingButton;
    
    [Header("가이드북")]
    [SerializeField] private Button _guideBook;
    [SerializeField] private Button[] _indexButton;
    [SerializeField] private Animator _guideBookAnimator;
    [SerializeField] private CanvasGroup[] _guideBookCanvasGroup;
    [SerializeField] private Image _fadeOutImage;

    [Header("도장")] 
    [SerializeField] private Button _stampZone;
    [SerializeField] private Button _approveBtn;
    [SerializeField] private Button _rejectBtn;
    
    [Header("결과창")]
    [SerializeField] private Image _report;
    [SerializeField] private TextMeshProUGUI _reportText;
    
    
    
    private bool _isGuideBookOpen = false;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _settingButton.onClick.AddListener(ShowSetting);
        _guideBook.onClick.AddListener(ToggleGuideBook);
        _approveBtn.onClick.AddListener(Approve);
        _rejectBtn.onClick.AddListener(Reject);
        

        foreach (Button button in _indexButton)
        {
            button.interactable = false;
        }
    }

    
    
    void ShowSetting()
    {
        Debug.Log("Show setting");
        // TODO: 설정 창 열기 추가
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

        _fadeOutImage.DOFade(0.7f, 1f);  // 배경 페이드 효과 추가
        _guideBook.transform.DOLocalMove(new Vector3(0, -46, 0), 1f)
            .OnComplete(() =>
            {
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
        _guideBook.transform.DOLocalMove(new Vector3(1142, -46, 0), 1f)
            .OnComplete(() =>
            {
                _fadeOutImage.DOFade(0f, 1f);  // 페이드 효과 해제
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

    void Reject()
    {
        Debug.Log("Reject");
    }

    void Approve()
    {
        Debug.Log("Approve");
    }


    public void ShowReportUI(string Text)
    {
        _report.transform.DOMove(new Vector3(0, 0, 0), 1f);
        _reportText.text = Text;
    }
    
    
    void NextDialogue()
    {
        Debug.Log("Show next dialogue");
        // TODO: 대화창 업데이트 로직 추가
    }
}
