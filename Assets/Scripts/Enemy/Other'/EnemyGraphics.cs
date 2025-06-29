using UnityEngine;

public class EnemyGraphics : MonoBehaviour
{
    [SerializeField] private GameObject heartObject;

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
