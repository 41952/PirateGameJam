using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public float walkSpeedForward = 5f;
    public float walkSpeedBackward = 3f;
    public float walkSpeedSide = 4f;
    public float runMultiplier = 1.6f;
    public float jumpPower = 5f;

    public float speedMultiplier = 1f;

    private Rigidbody rb;
    private GroundCheck gc;

    private GameObject body;
    private GameObject forwardPoint;

    private bool jumpCooldown;

    private Vector2 inputDirection = Vector2.zero;
    private PlayerControls playerControls;
    private bool isRunning = false;
    private bool jumpPressed = false;

    private PlayerInput playerInput;
    
    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        rb = GetComponent<Rigidbody>();
        gc = GetComponentInChildren<GroundCheck>();
        body = GameObject.FindWithTag("Body");
        forwardPoint = GameObject.FindWithTag("Forward");
        playerControls = InputHolder.GetControls(); ;
        playerInput = GetComponent<PlayerInput>();
        InputHolder.SetupInput(playerInput);

    }
    private void OnEnable()
    {
       



        
        //биндим булеан на ивент нажания и удержание кнопки спринта в управлении
        InputHolder.GetAction(InputActionType.Sprint).started += context => isRunning = true; //нажали
        InputHolder.GetAction(InputActionType.Sprint).performed += context => isRunning = true; //держим
        InputHolder.GetAction(InputActionType.Sprint).canceled += context => isRunning = false; //отпустили

        
    }
    private void OnDisable()
    {
        playerControls.MainActionMap.Disable();// врубаем и вырубаем управление на активации/деактивации скрипта
    }
  

    private void Update()
    {
        inputDirection = InputHolder.GetAction(InputActionType.Movement).ReadValue<Vector2>();
        float x = inputDirection.x;
        float z = inputDirection.y;
        

        Vector3 movementDirection = (x * transform.right + z * transform.forward).normalized;
        float speed = 0f;

        if (z > 0f && x == 0f)
            speed = walkSpeedForward;
        else if (z > 0f)
            speed = walkSpeedForward;
        else if (z < 0f)
            speed = walkSpeedBackward;
        else if (x != 0f)
            speed = walkSpeedSide;

        if (isRunning && z > 0f)
            speed *= runMultiplier;

        speed *= speedMultiplier;

        if (gc.onGround)
        {
            rb.linearVelocity = new Vector3(
                movementDirection.x * speed,
                rb.linearVelocity.y,
                movementDirection.z * speed
            );
        }
        else
        {
            rb.linearVelocity = new Vector3(
                Mathf.Lerp(rb.linearVelocity.x, movementDirection.x * speed, 0.1f),
                rb.linearVelocity.y,
                Mathf.Lerp(rb.linearVelocity.z, movementDirection.z * speed, 0.1f)
            );
        }

        if (InputHolder.GetAction(InputActionType.Jump).triggered && gc.onGround && !jumpCooldown)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpCooldown = true;
            Invoke(nameof(JumpReady), 0.01f);
        }

        if (z < 0f)
            forwardPoint.transform.localPosition = new Vector3(-x, body.transform.localPosition.y, -z);
        else if (x == 0f && z == 0f)
            forwardPoint.transform.localPosition = new Vector3(0f, body.transform.localPosition.y, 1f);
        else
            forwardPoint.transform.localPosition = new Vector3(x, body.transform.localPosition.y, z);

        body.transform.LookAt(forwardPoint.transform);
    }

    private void JumpReady()
    {
        jumpCooldown = false;
    }
}
