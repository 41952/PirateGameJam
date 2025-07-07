using UnityEngine;

using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class MedicAI : EnemyAI
{
    [Header("Medic Settings")]
    public List<EnemyType> priorityOrder;
    public float seekRadius = 10f;
    public float flySpeed = 5f;
    public float acceleration = 10f;            // For smooth velocity
    public float rotationSpeed = 360f;         // Degrees per second

    [HideInInspector] public EnemyHealth currentPatient;
    private Rigidbody rb;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody>();
        stateMachine.Initialize(new MedicIdleState(this, stateMachine));
    }

    void Update()
    {
        stateMachine.Update();
    }
}