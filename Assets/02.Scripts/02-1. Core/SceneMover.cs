using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : Singleton<SceneMover>
{

    

    protected override void Awake()
    {
        base.Awake();
    }
    

    public void MoveScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
