using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : DestructableObject
{
    public static Target target;
    private void Awake()
    {
        if (target == null) target = this;
        else if (target != this) Destroy(this);
    }
    public UnityEvent OnDieEvent;
    public override void OnDie()
    {
        OnDieEvent.Invoke();
    }
}
