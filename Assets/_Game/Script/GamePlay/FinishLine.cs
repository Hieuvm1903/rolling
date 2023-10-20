using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] ParticleSystem ps;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {

            if (GameManager.IsState(GameState.Play))
            {
                ps.Play();
                GameManager.Instance.Finish();               
            }

        }
    }
}
