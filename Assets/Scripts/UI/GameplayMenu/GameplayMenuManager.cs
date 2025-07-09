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

    private void OnEnable()
    {
        GameEvents.OnPlayerHealthChanged += OnHealthChanged;
        GameEvents.OnPlayerXPGained += OnExpChanged;
        GameEvents.OnPlayerLevelUp += OnLvlUp;
        GameEvents.OnAmmoChanged += OnAmmoChanged;
    }
    private void OnDisable()
    {
        GameEvents.OnPlayerHealthChanged -= OnHealthChanged;
        GameEvents.OnPlayerXPGained -= OnExpChanged;
        GameEvents.OnPlayerLevelUp -= OnLvlUp;
        GameEvents.OnAmmoChanged -= OnAmmoChanged;
    }
    private void OnHealthChanged(float currentHealth,float maxHealth)
    {
        healthText.text = $"{Mathf.Round(currentHealth)}/{maxHealth}";
        healthFillImage.fillAmount = currentHealth/maxHealth;
        healthPanel.sizeDelta = new Vector2 (healtPanelBaseWidth + maxHealth * healthPanelScaleModifier, healthPanel.sizeDelta.y);
    }

    private void OnExpChanged(PlayerExperience playerXP, int amount)
    {
        //expText.text = $"{Mathf.Round(playerXP.CurrentXP)}/{playerXP.XPForNextLevel}";
        expText.text = $"{playerXP.XPForNextLevel}";
        //expFillImage.fillAmount = playerXP.CurrentXP / playerXP.XPForNextLevel;
    }
    private void OnLvlUp(PlayerExperience playerXP, int newLevel)
    {

    }

    private void OnAmmoChanged(int currentAmmo, int maxAmmo)
    {
        ammoText.text = $"{currentAmmo}/{maxAmmo}";
    }
}
