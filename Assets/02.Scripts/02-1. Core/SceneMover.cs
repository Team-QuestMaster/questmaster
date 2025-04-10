using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMover : Singleton<SceneMover>
{

    public void MoveScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
}
