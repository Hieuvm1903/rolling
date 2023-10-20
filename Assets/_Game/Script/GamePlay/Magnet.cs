using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Magnet : MonoBehaviour
{
    [SerializeField] float time = 10;
    [SerializeField] float radius = 7;
    [SerializeField] LayerMask magnetLayer;
    [SerializeField] GameObject magnet;
    [SerializeField] Collider collider1;
    public static Ball target;
    public static bool isMagnet;
    public static float magnetTime;
    public static float magnetRadius;
    static Magnet Instance;
    public static Text text;
    public bool minus;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = new GameObject().AddComponent<Magnet>();
            Instance.magnetLayer = magnetLayer;
        }
        minus = false;
    }
    private void Update()
    {
        if (isMagnet && GameManager.IsState(GameState.Play))
        {
            DisplayTime(magnetTime);
            Instance.minus = true;
           
            if (minus)
            {
                Instance.MagnetExcute();
                
            }
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            MagnetOn(time, radius);
            collider1.enabled = false;
            Destroy(magnet);
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        int minutes = Mathf.FloorToInt(timeToDisplay / 60);
        int seconds = Mathf.FloorToInt(timeToDisplay % 60);

        text.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public static void MagnetOn(float time, float radius)
    {
        isMagnet = true;
        text.gameObject.SetActive(true);
        magnetTime = time;
        magnetRadius = radius;
    }
    void MagnetExcute()
    {
        Collider[] colliders = Physics.OverlapSphere(target.transform.position, magnetRadius, magnetLayer);
        foreach (Collider collider in colliders)
        {
            collider.GetComponent<Coin>()?.MoveToBall(target.transform);
        }
        magnetTime -= Time.deltaTime;
        minus = false;
        if (magnetTime <= 0)
        {
            isMagnet = false;
            text.gameObject.SetActive(false);
        }
        


    }


}

