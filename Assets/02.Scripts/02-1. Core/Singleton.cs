using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    private bool _dontDestroy = true; // DontDestroy 속성 설정여부 변수
    private static bool _isQuitting = false; // 에디터 모드 종료시 인스턴스 접근을 막기 위한 플래그 변수
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_isQuitting)
            {
                return null;
            }

            if (_instance == null)
            {
                T[] instances = FindObjectsByType<T>(FindObjectsSortMode.None);

                if (1 < instances.Length)
                {
                    Debug.LogError($"중복된 Singleton {typeof(T)} 인스턴스가 감지되어, 중복 인스턴스 제거를 진행합니다.");
                    for (int i = 1; i < instances.Length; i++)
                    {
                        Destroy(instances[i].gameObject);
                    }
                }

                _instance = instances.Length > 0 ? instances[0] : null;
                if (_instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name, typeof(T));
                    _instance = go.GetComponent<T>();
                }
            }
            return _instance;
        }
    }
    protected virtual void Awake()
    {
        if (_instance != null)
        {
            Destroy(transform.root.gameObject);
            return;
        }
        else
        {
            _instance = this as T;
        }

        if (_dontDestroy && transform.parent != null && transform.root != null)
        {
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            GameObject rootGO = GameObject.FindGameObjectWithTag(nameof(Tags.Singleton));
            if (rootGO != null)
            {
                transform.SetParent(rootGO.transform);
            }
            else if (_dontDestroy)
            {
                DontDestroyOnLoad(gameObject);
            }
        }
    }

    private void OnApplicationQuit()
    {
        _isQuitting = true;
    }

    protected virtual void OnDestroy()
    {
        _instance = null;
    }
}

