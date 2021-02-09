using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    PlatformFactory platformFactory;
    [SerializeField]
    PlatformMover platformMover;
    [SerializeField]
    ScoreManager score;
    Ball ball;

    [SerializeField]
    GameObject tryAgainMenu;
    bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        platformFactory.Initialize();
        platformMover.Initialize(this);
        ball = GameObject.FindObjectOfType<Ball>();
        ball.Logic = new BallLogic(ball);
        RegisterToEvents();
        tryAgainMenu.SetActive(false);
    }

    void RegisterToEvents()
    {
        EventObserver.instance.OnBallBounce += OnBallBounce;
        EventObserver.instance.OnBallTraspass += OnBallTraspass;
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameOver)
            ManageInput();
    }

    void ManageInput()
    {
        platformMover.RotatePlatformsFromInput(Input.GetAxis("Mouse X"),platformFactory.ActivePlatforms);
    }

   public void ResetGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    void OnBallBounce(object sender,BallBounceEvent ballBounceEvent)
    {
        if(ballBounceEvent.segment.SegmentType.Equals(PlatSegmentType.OBSTACLE))
        {
            tryAgainMenu.SetActive(true);
            Time.timeScale = 0;
            gameOver = true;
        }
    }

    void OnBallTraspass(object sender, BallTraspassEvent ballTraspassEvent)
    {
        score.Add(1);
        platformFactory.CreatePlatform();
        platformMover.MovePlatformsUpwards(platformFactory.ActivePlatforms);
        ball.FreezeBall(platformMover.FramesElapsedInTranslate);
    }
}
