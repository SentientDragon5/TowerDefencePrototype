using UnityEngine;

[CreateAssetMenu(fileName = "New Wall", menuName = "Tower Profiles/ Wall")]
public class TowerProfile : ScriptableObject
{
    [Header("Build Settings")]
    public string buildLable = "Wall";
    public int cost = 2;

    [Header("Health")]
    public float MaxHealth = 50;
}
