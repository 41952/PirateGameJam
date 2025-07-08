using UnityEngine;
using DG.Tweening;

public class SimpleFpsCamera : MonoBehaviour
{
    public float sens = 100f;
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform camEffectHolder;
    [SerializeField] private Transform camHolder;
    [SerializeField] private Camera cam;

    [Header("Shake Settings")]
    public float walkShakeStrength = 0.01f;
    public float walkShakeSpeed = 2f;
    public float runShakeStrength = 0.05f;
    public float runShakeDuration = 0.2f;

    [Header("FOV Settings")]
    public float baseFOV = 60f;
    public float sprintFOVBoost = 15f;
    public float aimFOVReduction = 20f;
    private float currentSprintFOV = 0f;
    private float currentAimFOV = 0f;

    [Header("Tilt Settings")]
    public float tiltAmount = 5f;
    public float tiltSpeed = 0.2f;

    [Header("Jump Impact")]
    public float jumpImpactOffset = -0.2f;
    public float jumpImpactDuration = 0.1f;

    private float xRotation;
    private float yRotation;
    private bool isRunning;
    private bool running;
    private bool isWalking;
    private Tween currentShake;
    private Vector2 inputDirection = Vector2.zero;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        cam.fieldOfView = baseFOV;

        InputHolder.GetAction(TypeInputAction.Sprint).performed += context => running = true;
        InputHolder.GetAction(TypeInputAction.Sprint).canceled += context => running = false; 
    }

    void Update()
    {
        HandleLook();
        HandleCameraShake();
        HandleCameraTilt();
        UpdateFOV();
        SetSprintFOV(running ? false : true);
        inputDirection = InputHolder.GetAction(TypeInputAction.Movement).ReadValue<Vector2>();
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sens;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    void HandleCameraShake()
    {
        float horizontalInput = inputDirection.x;
        float verticalInput = inputDirection.y;
        bool isMoving = horizontalInput != 0 || verticalInput != 0;

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

    void HandleCameraTilt()
    {
        
        float targetZ = inputDirection.x * -tiltAmount;
        camEffectHolder.DOLocalRotate(new Vector3(0, 0, targetZ), tiltSpeed).SetEase(Ease.OutQuad);
    }

    void UpdateFOV()
    {
        float targetFOV = baseFOV + currentSprintFOV - currentAimFOV;
        cam.DOFieldOfView(targetFOV, 0.2f);
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

    public void SetYRotation(float newY)
    {
        yRotation = newY;
        camHolder.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    // ============ ПУБЛИЧНЫЕ ЭФФЕКТЫ =============

    public void SetSprintFOV(bool enable)
    {
        currentSprintFOV = enable ? sprintFOVBoost : 0f;
    }

    public void SetAimFOV(bool enable)
    {
        currentAimFOV = enable ? aimFOVReduction : 0f;
    }

    public void PlayJumpImpact()
    {
        camEffectHolder.DOLocalMoveY(jumpImpactOffset, jumpImpactDuration)
            .SetRelative().SetLoops(2, LoopType.Yoyo);
    }

    public void PlayRecoil(float rotationAmount = 5f, float duration = 0.1f)
    {
        camEffectHolder.DOLocalRotate(new Vector3(-rotationAmount, 0, 0), duration)
            .SetRelative().SetLoops(2, LoopType.Yoyo).SetEase(Ease.OutQuad);
    }

    public void PlayFireShake(float shakeStrength = 0.1f, float duration = 0.1f)
    {
        camEffectHolder.DOShakePosition(duration, shakeStrength, 10, 90, false, true);
    }
}
