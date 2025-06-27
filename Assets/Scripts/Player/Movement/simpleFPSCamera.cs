using UnityEngine;
using DG.Tweening;

public class SimpleFpsCamera : MonoBehaviour
{
    public float sens = 100f;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform camEffectHolder;

    public float walkShakeStrength = 0.01f;
    public float walkShakeSpeed = 2f;
    public float runShakeStrength = 0.05f;
    public float runShakeDuration = 0.2f;

    private float xRotation;
    private float yRotation;

    private bool isRunning;
    private bool isWalking;
    private Tween currentShake;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        HandleLook();
        HandleCameraShake();
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sens;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void HandleCameraShake()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool isMoving = horizontalInput != 0 || verticalInput != 0;
        bool running = Input.GetKey(KeyCode.LeftShift);

        if (isMoving)
        {
            if (running && !isRunning)
            {
                StartRunningShake();
            }
            else if (!running && !isWalking)
            {
                StartWalkingShake();
            }
        }
        else
        {
            StopShake();
        }

        isRunning = isMoving && running;
        isWalking = isMoving && !running;
    }

    void StartWalkingShake()
    {
        StopShake();
        currentShake = DOTween.To(() => camEffectHolder.localPosition, x =>
        {
            camEffectHolder.localPosition = x + Vector3.up * Mathf.Sin(Time.time * walkShakeSpeed) * walkShakeStrength;
        }, camEffectHolder.localPosition, 1f).SetLoops(-1, LoopType.Restart).SetEase(Ease.Linear);
    }

    void StartRunningShake()
    {
        StopShake();
        currentShake = camEffectHolder.DOShakePosition(runShakeDuration, runShakeStrength, 10, 90, false, true)
            .SetLoops(-1, LoopType.Restart);
    }

    void StopShake()
    {
        if (currentShake != null && currentShake.IsActive()) currentShake.Kill();
        camEffectHolder.localPosition = Vector3.zero;
        isRunning = false;
        isWalking = false;
    }
}
