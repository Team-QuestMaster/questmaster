using DG.Tweening;
using UnityEngine;

public class ButtonInteraction : MonoBehaviour
{
    public void InteractionIn()
    {
        this.gameObject.transform.DOScale(new Vector3(1.2f, 1.2f, 1.2f), 0.1f);
    }

    public void InteractionOut()
    {
        this.gameObject.transform.DOScale(new Vector3(1,1,1), 0.1f);
    }
}
