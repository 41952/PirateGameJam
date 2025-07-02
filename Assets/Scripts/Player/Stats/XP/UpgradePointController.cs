using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradePointController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private RewardMenuUI rewardMenuUI;

    private int upgradePoints = 0;
    private bool menuOpen = false;

    private void OnEnable()
    {
        GameEvents.OnPlayerLevelUp += HandleLevelUp;
        GameEvents.OnUpgradeMenuClosed += OnMenuClosed; // <-- добавлено
    }

    private void OnDisable()
    {
        GameEvents.OnPlayerLevelUp -= HandleLevelUp;
        GameEvents.OnUpgradeMenuClosed -= OnMenuClosed; // <-- добавлено
    }

    private void Update()
    {
        if (!menuOpen && upgradePoints > 0 && Keyboard.current.cKey.wasPressedThisFrame)
        {
            OpenUpgradeMenu();
        }
    }

    private void HandleLevelUp(PlayerExperience sender, int newLevel)
    {
        upgradePoints++;
        Debug.Log($"Получено очко прокачки! Всего: {upgradePoints}");

    }

    private void OpenUpgradeMenu()
    {
        if (rewardMenuUI == null)
        {
            Debug.LogWarning("RewardMenuUI не назначен!");
            return;
        }

        menuOpen = true;
        upgradePoints--;

        Time.timeScale = 0f;
        AudioListener.pause = false; // Чтобы музыка играла при паузе

        rewardMenuUI.OpenMenu();
    }

    private void OnMenuClosed()
    {
        Time.timeScale = 1f;
        menuOpen = false;
    }
}
