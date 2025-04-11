using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneNameEnum
{
    StartScene,
    UIScene_Main,
    EndingScene
}


public class SceneChangeManager : Singleton<SceneChangeManager>
{
    [SerializeField]
    private Image _backgroundImage;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(_backgroundImage.gameObject);
    }
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    public void LoadScene(string sceneName)
    {
        _backgroundImage.gameObject.SetActive(true);
        BackGroundFadeOut
            (() => SceneManager.LoadScene(sceneName));
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        BackGroundFadeIn(() => _backgroundImage.gameObject.SetActive(false));
    }
    private void BackGroundFadeOut(params Action[] onCompleteActions)
    {
        _backgroundImage.DOFade(1, 2f).OnComplete(() =>
        {
            foreach (var action in onCompleteActions)
            {
                action?.Invoke();
            }
        });
    }
    private void BackGroundFadeIn(params Action[] onCompleteActions)
    {
        _backgroundImage.DOFade(0, 2f)
            .SetEase(Ease.InQuart)
            .OnComplete(() =>
        {
            foreach (var action in onCompleteActions)
            {
                action?.Invoke();
            }
        });
    }
}
