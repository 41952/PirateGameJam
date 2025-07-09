using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameplayMenuManager : MonoBehaviour
{
    [Header("Health")]
    [SerializeField]
    private RectTransform healthPanel;
    [SerializeField]
    private Image healthFillImage;
    [SerializeField]
    private TMP_Text healthText;
    [SerializeField]
    private float healthPanelScaleModifier = 1;
    [SerializeField]
    private float healtPanelBaseWidth = 400;

    [Header("Expirience")]
    [SerializeField]
    private RectTransform expPanel;
    [SerializeField]
    private Image expFillImage;
    [SerializeField]
    private TMP_Text expText;
    [SerializeField]
    private RectTransform lvlUpIcon;
    [SerializeField]
    private TMP_Text lvlUpText;
    [Header("Ammo")]
    [SerializeField]
    private TMP_Text ammoText;
    [Header("Hook")]
    [SerializeField]
    private Image hookFillImage;
    [Header("Ultimate")]
    private Image ultimateFillImage;


    private void OnEnable()
    {
        GameEvents.OnPlayerHealthChanged += OnHealthChanged;
        GameEvents.OnPlayerXPGained += OnExpChanged;
        GameEvents.OnPlayerLevelUp += OnLvlUp;
        GameEvents.OnAmmoChanged += OnAmmoChanged;
        GameEvents.OnHookCooldownUpdated += OnHookCooldownChanged;
        GameEvents.OnUltimateCooldown += OnUltimateCooldown;
    }

    

    private void OnDisable()
    {
        GameEvents.OnPlayerHealthChanged -= OnHealthChanged;
        GameEvents.OnPlayerXPGained -= OnExpChanged;
        GameEvents.OnPlayerLevelUp -= OnLvlUp;
        GameEvents.OnAmmoChanged -= OnAmmoChanged;
        GameEvents.OnHookCooldownUpdated -= OnHookCooldownChanged;
        GameEvents.OnUltimateCooldown -= OnUltimateCooldown;
    }
    private void OnHealthChanged(float currentHealth,float maxHealth)
    {
        healthText.text = $"{Mathf.Round(currentHealth)}/{maxHealth}";
        healthFillImage.fillAmount = currentHealth/maxHealth;
        healthPanel.sizeDelta = new Vector2 (healtPanelBaseWidth + maxHealth * healthPanelScaleModifier, healthPanel.sizeDelta.y);
    }

    private void OnExpChanged(PlayerExperience playerXP, int amount)
    {
        expText.text = $"{playerXP.CurrentXP-playerXP.XPForCurrentLevel}/{playerXP.MaxPXForNextLevel - playerXP.XPForCurrentLevel}";
        //expText.text = $"{playerXP.CurrentXP}/{playerXP.MaxPXForNextLevel}";
        expFillImage.fillAmount = ((float)playerXP.CurrentXP - playerXP.XPForCurrentLevel) / (playerXP.MaxPXForNextLevel - playerXP.XPForCurrentLevel);
    }
    private void OnLvlUp(PlayerExperience playerXP, int newLevel)
    {
        lvlUpText.text = $"{newLevel}";
    }

    private void OnAmmoChanged(int currentAmmo, int maxAmmo)
    {
        ammoText.text = $"{currentAmmo}/{maxAmmo}";
    }
    private void OnHookCooldownChanged(float currentTime, float maxTime)
    {
        hookFillImage.fillAmount = (maxTime - currentTime)/maxTime;
    }
    private void OnUltimateCooldown(float currentTime, float maxTime)
    {
        ultimateFillImage.fillAmount = currentTime / maxTime;
    }
}
