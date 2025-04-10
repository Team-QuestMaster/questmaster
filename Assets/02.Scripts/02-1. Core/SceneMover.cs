using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : Singleton<SceneMover>
{

    [SerializeField] private AudioSource _bgm;


    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        _bgm = GetComponent<AudioSource>();
        _bgm.Play();
    }

    public void MoveScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
