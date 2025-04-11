using UnityEngine;
using UnityEngine.UI;

public class ButtonOnClickSetter : MonoBehaviour
{
    [SerializeField]
    private Button _button;
    private void Start()
    {
        _button.onClick.RemoveAllListeners();
        _button.onClick.AddListener
            (() => SceneChangeManager.Instance.LoadScene(nameof(SceneNameEnum.StartScene)));
    }
}
