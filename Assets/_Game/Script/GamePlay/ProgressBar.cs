using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[SelectionBase]
public class ProgressBar : MonoBehaviour
{
    public int order;
    Level level =>GetComponentInParent<Level>();

    private void OnTriggerEnter(Collider other)
    {
        if(GameManager.IsState(GameState.Play))
        if(other.CompareTag("Ball"))
        {
            UpdateBar();
            


        }
    }
    void UpdateBar()
    {
        level.UpdateBar(order);
    }
    [ContextMenu("Child Count")]
    void ChildCount()
    {
        print(transform.parent.childCount);
    }    
  

}

