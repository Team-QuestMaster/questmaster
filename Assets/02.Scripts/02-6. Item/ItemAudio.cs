using UnityEngine;
using UnityEngine.EventSystems;

public class ItemAudio : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private AudioClip _grabSound;
    [SerializeField]
    private AudioClip _dropSound;
    public void OnPointerDown(PointerEventData eventData)
    {
        if(ReferenceEquals(_grabSound, null))
        {
            return;
        }
        AudioManager.Instance.PlaySFX(_grabSound);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if(ReferenceEquals(_dropSound, null))
        {
            return;
        }
        AudioManager.Instance.PlaySFX(_dropSound);
    }
}
