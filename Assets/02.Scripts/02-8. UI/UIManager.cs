using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public GuideBookUI GuideBookUI;
    public StampUI StampUI;
    public ReportUI ReportUI;
    public CharacterUI CharacterUI;
    public SettingUI SettingUI;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        GuideBookUI.Initialize();
        StampUI.Initialize();
        ReportUI.Initialize();
        CharacterUI.Initialize();
        SettingUI.Initialize();
    }
}