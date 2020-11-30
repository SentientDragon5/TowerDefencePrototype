using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Simplified Character from Standard Assets.
//It is only capable of moving on ground.

public class Character1 : DestructableObject
{
    public int DropAmount = 1;
    public float Damage = 1;

    public float MaxSpeed = 1f;//0.1 = walk, 1 = run, 10 = sprint
    public float MovingTurnSpeed = 360;
    public float StationaryTurnSpeed = 180;
    public float MoveSpeedMultiplier = 1;
    public float AnimSpeedMultiplier = 1;
    public float GroundCheckDistance = 0.3f;

    private Rigidbody Rigidbody;
    private Animator Animator;
    bool IsGrounded = false;
    const float Half = 0.5f;
    float TurnAmount;
    float ForwardAmount;
    Vector3 GroundNormal;

    public override void Start()
    {
        base.Start();
        Animator = GetComponent<Animator>();
        Rigidbody = GetComponent<Rigidbody>();

        Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    public override void OnDie()
    {
        BuildManager.instance.Balance += DropAmount;
        WaveManager.instance.checkForWin();
        base.OnDie();
    }

    public void Move(Vector3 move)
    {

        if (move.magnitude > 1f) move.Normalize();

        move *= MaxSpeed;

        move = transform.InverseTransformDirection(move);

        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, GroundNormal);
        TurnAmount = Mathf.Atan2(move.x, move.z);
        ForwardAmount = move.z;
        AppyExtraTurnRotation();

        HandleGroundedMovement();
        UpdateAnimatior(move);
    }


    void UpdateAnimatior(Vector3 move)
    {
        Animator.SetFloat("Forward", ForwardAmount, 0.1f, Time.deltaTime);
        Animator.SetFloat("Turn", TurnAmount, 0.1f, Time.deltaTime);
        if (move.magnitude > 0) Animator.speed = AnimSpeedMultiplier;
        else Animator.speed = 1;
    }

    void HandleGroundedMovement()
    {
        if (Rigidbody.velocity.magnitude < 0.5f) Rigidbody.velocity = Vector3.zero; //prevent Drifting
    }

    void AppyExtraTurnRotation()
    {
        float turnSpeed = Mathf.Lerp(StationaryTurnSpeed, MovingTurnSpeed, ForwardAmount);
        transform.Rotate(0, TurnAmount * turnSpeed * Time.deltaTime, 0);
    }

    public void OnAnimatorMove()
    {
        if (IsGrounded && Time.deltaTime > 0)
        {
            Vector3 v = (Animator.deltaPosition * MoveSpeedMultiplier) / Time.deltaTime;
            v.y = Rigidbody.velocity.y;
            Rigidbody.velocity = v;
        }

    }

    void CheckGroundStatus()
    {
        RaycastHit hit;
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * GroundCheckDistance));
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hit, GroundCheckDistance))
        {
            GroundNormal = hit.normal;
            IsGrounded = true;
            Animator.applyRootMotion = true;
        }
        else
        {
            IsGrounded = false;
            GroundNormal = Vector3.up;
            Animator.applyRootMotion = false;
        }
    }
}
