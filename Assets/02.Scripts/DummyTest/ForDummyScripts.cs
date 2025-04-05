using System;
using UnityEngine;

public class ForDummyScripts : MonoBehaviour
{

    [SerializeField] private bool Approved;

    private void Start()
    {
        ApproveDummy();
    }


    public void ApproveDummy()
    {
       // UIManager.Instance.StampUI.UIApproveEvent += () => Debug.Log("ForDummyScripts Approved"); ;
    }
    
    
   
    
}
