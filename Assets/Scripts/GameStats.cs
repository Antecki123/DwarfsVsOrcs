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
    public GameObject gameoverScreen;
    public GameObject winScreen;
    public GameObject observator;

    [Space]
    public float waveDuration = 150f;
    public static int waveCounter = 1;
    private float gameTimer;

    public bool endgame = false;

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
        if (gameTimer <= 0f && waveCounter < 3)
        {
            observator.GetComponent<Animator>().Play("Terrified");
            FindObjectOfType<AudioManager>().PlaySound("Horn");

            gameTimer = waveDuration;
            waveCounter++;
        }
    }

    private void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
            timeToDisplay = 0;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timerDisplay.text = string.Format("{00:00}:{01:00}", minutes, seconds);
    }

    private void WinCondition()
    {
        if (waveCounter == 3 && gameTimer <= 0 && !endgame)
        {
            FindObjectOfType<AudioManager>().PlaySound("WinMusic");
            observator.GetComponent<Animator>().Play("Victory");

            endgame = true;
            winScreen.SetActive(true);
            Time.timeScale = .1f;
        }
    }

    private void LoseCondition()
    {
        if (currentHealth <= 0 && !endgame)
        {
            observator.GetComponent<Animator>().Play("Crying");

            endgame = true;
            gameoverScreen.SetActive(true);
            Time.timeScale = .1f;
        }
    }
}