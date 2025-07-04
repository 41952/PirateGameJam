using UnityEngine;

public class EnemyGraphics : MonoBehaviour
{
    [SerializeField] private GameObject heartOutline;
    [SerializeField] private GameObject buffOutline;
    [SerializeField] private GameObject heartObject;
    
    public void ActivateHeartOutline() {
        if (heartOutline != null) heartOutline.SetActive(true);
    }
    public void DeactivateHeartOutline() {
        if (heartOutline != null) heartOutline.SetActive(false);
    }

    public void ActivateBuffOutline() {
        if (buffOutline != null && (heartOutline == null || !heartOutline.activeSelf))
            buffOutline.SetActive(true);
    }
    public void DeactivateBuffOutline() {
        if (buffOutline != null)
            buffOutline.SetActive(false);
    }

    public void ActivateHeart()
    {
        if (heartObject != null)
            heartObject.SetActive(true);
    }

    public void DeactivateHeart()
    {
        if (heartObject != null)
            heartObject.SetActive(false);
    }
}
