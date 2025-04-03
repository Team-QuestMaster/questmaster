using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    protected override void Awake()
    {
        base.Awake();
    }

    [SerializeField] 
    private TextMeshProUGUI _dialogue;

    [SerializeField] 
    private Button _settingButton;
    
    
    

}
