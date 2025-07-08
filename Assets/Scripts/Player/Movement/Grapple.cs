using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    [SerializeField] private float reelSpeed = 5f;
    [SerializeField] private float grappleDuration = 5f;
    [SerializeField] private float grappleCooldown = 5f;

    [Header("jointSettings")]
    [SerializeField] private float spring = 20f;
    [SerializeField] private float damper = 4f;
    [SerializeField] private float massScale = 1f;

    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;

    private bool isOnCooldown = false;
    private float grappleTimer = 0f;
    private float cooldownTimer = 0f;

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        InputHolder.GetAction(TypeInputAction.Hook).started += context => TryStartGrapple();
        InputHolder.GetAction(TypeInputAction.Hook).canceled += context => StopGrapple();
        lr.positionCount = 0;
    }

    private void Update()
    {
        // Крутим верёвку
        if (joint != null)
        {
            joint.maxDistance = Mathf.Max(joint.minDistance + 1f, joint.maxDistance - reelSpeed * Time.deltaTime);

            // Обновляем таймер зацепа
            grappleTimer -= Time.deltaTime;
            if (grappleTimer <= 0f)
            {
                StopGrapple(); // автоматическое отцепление
            }
        }

        // Обновляем таймер кулдауна
        if (isOnCooldown)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                isOnCooldown = false;
            }
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    private void TryStartGrapple()
    {
        if (isOnCooldown || joint != null) return; // нельзя зацепиться

        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);
            joint.maxDistance = distanceFromPoint * 0.85f;
            joint.minDistance = distanceFromPoint * 0.05f;

            joint.spring = spring;
            joint.damper = damper;
            joint.massScale = massScale;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;

            // Запускаем таймер зацепа
            grappleTimer = grappleDuration;
        }
    }

    private void StopGrapple()
    {
        if (joint != null)
        {
            Destroy(joint);
            joint = null;
            lr.positionCount = 0;

            // Запускаем кулдаун
            isOnCooldown = true;
            cooldownTimer = grappleCooldown;
        }
    }

    private Vector3 currentGrapplePosition;

    private void DrawRope()
    {
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);

        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    public bool IsGrappling() => joint != null;

    public Vector3 GetGrapplePoint() => grapplePoint;
}
