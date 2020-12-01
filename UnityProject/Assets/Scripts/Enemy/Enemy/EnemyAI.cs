using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
[RequireComponent(typeof(Character1))]
public class EnemyAI : MonoBehaviour
{
    public UnityEngine.AI.NavMeshAgent agent { get; private set; }
    public Character1 character { get; private set; }
    public Transform target;

    public LayerMask AttackLayers;

    List<DestructableObject> AttackTargets = new List<DestructableObject>();
    Collider[] colliders;
    DestructableObject AttackTarget;

    private void Start()
    {
        agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
        character = GetComponent<Character1>();

        agent.updateRotation = false;
        agent.updatePosition = true;

        SetTarget(Target.target.transform);
    }

    private void Update()
    {
        if (target != null) agent.SetDestination(target.position);

        if (agent.remainingDistance > agent.stoppingDistance) character.Move(agent.desiredVelocity);
        else
        {
            character.Move(Vector3.zero);
            colliders = Physics.OverlapBox(transform.position + transform.forward, new Vector3(0.49f, 0.49f, 0.49f), Quaternion.identity, AttackLayers);
            AttackTargets.Clear();
            foreach(Collider collider in colliders)
            {
                if(collider.gameObject.TryGetComponent<DestructableObject>(out DestructableObject obj))
                {
                    if (!AttackTargets.Contains(obj))
                    {
                        AttackTargets.Add(obj);
                    }
                }
            }
            if(AttackTarget != null)
            {
                if (AttackTargets.Contains(Target.target))
                {
                    AttackTarget = Target.target;
                }
                else
                {
                    AttackTarget = AttackTargets[0];
                }
                AttackTarget.Health -= character.Damage * Time.deltaTime;
            }
        }
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }
}

