using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class
    Ball : MonoBehaviour
{
    [SerializeField] float speed = 150f;
    [SerializeField] ParticleSystem ps;
    [SerializeField] Transform skinBase;

    GameObject skinHead;
    GameObject currentSkin;
    public Rigidbody rb;
    Vector3 velocity;
    Vector3 contactPoint;
    [SerializeField, Min(0.1f)]
    float ballRadius = 1f;
    public static float ratio;
    Vector2 anchor;
    Vector2 newPos;

    bool isDrag;
    bool isGround;
    public static Transform InitPos;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        isDrag = false;
        
        ChangeSkin();
    }
    private void Start()
    {
        Magnet.target = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsState(GameState.Play))
        {
            isGround = CheckGround();
            if (Input.GetMouseButtonDown(0))
            {
                isDrag = true;
                anchor = Camera.main.ScreenToViewportPoint(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDrag = false;
                anchor = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                velocity = Vector3.zero;
            }

            if (isDrag )
            {
                newPos = Camera.main.ScreenToViewportPoint(Input.mousePosition);
                Vector2 diff = newPos - anchor;
                velocity = new Vector3(diff.x, 0, diff.y * ratio) * speed;
                float v = velocity.magnitude;
                velocity = CamerManager.Instance.currentCam.transform.TransformDirection(velocity);
                velocity = transform.InverseTransformDirection(velocity).normalized  ;
                
                anchor = newPos;
                if (isGround)
                {
                    ballRadius = 1;
                    velocity = velocity * v;
                    velocity.y = -5;
                    
                }
                else
                {
                    velocity = velocity * 10;
                    ballRadius = 0.7f;
                }
                
                rb.AddRelativeForce(velocity
                        , ForceMode.Acceleration
                        );

            }

            
            
        }
        

    }
    private void FixedUpdate()
    {
        UpdateBall();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NenNha"))
        {
            OnInit();
            
            skinBase.gameObject.SetActive(false);
            GameManager.Instance.Replay();



        }

    }
    private void OnCollisionEnter(Collision collision)
    {

        if (rb.velocity.magnitude > 15)
        {
            Instantiate(ps, collision.contacts[0].point, Quaternion.identity);
            SoundManager.Instance.Collision();
        }
        contactPoint = collision.contacts[0].point;

    }
    
    public void OnInit()
    {
        velocity = Vector3.zero;
        rb.velocity = Vector3.zero;
        transform.position = InitPos.position;
        
        transform.rotation = InitPos.rotation;
        CamerManager.Instance.InitCam();
        ChangeSkin();
    }
  
    void UpdateBall()
    {
        Vector3 movement = rb.velocity * Time.deltaTime;
        float distance = movement.magnitude;
        float angle = distance * Mathf.Rad2Deg / ballRadius;
        currentSkin.transform.localRotation =
            Quaternion.Euler(Vector3.right * angle) * currentSkin.transform.localRotation;
        if (rb.velocity.sqrMagnitude > 0.0001f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(rb.velocity), 0.5f);

        }
    }
     
    
    bool CheckGround()
    {
        RaycastHit hit;
        float groundRayLength = 0.7f;

        if (Physics.Raycast(transform.position, -transform.up, out hit, groundRayLength) 
            || Physics.Raycast(transform.position, (contactPoint-transform.position), out hit, 1f)
            || Physics.Raycast(transform.position, Vector3.down, out hit, groundRayLength)
            )
        {
            //Vector3 groundNormal = hit.normal;
            return true;
        }
        return false;
    }
    public void Pause()
    {
        velocity = rb.velocity;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
    }
    public void Continue()
    {
        skinBase.gameObject.SetActive(true);
        rb.velocity = velocity;
        rb.isKinematic = false;
    }

    public Transform SkinTF()
    {
        return currentSkin.transform;
    }
    public void ChangeSkin()
    {
        if(currentSkin)
        {
            Destroy(currentSkin);
        }
        if(skinHead)
        {
            Destroy(skinHead);
        }
        SkinData data = DataManager.Instance.GetSkin(DataManager.Instance.GetLastSkin());
        currentSkin = Instantiate(data.skinTire, transform.position, transform.rotation, skinBase);
        if(data.skinHead)
        skinHead = Instantiate(data.skinHead, transform.position, transform.rotation, skinBase);
        skinBase.gameObject.SetActive(true);
    }




}
