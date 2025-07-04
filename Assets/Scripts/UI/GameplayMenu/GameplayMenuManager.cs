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
    private float healthPanelScaleModifier = 5;

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
        healthText.text = $"{currentHealth}/{maxHealth}";
        healthPanel.sizeDelta = new Vector2 (healthPanel.sizeDelta.x + maxHealth * healthPanelScaleModifier, healthPanel.sizeDelta.y);
    }
}
