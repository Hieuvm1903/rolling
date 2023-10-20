using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] LevelData levels;
    [SerializeField] Ball ball;
    [SerializeField] Level currentLevel;
    static GameState gameState;
    void Start()
    {
        Application.targetFrameRate = 60;
        Ball.ratio = Screen.height / Screen.width;
        ChangeState(GameState.MainMenu);
        UIManager.Instance.MainMenu();

    }

    // Update is called once per frame


    public void OnInit()
    {
        if(currentLevel)
        {
            Destroy(currentLevel.gameObject);
        }
        currentLevel = Instantiate(levels.GetLevel(DataManager.LevelPassed));
        currentLevel.OnInit();
        Ball.InitPos = currentLevel.InitPos;
        ball.OnInit();
        ChangeSky();
    }
    void ChangeSky()
    {
        Material mat = DataManager.Instance.GetSky(DataManager.Instance.GetLastSky()).material;
        RenderSettings.skybox = mat;
    }
    public void Replay()
    {
        if (DataManager.BallLives > 0)
        {
            DataManager.BallLives--;
            UIManager.Instance.OnLivesChange(false);



        }
        else
        {
            UIManager.Instance.OpenLose();
        }

        
       
    }
    public void Finish()
    {
        currentLevel.Finish();
        ChangeState(GameState.MainMenu);
        SoundManager.Instance.Play();
        CamerManager.Instance.OnWin();
        DataManager.LevelPassed++;
        ball.rb.velocity *= 0.2f;
        Invoke(nameof(EndLevel), 1f);
    }
    void EndLevel()
    {
        UIWin.coin = currentLevel.Prize;
        UIManager.Instance.OpenWin();

    }
    public static bool IsState(GameState state)
    {
        return gameState == state;
    }
    public static void ChangeState(GameState state)
    {
        gameState = state;
    }
    public void Pause()
    {
        ChangeState(GameState.Pause);
        DOTween.PauseAll();
        ball.Pause();
    }
    public void Continue()
    {
        ChangeState(GameState.Play);
        currentLevel.FindMax();
        DOTween.PlayAll();
        ball.Continue();
    }
    public Transform SkinBall()
    {
        return ball.SkinTF();
    }
}
public enum GameMode
{
    Normal,

}
public enum GameState
{
    Play,
    Pause,
    MainMenu
}
