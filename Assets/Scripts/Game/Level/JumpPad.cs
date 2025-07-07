using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [Header("Настройки прыжка")]
    public float jumpForce = 15f;

    [Tooltip("Пустышка, указывающая направление джампа")]
    public Transform jumpDirection;
    public SimpleFpsCamera cam;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Body")) return;

        // Получаем корневой объект игрока с Movement и Rigidbody
        Movement playerMovement = other.GetComponentInParent<Movement>();
        if (playerMovement == null) return;

        Rigidbody rb = playerMovement.GetComponent<Rigidbody>();
        if (rb == null) return;

        // Направление от JumpPad
        Vector3 direction = jumpDirection.forward.normalized;

        // Сброс скорости и импульс
        rb.linearVelocity = Vector3.zero;
        rb.AddForce(direction * jumpForce, ForceMode.Impulse);

        // Вычисляем угол поворота по Y (yaw)
        float targetYaw = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        SimpleFpsCamera cam = FindObjectOfType<SimpleFpsCamera>();
        if (cam != null)
        {
            cam.SetYRotation(targetYaw);
        }
    }
}
