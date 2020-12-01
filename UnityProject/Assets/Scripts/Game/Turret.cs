using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : DestructableObject
{
    [Header("Configure")]
    public TurretProfile Profile;

    //Configured by Profile Asset
    float radius = 10;
    float BulletSpeed = 100;
    float BulletDamage = 1f;
    float BulletLifeTime = 1f;
    GameObject BulletPrefab;
    float firingRate;

    public LayerMask EnemyLayer = 1;
    public Transform[] Barrels;
    public Transform pivot;

    [Header("Current Data")]
    public Transform Target;
    public List<Transform> Targets = new List<Transform>();
    float lastFired;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    void Awake()
    {
        ImportDataFromProfile(Profile);
        Health = MaxHealth;
    }

    void ImportDataFromProfile(TurretProfile profile)
    {
        MaxHealth = profile.MaxHealth;

        radius = profile.radius;
        BulletSpeed = profile.BulletSpeed;
        BulletDamage = profile.BulletDamage;
        BulletLifeTime = profile.BulletLifeTime;
        BulletPrefab = profile.BulletPrefab;
        firingRate = profile.FiringRate;
    }

    public override void Update()
    {
        base.Update();

        Targets.Clear();
        foreach(Collider collider in Physics.OverlapSphere(transform.position, radius, EnemyLayer))
        {
            Targets.Add(collider.transform);
        }
        if(Targets.Count > 0)
        {
            Target = Targets[0];
            lookAt(Target);
            if(Time.time - lastFired > firingRate)
            {
                lastFired = Time.time;
                fireAt(Target);
            }
        }
    }

    void lookAt(Transform target)
    {
        Transform LookAt = pivot;
        LookAt.LookAt(target);
        pivot.rotation = Quaternion.Euler(0, LookAt.eulerAngles.y, 0);
    }

    void fireAt(Transform target)
    {
        foreach(Transform Barrel in Barrels)
        {
            GameObject bullet = Instantiate(BulletPrefab, Barrel.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.target = target.position;
            bulletScript.speed = BulletSpeed;
            bulletScript.damage = BulletDamage;
            bulletScript.lifeTime = BulletLifeTime;
        }
    }
}
