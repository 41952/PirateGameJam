using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class GameStatsUI : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject panel;
    [SerializeField] private TMP_Text xpText;
    [SerializeField] private TMP_Text wavesText;
    [SerializeField] private TMP_Text killsText;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button menuButton;

    private int kills;
    private int waves;
    private float elapsedTime;
    private bool statsActive = false;
    private PlayerExperience playerXP;

    private void Start()
    {
        panel.SetActive(false);

        restartButton.onClick.AddListener(RestartGame);
        menuButton.onClick.AddListener(GoToMenu);

        GameEvents.OnWaveStarted += OnWaveStarted;
        GameEvents.OnEnemyDeath += OnEnemyDeath;
        GameEvents.OnPlayerXPGained += OnPlayerXPGained;

        // Найдём PlayerExperience один раз
        playerXP = FindObjectOfType<PlayerExperience>();
    }

    private void Update()
    {
            elapsedTime += Time.unscaledDeltaTime;
    }

    private void OnDestroy()
    {
        GameEvents.OnWaveStarted -= OnWaveStarted;
        GameEvents.OnEnemyDeath -= OnEnemyDeath;
        GameEvents.OnPlayerXPGained -= OnPlayerXPGained;
    }

    public void ShowStats()
    {
        statsActive = true;
        Time.timeScale = 0f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        panel.SetActive(true);

        // Отобразим значения
        xpText.text = $"XP: {playerXP?.CurrentXP ?? 0}";
        wavesText.text = $"Waves: {waves}";
        killsText.text = $"Kills: {kills}";
        timeText.text = $"Time: {FormatTime(elapsedTime)}";
        rankText.text = $"Rank: {CalculateRank()}";
    }

    private void RestartGame()
    {
        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void GoToMenu()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        MusicManager.Instance.PlayMenu();
        SceneManager.LoadScene(0);
    }

    private void OnWaveStarted(int waveNumber)
    {
        waves = waveNumber;
    }

    private void OnEnemyDeath(EnemyHealth enemy)
    {
        kills++;
    }

    private void OnPlayerXPGained(PlayerExperience xp, int amount)
    {
        // XP отображается напрямую из PlayerExperience
    }

    private string FormatTime(float seconds)
    {
        int mins = Mathf.FloorToInt(seconds / 60);
        int secs = Mathf.FloorToInt(seconds % 60);
        return $"{mins:00}:{secs:00}";
    }

    private string CalculateRank()
    {
        int xp = playerXP?.CurrentXP ?? 0;
        float timeMinutes = elapsedTime / 60f;

        int score = 0;
        if (xp >= 1000) score++;
        if (waves >= 10) score++;
        if (kills >= 422) score++;
        if (timeMinutes >= 18) score++;

        return score switch
        {
            4 => "S",
            3 => "A",
            2 => "B",
            1 => "C",
            _ => "D",
        };
    }
}
