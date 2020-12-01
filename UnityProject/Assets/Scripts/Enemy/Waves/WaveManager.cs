using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    public static WaveManager instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);
    }

    public EnemyDatabase EnemyDatabase;
    public Transform[] spawnLocs;
    public Wave[] Waves;
    public int CurrentWave;
    int fieldValue = 0;

    public SpawnGroupAsset[] customGroups;
    public bool customGame;

    public UnityEvent OnWin;

    // Start is called before the first frame update
    void Start()
    {
        if (customGame)
        {
            CustomGameStart();
        }
        SpawnWave(0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void checkForWin()
    {
        Collider[] colliders = Physics.OverlapBox(Vector3.zero, Vector3.one * 100);
        List<Character1> characters = new List<Character1>();
        foreach(Collider collider in colliders)
        {
            if (collider.transform.parent != null)
            {
                if (collider.transform.parent.gameObject.TryGetComponent(out Character1 character))
                {
                    characters.Add(character);
                }
            }
        }
        if (CurrentWave >= BuildManager.instance.startAsset.numberOfWaves && characters.Count <= 1)
        {
            OnWin.Invoke();
        }
    }

    void CustomGameStart()
    {
        Waves = new Wave[80];
        int i = 0;
        foreach(SpawnGroupAsset customWave in customGroups)
        {
            SpawnGroupAsset[] custWaveArray = new SpawnGroupAsset[1];
            custWaveArray[0] = customWave;
            Waves[i] = new Wave(custWaveArray);
            i++;
        }
    }


    public void StartNextWave()
    {
        CurrentWave += 1;
        SpawnWave(CurrentWave);
    }

    public void SpawnWave(int wave)
    {
        if (wave > Waves.Length - 1)
        {
            Debug.Log("Wave Not Found");
            return;
        }

        for(int Loc=0; Loc < spawnLocs.Length; Loc++)
        {
            SpawnGroupAsset group = Waves[wave].groups[Loc];
            //Simple Enemies



            for (int Tier = 0; Tier < group.SimpleEnemies.Length; Tier++)
            {
                for (int i = 0; i < group.SimpleEnemies[Tier]; i++)
                {
                    Instantiate(EnemyDatabase.SimplePrefabs[Tier],
                    spawnLocs[Loc].position, Quaternion.identity,
                    spawnLocs[Loc]);
                }
                /*
                Debug.Log(Tier);// + " as simp at loc #" + Loc + " With tier # " + Tier);
                Debug.Log(EnemyDatabase.SimplePrefabs[Tier].name);// + " as simp at loc #" + Loc + " With tier # " + Tier);
                Instantiate(EnemyDatabase.SimplePrefabs[Tier],
                    spawnLocs[Loc].position, Quaternion.identity,
                    spawnLocs[Loc]);
                */
            }

            for (int Tier = 0; Tier < group.LightEnemies.Length; Tier++)
            {
                for (int i = 0; i < group.LightEnemies[Tier]; i++)
                {
                    Instantiate(EnemyDatabase.LightPrefabs[Tier],
                    spawnLocs[Loc].position, Quaternion.identity,
                    spawnLocs[Loc]);
                }
            }

            for (int Tier = 0; Tier < group.HeavyEnemies.Length; Tier++)
            {
                for (int i = 0; i < group.HeavyEnemies[Tier]; i++)
                {
                    Instantiate(EnemyDatabase.HeavyPrefabs[Tier],
                    spawnLocs[Loc].position, Quaternion.identity,
                    spawnLocs[Loc]);
                }
            }

            return;

            foreach (int Tier in group.LightEnemies)
            {
                Instantiate(EnemyDatabase.LightPrefabs[Tier], spawnLocs[Loc].position, Quaternion.identity, spawnLocs[Loc]);
                //Debug.Log(EnemyDatabase.LightPrefabs[Tier].name + " as simp at loc #" + Loc + " With tier # " + Tier);
            }

            foreach (int Tier in group.HeavyEnemies)
            {
                Instantiate(EnemyDatabase.HeavyPrefabs[Tier], spawnLocs[Loc].position, Quaternion.identity, spawnLocs[Loc]);
                //Debug.Log(EnemyDatabase.HeavyPrefabs[Tier].name + " as simp at loc #" + Loc + " With tier # " + Tier);
            }

            return;

            for (int Tier = 0; Tier < group.SimpleEnemies.Length; Tier++)
            {
                for (int Individual = 0; Individual < group.SimpleEnemies[Tier]; Individual++)
                {
                    Instantiate(EnemyDatabase.SimplePrefabs[Tier], spawnLocs[Loc].position, Quaternion.identity, spawnLocs[Loc]);
                    Debug.Log(EnemyDatabase.SimplePrefabs[Tier].name + " as simp #" + Individual + " at loc #" + Loc + " With tier # " + Tier);
                }
            }
            //Light Enemies
            for (int Tier = 0; Tier < group.LightEnemies.Length; Tier++)
            {
                for (int Individual = 0; Individual < group.LightEnemies[Tier]; Individual++)
                {
                    Instantiate(EnemyDatabase.LightPrefabs[Tier], spawnLocs[Loc].position, Quaternion.identity, spawnLocs[Loc]);
                    Debug.Log(EnemyDatabase.SimplePrefabs[Tier].name + " as Light #" + Individual + " at loc #" + Loc + " With tier # " + Tier);
                }
            }
            //Heavy Enemies
            for (int Tier = 0; Tier < group.HeavyEnemies.Length; Tier++)
            {
                for (int Individual = 0; Individual < group.HeavyEnemies[Tier]; Individual++)
                {
                    Instantiate(EnemyDatabase.HeavyPrefabs[Tier], spawnLocs[Loc].position, Quaternion.identity, spawnLocs[Loc]);
                    Debug.Log(EnemyDatabase.SimplePrefabs[Tier].name + " as Heavy #" + Individual + " at loc #" + Loc + " With tier # " + Tier);
                }
            }
        }
    }

    public void SpawnWaveFromTextBox()
    {
        SpawnWave(fieldValue);
    }

    public void TextToInt(TMP_InputField input)
    {
        fieldValue = int.Parse(input.text); //for integer
    }
}
[System.Serializable]
public class Wave
{
    public SpawnGroupAsset[] groups;
    public Wave(SpawnGroupAsset[] groupAssets)
    {
        groups = groupAssets;
    }
}