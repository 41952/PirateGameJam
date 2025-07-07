using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    [Header("Куда тпхать")]
    public Transform exitPoint;

    [Header("Сброс скорости")]
    public bool resetVelocity = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Body")) return;

        // Получаем игрока и Rigidbody
        Movement playerMovement = other.GetComponentInParent<Movement>();
        if (playerMovement == null) return;

        Rigidbody rb = playerMovement.GetComponent<Rigidbody>();
        if (rb == null) return;

        // Телепорт
        playerMovement.transform.position = exitPoint.position;

        if (resetVelocity)
        {
            rb.linearVelocity = Vector3.zero;
        }

        // Поворот по Y
        float yaw = exitPoint.rotation.eulerAngles.y;
        playerMovement.transform.rotation = Quaternion.Euler(0, yaw, 0);

        // Поворот камеры
        SimpleFpsCamera cam = FindObjectOfType<SimpleFpsCamera>();
        if (cam != null)
        {
            cam.SetYRotation(yaw);
        }
    }
}
