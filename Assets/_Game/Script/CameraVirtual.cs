using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVirtual : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera CVC => GetComponent<CinemachineVirtualCamera>();
    CinemachineOrbitalTransposer COT => CVC?.GetCinemachineComponent<CinemachineOrbitalTransposer>();
    Rigidbody rb => CVC?.Follow.GetComponent<Rigidbody>();
  



    private void LateUpdate()
    {
        if(COT)
        {
            float v = rb.velocity.magnitude;
            COT.m_RecenterToTargetHeading.m_WaitTime = 100f / (v * v + 0.1f);
            COT.m_RecenterToTargetHeading.m_RecenteringTime = 50 / (v * v + 0.1f);
        }    

    }
    public void Close()
    {
        //COT.m_XAxis.Value = 0;
        gameObject.SetActive(false);
    }
    public void OnInit()
    {
        COT.m_XAxis.Value = 0;
    }
}
