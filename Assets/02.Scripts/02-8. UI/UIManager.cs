using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    
    [SerializeField] 
    private TextMeshProUGUI _dialogue;

    [SerializeField] 
    private Button _settingButton;
    
    [SerializeField]
    private Button _guideBook;
    
    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _settingButton.onClick.AddListener(() => ShowSetting());
        _guideBook.onClick.AddListener(() => GuideBookShow());
    }

    void ShowSetting()
    {
        Debug.Log("Show setting");
    }

    void GuideBookShow()
    {
        Debug.Log("Show guidebook");
    }

    void GuideBookHide()
    {
        Debug.Log("Hide guidebook");
    }

    void NextDialogue()
    {
        Debug.Log("Show next dialogue");
    }
    
    
    
}
