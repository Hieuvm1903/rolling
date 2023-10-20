using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] ParticleSystem ps;
    [SerializeField] Collider collider1;
    bool isMagnet=false;
    Transform target;
    private void Start()
    {

        transform.rotation = Quaternion.Euler(0, 0, 0);
        transform.DORotate(Vector3.up * 360, 1f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
    }
    private void Update()
    {
        if(isMagnet && (GameManager.IsState(GameState.Play)))
        {
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime*25f);
            if(Vector3.SqrMagnitude(transform.position-target.position)<0.75f)
            {
                Instantiate(ps, transform.position, Quaternion.identity).Play();
                OnDespawn();
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            OnDeath();
        }
    }
    public void OnDeath()
    {
        GetComponent<Collider>().enabled = false;
        Instantiate(ps, transform.position, Quaternion.identity).Play();
        transform.DORotate(Vector3.up * 540, 0.5f, RotateMode.FastBeyond360);
        transform.DOMoveY(transform.position.y+1, 0.5f).OnComplete(()=>OnDespawn());
    }   
    public void OnDespawn()
    {
        transform.DOKill();
        DataManager.Instance.AddCoin(5);
        SoundManager.Instance.AddCoin();
        isMagnet = false;
        Destroy(gameObject);
    }
    private void OnDestroy()
    {
                transform.DOKill();
    }
    public void MoveToBall(Transform position)
    {
        isMagnet = true;
        collider1.enabled = false;
        target = position;
        
        
    }

}
