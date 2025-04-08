using UnityEngine;
using UnityEngine.UI;

public class ImageShadowManager : Singleton<ImageShadowManager>
{
    [SerializeField]
    private ImageShadow _imageShadow;

    protected override void Awake()
    {
        base.Awake();
    }

    public void SetTargetImage(Image image)
    {
        _imageShadow.ActiveWithTarget(image);
    }

    public void DisableImageShadow()
    {
        _imageShadow.DisableShadow();
    }
}
