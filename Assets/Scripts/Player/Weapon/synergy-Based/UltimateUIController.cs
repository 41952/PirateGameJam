using UnityEngine;
using DG.Tweening;

public class UltimateUIController : MonoBehaviour
{
    [SerializeField] private CanvasGroup vignetteGroup;
    [SerializeField] private float fadeDuration = 0.5f;

    private Tween fadeTween;

    private void OnEnable()
    {
        GameEvents.OnUltimateStateChanged += OnUltimateChanged;
    }

    private void OnDisable()
    {
        GameEvents.OnUltimateStateChanged -= OnUltimateChanged;
    }

    private void OnUltimateChanged(bool isActive)
    {
        fadeTween?.Kill(); // отменим старую анимацию

        vignetteGroup.gameObject.SetActive(true); // убедимся, что объект активен

        float targetAlpha = isActive ? 1f : 0f;
        fadeTween = vignetteGroup.DOFade(targetAlpha, fadeDuration).OnComplete(() =>
        {
            if (!isActive)
                vignetteGroup.gameObject.SetActive(false); // скрываем после исчезновения
        });
    }
}
