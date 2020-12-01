using UnityEngine;

[CreateAssetMenu(fileName = "New BuildingDatabase", menuName = "Database/ Building")]
public class BuildingDatabase : ScriptableObject
{
    public GameObject[] Towers;
    public TowerProfile[] Profiles;
}