using UnityEngine;

[CreateAssetMenu(fileName = "New SpawnGroup", menuName = "Waves/ SpawnGroup")]
public class SpawnGroupAsset : ScriptableObject
{
    public int[] SimpleEnemies;
    public int[] LightEnemies;
    public int[] HeavyEnemies;
}
