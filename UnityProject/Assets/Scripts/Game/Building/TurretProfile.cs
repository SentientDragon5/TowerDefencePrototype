using UnityEngine;

[CreateAssetMenu(fileName = "New Turret", menuName = "Tower Profiles/ Turret")]
public class TurretProfile : TowerProfile
{
    [Header("Turret Settings")]
    public GameObject BulletPrefab;
    public float FiringRate;
    public float radius = 10;

    [Header("Bullet Settings")]
    public float BulletSpeed = 100;
    public float BulletDamage = 1f;
    public float BulletLifeTime = 1f;
}
