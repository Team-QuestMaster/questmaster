using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GuideBookUI : MonoBehaviour
{
    [SerializeField] private Button _guideBook;
    [SerializeField] private Button[] _indexButton;
    [SerializeField] private Animator _guideBookAnimator;
    [SerializeField] private CanvasGroup[] _guideBookCanvasGroup;
    [SerializeField] private Image _fadeOutImage;
    [SerializeField] private Button _backButton;

    private bool _isGuideBookOpen;

    

    public void Initialize()
    {
        if (!ReferenceEquals(_backButton, null))
            _backButton.onClick.AddListener(() => UIGuideBookHide());



        if (!ReferenceEquals(_guideBook, null))
            _guideBook.onClick.AddListener(UIGuideBookShow);

        if (!ReferenceEquals(_indexButton, null))
        {
            for (int i = 0; i < _indexButton.Length; i++)
            {
                int index = i; // 클로저 문제 방지용
                if (!ReferenceEquals(_indexButton[index], null))
                    _indexButton[index].onClick.AddListener(() => ChangeIndex(index));

                _indexButton[i].interactable = false;
            }
        }

        // 처음에는 모든 페이지 비활성화
        HideAllCanvasGroups();
    }

    void UIGuideBookShow()
    {
        _backButton.gameObject.SetActive(false);
        if (!_isGuideBookOpen) GuideBookShow();
        _isGuideBookOpen = true;
        _guideBook.interactable = false;
        
    }

    void UIGuideBookHide()
    {
        if (_isGuideBookOpen) StartCoroutine(GuideBookHide());
        _backButton.gameObject.SetActive(false);
        _guideBook.interactable = true;
    }

    public void BookShake()
    {
        if(!_isGuideBookOpen)
        _guideBook.transform.DOShakeRotation( 0.1f, new Vector3(0,0,5));
    }

    
    
    void GuideBookShow()
    {
        
        if (!ReferenceEquals(_guideBook, null))
        {
            
            
            _guideBook.transform.DORotate(Vector3.zero, 1f);
            _guideBook.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 1f);
            _guideBook.transform.DOLocalMove(new Vector3(0, 100, 0), 1f).OnComplete(() =>
            {
                BackFadeOn(1f);
                _guideBookAnimator.SetTrigger("Open");
                StartCoroutine(WaitForAnimationToEnd("BookOpened", () =>
                {
                    ShowBookCanvasGroup();
                    ChangeIndex(0); // 첫 페이지 기본 표시
                    
                    _guideBook.transform.rotation = Quaternion.Euler(0, 0, 0);
                    _backButton.gameObject.SetActive(true);
                    
                }));
            });
        }
    }

    IEnumerator GuideBookHide()
    {
        
        
        HideBookCanvasGroup();
        yield return new WaitForSeconds(0.2f);
        _guideBookAnimator.SetTrigger("Close");
        yield return WaitForAnimationToEnd("BookClose");

        if (!ReferenceEquals(_guideBook, null))
        {
            _guideBook.transform.DORotate(new Vector3(0, 0, 30), 1f);
            _guideBook.transform.DOScale(new Vector3(1f, 1f, 1f), 1f);
            _guideBook.transform.DOLocalMove(new Vector3(1142, -46, 0), 1f).OnComplete(() =>
            {
                BackFadeOff(1f);
                _backButton.gameObject.SetActive(true);
                _isGuideBookOpen = false;
            });
        }
    }

    IEnumerator WaitForAnimationToEnd(string animationName, System.Action onComplete = null)
    {
        while (!_guideBookAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName) ||
               _guideBookAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
        onComplete?.Invoke();
    }

    void ShowBookCanvasGroup()
    {
        if (!ReferenceEquals(_indexButton, null))
        {
            foreach (var button in _indexButton)
                if (!ReferenceEquals(button, null)) button.interactable = true;
        }
    }

    void HideBookCanvasGroup()
    {
        HideAllCanvasGroups();

        if (!ReferenceEquals(_indexButton, null))
        {
            foreach (var button in _indexButton)
                if (!ReferenceEquals(button, null)) button.interactable = false;
        }
    }

    void HideAllCanvasGroups()
    {
        if (!ReferenceEquals(_guideBookCanvasGroup, null))
        {
            foreach (var group in _guideBookCanvasGroup)
            {
                if (!ReferenceEquals(group, null))
                {
                    group.alpha = 0;
                    group.interactable = false;
                    group.blocksRaycasts = false;
                }
            }
        }
    }

    public void IndexIn(int index)
    {
        if (ReferenceEquals(_guideBookCanvasGroup, null) || index < 0 || index >= _guideBookCanvasGroup.Length)
            return;
        _indexButton[index].transform.DOScaleX(0.8f, 0.1f);

    }
    
    public void IndexOut(int index)
    {
        if (ReferenceEquals(_guideBookCanvasGroup, null) || index < 0 || index >= _guideBookCanvasGroup.Length)
            return;
        _indexButton[index].transform.DOScaleX(1f, 0.1f);

    }
    
    public void BackButtonIn()
    {
        if (ReferenceEquals(_backButton, null))
            return;
        _backButton.transform.DOScaleX(0.8f, 0.1f);

    }
    
    public void BackButtonOut()
    {
        if (ReferenceEquals(_backButton, null))
            return;
        _backButton.transform.DOScaleX(1f, 0.1f);

    }
    
    public void ChangeIndex(int index)
    {
        if (ReferenceEquals(_guideBookCanvasGroup, null) || index < 0 || index >= _guideBookCanvasGroup.Length)
            return;
        

        HideAllCanvasGroups();

        var group = _guideBookCanvasGroup[index];
        if (!ReferenceEquals(group, null))
        {
            group.alpha = 1;
            group.interactable = true;
            group.blocksRaycasts = true;
        }
    }

    void BackFadeOn(float time)
    {
        if (!ReferenceEquals(_fadeOutImage, null))
        {
            _fadeOutImage.DOFade(0.7f, time);
            _fadeOutImage.raycastTarget = true;
        }
    }

    void BackFadeOff(float time)
    {
        if (!ReferenceEquals(_fadeOutImage, null))
        {
            _fadeOutImage.DOFade(0f, time);
            _fadeOutImage.raycastTarget = false;
        }
    }
}
