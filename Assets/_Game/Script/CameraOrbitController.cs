using UnityEngine;
using Cinemachine;

public class CameraOrbitController : CameraVirtual
{   
    public float rotationSpeed = 5f; // Adjust the rotation speed as needed

    private CinemachineFreeLook freeLookCamera;

    private void Start()
    {
        freeLookCamera = GetComponent<CinemachineFreeLook>();
    }

    private void LateUpdate()
    {
      // Rotate the camera around the target
        freeLookCamera.m_XAxis.Value += rotationSpeed * Time.deltaTime;
    }
}
