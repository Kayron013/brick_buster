using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public BallController _ball;
    public BrickController _brick;

    private BallController mainBall;
    private HandleController handle;

    private ScoreController score;
    private StartButtonController startBtn;

    //private static readonly int[][] brickRowConfigs = {
    //    new int[] {1,2,2,4,5,1,7,3,4,9 },
    //    new int[] {10,3,2,4,2,0,2,3,4,5 },
    //    new int[] {1,2,3,4,2,10,2,3,4,5 },
    //    new int[] {10,3,3,4,5,0,2,3,4,5 },
    //    new int[] {1,2,2,5,5,1,0,3,4,5 },
    //};

    private int brickRows = 7;
    private int brickCols = 10;
    private int numBricks;

    public int currentRound = 1;
    public int currentStreak = 0;

    public bool IsPlaying { get; set; } = false;
    public bool WaveIsActive { get; set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        handle = FindObjectOfType<HandleController>();
        score = FindObjectOfType<ScoreController>();
        startBtn = FindObjectOfType<StartButtonController>();
        BrickGenerator.brick = _brick;
        mainBall = Instantiate(_ball);
    }

    public void StartGame()
    {
        IsPlaying = true;
        numBricks = BrickGenerator.Generate(brickRows, brickCols);
        currentRound = 1;
        currentStreak = 0;
        handle.Reset();
        mainBall.Reset();
        score.Reset();
        TimeKeeper.ResetClock();
        Time.timeScale = 1;
        startBtn.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        // Click to shoot ball and start wave
        if (IsPlaying && !WaveIsActive && Input.GetMouseButtonDown(0))
        {
            WaveIsActive = true;
            StartWave();
        }

        score.SetCurrentTime(TimeKeeper.TimeSpan.ToString("c"));
    }

    void EndGame()
    {
        TimeKeeper.StopClock();
        WaveIsActive = false;
        IsPlaying = false;
        Time.timeScale = 0;
        startBtn.Show();

        if (DataStore.ReportRounds(currentRound))
        {
            score.SetRecordRound(currentRound.ToString());
        }

        if (DataStore.ReportTime(TimeKeeper.TimeSpan))
        {
            score.SetRecordTime(TimeKeeper.TimeSpan.ToString("c"));
        }
    }

    public void RestartRound()
    {
        TimeKeeper.StopClock();
        if (WaveIsActive)
        {
            currentRound++;
        }
        currentStreak = 0;
        WaveIsActive = false;
        mainBall.Reset();
        handle.Reset();
        score.SetCurrentRound(currentRound.ToString());
        score.SetCurrentStreak(currentStreak.ToString());
    }

    void StartWave()
    {
        mainBall.Launch();
        TimeKeeper.StartClock();
    }

    public void BrickDestroyed()
    {
        currentStreak++;
        if (DataStore.ReportStreak(currentStreak))
        {
            score.SetRecordStreak(currentStreak.ToString());
        }
        score.SetCurrentStreak(currentStreak.ToString());


        numBricks--;
        if(numBricks == 0)
        {
            EndGame();
        }
        mainBall.IncreaseSpeed();
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
