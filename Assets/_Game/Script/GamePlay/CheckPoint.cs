using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] Transform initPos;
    [SerializeField] ParticleSystem locked;
    [SerializeField] ParticleSystem unlocked;
    public Transform InitPos { get { return initPos; } }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {

            Ball.InitPos = initPos;
            locked?.Stop();
            unlocked.Play();

        }
    }
}
