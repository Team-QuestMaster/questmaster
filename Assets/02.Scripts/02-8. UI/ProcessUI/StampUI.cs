using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StampUI : MonoBehaviour
{
    
   
    [SerializeField] private Image _stampZone;
    [SerializeField] private Image _approveStamp;
    [SerializeField] private Animator _approveAnimator;
    [SerializeField] private Image _rejectStamp;
    [SerializeField] private Animator _rejectAnimator;
    //[SerializeField] private Button _closeStampBtn;
    [SerializeField] private Image _popUpButton;
    
    [SerializeField] private UnderChecker _approveUnderChecker;
    [SerializeField] private UnderChecker _rejectUnderChecker;

    [Header("Audio")]
    [SerializeField]
    private AudioClip _onStampBoardMove;
    
    private Vector3 _approvePosition;
    private Vector3 _rejectPosition;
    private bool _show;
    private bool _moving = false;

    public event Action UIApproveEvent;
    public event Action UIRejectEvent;
    public event Action UIStampBackEvent;


   // private Tweener _tweener;
   private void Start()
   {
       Initialize();
   }


   public void Initialize()
    {

        //_popUpButton.onClick.AddListener(StampInteract);
        _approvePosition = _approveStamp.transform.localPosition;
        _rejectPosition = _rejectStamp.transform.localPosition;
        UIApproveEvent += () =>
        {
            _approveAnimator.SetTrigger("Stamp");
            _approveAnimator.SetBool("Hover", false);
        };
        UIRejectEvent += () =>
        {
            _rejectAnimator.SetTrigger("Stamp");
            _rejectAnimator.SetBool("Hover", false);
        };
        
        
        UIStampBackEvent += () =>
        {
            _approveAnimator.SetBool("Hover", false);
            _rejectAnimator.SetBool("Hover", false);
            _rejectUnderChecker.Interactable = true;
            _approveUnderChecker.Interactable = true;

        };
        
        
    }

    public void StampInteract()
    {
        if (!_show)
        {
            ShowStampPopUp();
            _approveStamp.gameObject.SetActive(!_show);
            _rejectStamp.gameObject.SetActive(!_show);
        }
        else
        {
            HideStampPopUp();
            
        }
        
        _show = !_show;
    }

    public void ApproveHover()
    {
        _approveAnimator.SetBool("Hover", true);
    }

    public void RejectHover()
    {
        _rejectAnimator.SetBool("Hover", true);
    }
    
    
    public void SetAlpha(float alpha)
    {
        HoverStampPopUp();
        Color color = _popUpButton.color; // 현재 색상 가져오기
        color.a = alpha;                  // 알파 값 변경
        _popUpButton.color = color; 
    }

    void HoverStampPopUp()
    {
        if (!_moving)
        {
            _stampZone.transform
                .DOShakePosition(0.1f, new Vector3(0, 5, 0), 10, 90, false, true)
                .SetRelative();
        }
    }
    
    void ShowStampPopUp()
    {
        AudioManager.Instance.PlaySFX(_onStampBoardMove);
        _moving = true;
        _stampZone.transform.DOLocalMove(new Vector3(280, -500, 0), 0.5f).OnComplete(() => _moving = false);
    }

    void HideStampPopUp()
    {
        AudioManager.Instance.PlaySFX(_onStampBoardMove);
        _moving = true;
        
        StampBack();
        _stampZone.transform.DOLocalMove(new Vector3(280, -710, 0), 0.5f).OnComplete(() =>
        {
            _approveStamp.gameObject.SetActive(false);
            _rejectStamp.gameObject.SetActive(false);
            _moving = false;
        });
    }


    public void StampBack()
    {
        _approveStamp.transform.localPosition = _approvePosition;
        _rejectStamp.transform.localPosition = _rejectPosition;
        UIStampBackEvent?.Invoke();
    }

    public void Reject()
    {
        UIRejectEvent?.Invoke();
    }

    public void Approve()
    {
        UIApproveEvent?.Invoke();
    }
}