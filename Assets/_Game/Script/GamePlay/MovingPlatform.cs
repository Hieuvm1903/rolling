using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Transform start;
    [SerializeField] Transform end;
    [SerializeField] float time;
    private void Start()
    {
        transform.position= start.position;
        transform.DOMove(end.position,time).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutSine);

    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
}
