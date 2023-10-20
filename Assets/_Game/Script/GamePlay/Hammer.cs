using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] RotateMode mode;
    [SerializeField] float from = 120 ;
    [SerializeField] float to = -120;
    [SerializeField] float duration = 3;
    void Start()
    {
        RunTest();
        
    }
    void RunTest()
    {
        Vector3 forward = transform.TransformDirection( transform.forward);
        Vector3 q = (transform.localRotation).eulerAngles;

        q.z = 0;
        

        switch (mode)
        {

            case RotateMode.Fast:
                {
                    transform.localRotation = Quaternion.Euler(forward * from + q);
                    transform.DOLocalRotate(forward * to+q, duration).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutExpo);
                   
                    break;
                }
            case RotateMode.FastBeyond360:
                {
                    transform.localRotation = Quaternion.Euler(q);
                    transform.DORotate(forward * 360+q, duration,RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
                    break;
                }


        }
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }




}
