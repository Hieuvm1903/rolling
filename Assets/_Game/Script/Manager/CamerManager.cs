using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerManager :Singleton<CamerManager>
{
    [SerializeField] Rigidbody rb;
    [SerializeField] CameraVirtual CVC;
    [SerializeField] CameraVirtual CVC2;
    [SerializeField] CameraVirtual CVC3;
    [SerializeField] CameraVirtual CVC4;
    [SerializeField] CameraVirtual FreeLook;
    [SerializeField] CameraVirtual blank;
    public CameraVirtual currentCam;
    private void Start()
    {
        OnInit();
    }
    public void OnInit()
    {
        ChangeCam(CVC3);
    }    
    private void Update()
    {
        if(GameManager.IsState(GameState.Play))
        if (rb.velocity.magnitude > 30)
        {
            ChangeCam(CVC2);
        }
        else
        {
            ChangeCam(CVC);
        }
    }
    public void ChangeCam(CameraVirtual cam)
    {
        currentCam?.Close();
        currentCam = cam;
        currentCam.gameObject.SetActive(true);
    }
    public void World()
    {
        ChangeCam(FreeLook);
        
    }    
    public void Ball()
    {
        ChangeCam(CVC4);
    }    
    public void InitCam()
    {
        CVC.OnInit();
    }
    public void OnWin()
    {
        currentCam.gameObject.SetActive(false);
    }


}
