using TMPro;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    [Header("Game Stats")]
    public int startMoney;
    public float startHealth;
    [Space]

    public TMP_Text moneyDisplay;
    public TMP_Text timerDisplay;

    public static int currentMoney;
    public static float currentHealth;

    [Space]
    public float waveDuration = 150f;
    public static int activeWave = 1;
    private float gameTimer;

    private void Awake()
    {
        currentMoney = startMoney;
        currentHealth = startHealth;

        gameTimer = waveDuration;
    }

    private void Update()
    {
        moneyDisplay.text = currentMoney.ToString();

        WinCondition();
        LoseCondition();
        DisplayTime(gameTimer);

        gameTimer -= Time.deltaTime;
        if (gameTimer <= 0f)
        {
            FindObjectOfType<AudioManager>().PlaySound("Horn");

            gameTimer = waveDuration;
            activeWave++;
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
        }
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerDisplay.text = string.Format("{00:00}:{01:00}", minutes, seconds);
    }

    private void WinCondition()
    {
        /*
        if (waveNumber == waveLength && GameObject.FindGameObjectsWithTag("Enemy") == null)
        {
            print("YOU WIN, noooob");
            Time.timeScale = 0;
        }
        */
    }
    
    private void LoseCondition()
    {
        if (currentHealth <= 0)
        {
            //print("YOU LOSE, noooob");
            Time.timeScale = .2f;
        }
    }
    
}
