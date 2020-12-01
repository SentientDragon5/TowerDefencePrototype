using UnityEngine;

[CreateAssetMenu(fileName = "New WavePrefab", menuName = "Waves/ WavePrefab")]
public class WavePrefab : ScriptableObject
{
    public SpawnGroupAsset[] groups;
}
