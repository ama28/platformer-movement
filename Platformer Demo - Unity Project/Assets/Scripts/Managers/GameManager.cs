using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    [Header("Debug Options")]
    // public bool infiniteAmmo;
    // public bool canRestartWithR;
    // public bool infiniteHealth;

    [Header("Core Game Attributes")]
    // public float timeSaveMultiplierIncrement; 

    #region Hidden Counters
    // [HideInInspector] public float curTimeSaveMultiplier = 1; 
    // [HideInInspector] public float stopwatch = 0;
    #endregion

    #region Global Object References
    [HideInInspector] public GameObject player;
    [HideInInspector] public PlayerMovement playerMovement;
    // [HideInInspector] public Player playerScript;
    [HideInInspector] public ShootingManager shootingManager;
    // [HideInInspector] public StatusManager statusManager;
    // [HideInInspector] public UIManager uiManager;
    #endregion

    public override void Awake()
    {
        base.Awake();
        player = GameObject.FindWithTag("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
        // playerScript = player.GetComponent<Player>();
        shootingManager = FindAnyObjectByType<ShootingManager>();
        // statusManager = FindAnyObjectByType<StatusManager>();
        // uiManager = FindAnyObjectByType<UIManager>();
    }

    void Start()
    {
        // stopwatch = 0;
    }

    void Update()
    {
        // temporary for restart debugging behavior
        // if (canRestartWithR && Input.GetKeyDown(KeyCode.R)) RestartGame();

        // incrememnt stage timer
        // stopwatch += Time.deltaTime;

        // trigger game over
        // if (playerScript.curHealth <= 0)
        // {
        //     RestartGame();
        // }
    }

    public void GameOver()
    {
        RestartGame();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    #region Time Save Multiplier Methods
    // public void IncrementTimeSaveMultiplier()
    // {
    //     curTimeSaveMultiplier += timeSaveMultiplierIncrement;
    // }

    // public void ResetTimeSaveMultiplier()
    // {
    //     curTimeSaveMultiplier = 1;
    // }
    #endregion
}
