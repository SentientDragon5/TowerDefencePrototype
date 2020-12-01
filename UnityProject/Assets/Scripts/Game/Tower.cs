using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : DestructableObject
{
    [Header("Configure")]
    public TowerProfile Profile;

    public virtual void Awake()
    {
        ImportDataFromProfile(Profile);
    }

    public override void Update()
    {
        base.Update();
    }

    public virtual void ImportDataFromProfile(TowerProfile profile)
    {
        MaxHealth = profile.MaxHealth;
    }
}
