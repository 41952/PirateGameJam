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

    private void OnEnable()
    {
        GameEvents.OnPlayerHealthChanged += OnHealthChanged;
    }
    private void OnDisable()
    {
        GameEvents.OnPlayerHealthChanged -= OnHealthChanged;
    }
    private void OnHealthChanged(float currentHealth,float maxHealth)
    {
        healthText.text = $"{Mathf.Round(currentHealth)}/{maxHealth}";
        healthFillImage.fillAmount = currentHealth/maxHealth;
        healthPanel.sizeDelta = new Vector2 (healtPanelBaseWidth + maxHealth * healthPanelScaleModifier, healthPanel.sizeDelta.y);
    }

    private void OnExpChanged()
    {

    }
}
