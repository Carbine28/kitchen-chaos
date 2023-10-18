using System;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{

    public event EventHandler OnGameStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public static KitchenGameManager Instance { get; private set; }
    private enum State
    {
        WaitingToStart,
        CountdowntoStart,
        GamePlaying,
        GameOver
    }

    private State state;
    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    [SerializeField] private float gamePlayingTimerMax = 10f;
    [SerializeField] private float bonusTime = 7.5f;
    [SerializeField] private float penaltyTime = 3f;
    private bool isGamePaused = false;
    // hey
    private void Awake()
    {
        Instance = this;    
        state = State.WaitingToStart;
        
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += Instance_OnInteractAction;
        DeliveryManager.Instance.OnRecipeSuccess += DeliveryManager_OnRecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
    }

    private void DeliveryManager_OnRecipeFailed(object sender, EventArgs e)
    {
        // Successful delivery, give player a bit more time
        AddTimeToGame(bonusTime);
    }

    private void DeliveryManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        // failed delivery, subtract time from player
        AddTimeToGame(-penaltyTime);
    }

    private void Instance_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart) 
        {
            state = State.CountdowntoStart;
            OnGameStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                break;
            case State.CountdowntoStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsWaiting()
    {
        return state == State.WaitingToStart;
    }
    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountdownToStartActive()
    {
        return state == State.CountdowntoStart;
    }

    public float GetCountDownToStartTimer() { return countdownToStartTimer; }

    public bool IsGameOver() {  return state == State.GameOver; }

    public float GetGamePlayingTimerNormalised()
    {
        return 1 - gamePlayingTimer / gamePlayingTimerMax;
    }

    public void TogglePauseGame()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f; // Manipulates time.deltatime
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    private void AddTimeToGame(float time)
    {
        gamePlayingTimer += time;
        if (gamePlayingTimer > gamePlayingTimerMax)
            gamePlayingTimer = gamePlayingTimerMax;

    }
}
