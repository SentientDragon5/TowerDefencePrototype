using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    [Header("Health")]
    public float MaxHealth;
    public float Health;

    
    public virtual void Start()
    {
        Health = MaxHealth;
    }

    
    public virtual void Update()
    {
        if(Health <= 0)
        {
            OnDie();
        }
    }

    public virtual void OnDie()
    {
        Destroy(this.gameObject);
    }
}
