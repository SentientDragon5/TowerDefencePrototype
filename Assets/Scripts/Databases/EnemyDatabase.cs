using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyDatabase", menuName = "Database/ Enemy")]
public class EnemyDatabase : ScriptableObject
{
    [Tooltip("Put in order of Tier, as an index")]
    public GameObject[] SimplePrefabs;
    public GameObject[] LightPrefabs;
    public GameObject[] HeavyPrefabs;
}
