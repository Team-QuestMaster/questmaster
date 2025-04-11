using UnityEngine;
using UnityEngine.EventSystems;

public class EventAudioPlayer : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
{
    // 여러 이벤트 필요에 따라 추가해서 사용 예정
    [SerializeField]
    private AudioClip _onClickAudioClip;
    
    [SerializeField]
    private AudioClip _onDownAudioClip;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (ReferenceEquals(_onClickAudioClip, null))
        {
            // 오디오가 없으면 재생 안함
            return;
        }
        AudioManager.Instance.PlaySFX(_onClickAudioClip);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (ReferenceEquals(_onDownAudioClip, null))
        {
            // 오디오가 없으면 재생 안함
            return;
        }
        AudioManager.Instance.PlaySFX(_onDownAudioClip);
    }
}
