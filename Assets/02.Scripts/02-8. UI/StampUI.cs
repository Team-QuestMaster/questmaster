using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StampUI : MonoBehaviour
{
    [SerializeField] private Button _stampZone;
    [SerializeField] private Image _approveStamp;
    [SerializeField] private Image _rejectStamp;
    [SerializeField] private Button _closeStampBtn;

    private Vector3 _approvePosition;
    private Vector3 _rejectPosition;

    public void Initialize()
    {
        _stampZone.onClick.AddListener(ShowStampPopUp);
        _closeStampBtn.onClick.AddListener(HideStampPopUp);
        _approvePosition = _approveStamp.transform.localPosition;
        _rejectPosition = _rejectStamp.transform.localPosition;
    }

    void ShowStampPopUp()
    {
        _stampZone.transform.DOLocalMove(new Vector3(280, -320, 0), 0.5f);
    }

    void HideStampPopUp()
    {
        _approveStamp.transform.localPosition = _approvePosition;
        _rejectStamp.transform.localPosition = _rejectPosition;
        _stampZone.transform.DOLocalMove(new Vector3(280, -720, 0), 0.5f);
    }

    public void Reject() => Debug.Log("Reject");
    public void Approve() => Debug.Log("Approve");
}