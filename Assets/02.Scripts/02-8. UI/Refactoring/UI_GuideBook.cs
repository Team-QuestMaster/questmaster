using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class UI_GuideBook : MonoBehaviour
{
    [SerializeField] private Button _guideBook;
    [SerializeField] private Button[] _indexButton;
    [SerializeField] private Image[] _indexImage;
    [SerializeField] private Animator _guideBookAnimator;
    [SerializeField] private GameObject[] _guideBookPages;
    [SerializeField] private UI_FadeInOut _fadeOutImage;
    [SerializeField] private Button _backButton;
    
    private bool _isGuideBookOpen;
    
    [SerializeField] private AudioClip[] _audioClips;
    enum _audioType
    {
        BookShow,
        BookHide,
        PageChange,
        BookSlide
    }

    private void Awake()
    {
        
    }

    //*********************UI메니저가 바뀐다면 조정될 수도 있음************
    public void GuideBoxInteractable()
    {
        if (!_isGuideBookOpen)
        {
            UIManager.Instance.PlayInteractableSound();
        }
    }
    
    public void GuideBookShow()
    {
        
        
        if (!ReferenceEquals(_guideBook, null))
        {
            
            
            _guideBook.transform.DORotate(Vector3.zero, 1f);
            _guideBook.transform.DOScale(new Vector3(1.7f, 1.7f, 1.7f), 1f);
            _guideBook.transform.DOLocalMove(new Vector3(0, 100, 0), 1f).OnComplete(() =>
            {
                _guideBookAnimator.SetTrigger("Open");
                AudioManager.Instance.PlaySFX(_audioClips[(int)_audioType.BookShow]);
                StartCoroutine(WaitForAnimationToEnd("BookOpened", () =>
                {
                    ShowBookPages();
                    ChangeIndex(0); // 첫 페이지 기본 표시
                    
                    _guideBook.transform.rotation = Quaternion.Euler(0, 0, 0);
                    _backButton.gameObject.SetActive(true);
                    
                }));
            });
        }
    }
    
    public void UIGuideBookHide()
    {
        if (_isGuideBookOpen) StartCoroutine(GuideBookHide());
        
        _guideBook.interactable = true;
        AudioManager.Instance.PlaySFX(_audioClips[(int)_audioType.BookHide]);
    }
    
    void ShowBookPages()
    {
        if (!ReferenceEquals(_indexButton, null))
        {
            foreach (var button in _indexButton)
                if (!ReferenceEquals(button, null)) button.gameObject.SetActive(true);
            foreach (var index in _indexImage)
                if (!ReferenceEquals(index, null)) index.gameObject.SetActive(true);
            {
                
            }
        }
    }
    IEnumerator GuideBookHide()
    {
        
        
        HideBookPages();
        yield return new WaitForSeconds(0.2f);
        _guideBookAnimator.SetTrigger("Close");
        
        yield return WaitForAnyAnimationEnd("BookClose","BookIdle",null);
        AudioManager.Instance.PlaySFX(_audioClips[(int)_audioType.BookSlide]);
        if (!ReferenceEquals(_guideBook, null))
        {
            _guideBook.transform.DORotate(new Vector3(0, 0, 30), 1f);
            _guideBook.transform.DOScale(new Vector3(1f, 1f, 1f), 1f);
            _guideBook.transform.DOLocalMove(new Vector3(1142, -46, 0), 1f).OnComplete(() =>
            {

                _backButton.gameObject.SetActive(false);
                _isGuideBookOpen = false;
            });
        }
    }
    void HideBookPages()
    {
        if (!ReferenceEquals(_guideBookPages, null))
        {
            foreach (GameObject page in _guideBookPages)
            {
                if (!ReferenceEquals(page, null))
                {
                    page.SetActive(false);
                }
            }
        }
    }
    
    public void ChangeIndex(int index)
    {
        if (ReferenceEquals(_guideBookPages, null) || index < 0 || index >= _guideBookPages.Length)
            return;
        

        HideBookPages();

        var group = _guideBookPages[index];
        if (!ReferenceEquals(group, null))
        {
            group.SetActive(true);
        }
        
        AudioManager.Instance.PlaySFX(_audioClips[(int)_audioType.PageChange]);
    }
    
    IEnumerator WaitForAnimation(string animationName, Action onAnimEnd)
    {
        while (!_guideBookAnimator.GetCurrentAnimatorStateInfo(0).IsName(animationName) ||
               _guideBookAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f)
        {
            yield return null;
        }
        onAnimEnd?.Invoke();
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
    
    IEnumerator WaitForAnyAnimationEnd(string animA, string animB, Action onComplete)
    {
        bool aDone = false;
        bool bDone = false;

        StartCoroutine(WaitForAnimation(animA, () => aDone = true));
        StartCoroutine(WaitForAnimation(animB, () => bDone = true));

        // 둘 중 하나라도 끝날 때까지 대기
        yield return new WaitUntil(() => aDone || bDone);

        onComplete?.Invoke();
    }
    
}
