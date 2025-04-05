using System;
using UnityEngine;

public class MiniCharacterUI : MonoBehaviour
{
    private CharacterUI _customerUI;


    private void Awake()
    {
        _customerUI = UIManager.Instance.CharacterUI;
    }
}
