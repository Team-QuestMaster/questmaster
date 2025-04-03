using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    
    [SerializeField] 
    private TextMeshProUGUI _dialogue;

    [SerializeField] 
    private Button _settingButton;
    
    
    protected override void Awake()
    {
        base.Awake();
    }

   
    
    
    

}
