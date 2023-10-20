using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{

    [SerializeField] List<CheckPoint> checkPoints;
    [SerializeField] ProgressBar[] progressBars;
    [SerializeField] int prize;
    public static Slider slide;
    int maxBar;
    public int Prize { get { return prize; } private set { prize = value; } }
    public Transform InitPos { get; set; }
    public List<CheckPoint> CheckPoints { get { return checkPoints; } set { } }
    private void Start()
    {
        
    }
    // Start is called before the first frame update
    public void OnInit()
    {
        
        InitPos = checkPoints[0].InitPos;
        
    }
    public void FindMax()
    {
        for (int i = 0; i < progressBars.Length; i++)
        {
            if (maxBar < progressBars[i].order)
            {
                maxBar = progressBars[i].order;
            }
        }
    }    
    public void Finish()
    {
        UpdateBar(maxBar);
    }
    public void UpdateBar(int progress)
    {
        float pg = (float)1f*progress / maxBar;
        DOTween.To(() => slide.value, x => slide.value = x,pg, 0.75f);
        //slide.value = pg;
    }
    [ContextMenu("Set progressPoint order")]
    void SetProgress()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(this);
        Undo.RecordObject(this, this.name);
        PrefabUtility.RecordPrefabInstancePropertyModifications(this);

        for (int i = 0; i < progressBars.Length; i++)
        {
            progressBars[i].order= i;
        }
#endif

    }    
}
