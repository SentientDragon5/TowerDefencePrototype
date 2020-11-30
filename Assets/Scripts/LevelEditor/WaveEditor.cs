using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class WaveEditor : MonoBehaviour
{
    public static WaveEditor instance;
    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(this);
    }

    public int WaveCurrentlyEditing = -1;   

    [Header("Instantiation Of Wave Selection Panels")]
    public Transform panel;
    public GameObject WaveSelectButtonPrefab;

    int numberOfWaves = 5;

    int simpleAmount;
    int lightAmount;
    int heavyAmount;

    [Header("Indicators")]
    public TextMeshProUGUI CurrentIndicator;
    public TextMeshProUGUI simpleAmountIndicator;
    public TextMeshProUGUI lightAmountIndicator;
    public TextMeshProUGUI heavyAmountIndicator;


    [Header("SpawnGroup references")]
    public SpawnGroupAsset[] spawnGroups;
    public SpawnGroupAsset DefaultGroup;
    SpawnGroupAsset CurrentLoaded;

    [Header("Start")]
    public GameStartAsset startAsset;
    public TextMeshProUGUI startBalanceIndicator;
    int startBalance;

    void Start()
    {
        OnSwitchWaveEditing();
        InstantiateWaveSelectionButtons();
        OnChangeEnemyAmount();
        startBalance = startAsset.Balance;
        startBalanceIndicator.text = startBalance.ToString();
    }

    public void ChangeStartBalance(int Amount)
    {
        //startBalance = startAsset.Balance;
        startBalance += Amount;
        startAsset.Balance = startBalance;
        startBalanceIndicator.text = startBalance.ToString();
    }


    public void OnSwitchWaveEditing()
    {
        GetCurrentAmounts();
        CurrentIndicator.text = WaveCurrentlyEditing.ToString();
        UpdateUI();
    }

    public void InstantiateWaveSelectionButtons()
    {
        if (panel.childCount > 0)
        {
            for (int i = 0; i < panel.childCount; i++)
            {
                Destroy(panel.GetChild(i).gameObject);
            }
        }
        for (int wave = 0; wave < numberOfWaves; wave++)
        {
            GameObject selectButton = Instantiate(WaveSelectButtonPrefab, panel);
            selectButton.GetComponent<WaveSelection>().ID = wave;
        }
        startAsset.numberOfWaves = numberOfWaves;
    }

    public void OverwriteToDefaultWaves()
    {
        GetCurrentAmounts();
        spawnGroups[WaveCurrentlyEditing] = DefaultGroup;
        GetCurrentAmounts();
        //SetCurrentAmounts();
        UpdateUI();
    }

    public void DefaultAllOpenWaves()
    {
        for (int wave = 0; wave < numberOfWaves; wave++)
        {
            spawnGroups[wave] = DefaultGroup;
        }
    }

    public void GetCurrentAmounts()
    {
        if (WaveCurrentlyEditing < 0) CurrentLoaded = DefaultGroup;
        else CurrentLoaded = spawnGroups[WaveCurrentlyEditing];

        if(CurrentLoaded.SimpleEnemies.Length != 1 ||
            CurrentLoaded.LightEnemies.Length != 1 ||
            CurrentLoaded.HeavyEnemies.Length != 1) //if there is any problems, reset it to the default asset.
        {
            simpleAmount = DefaultGroup.SimpleEnemies[0];
            lightAmount = DefaultGroup.LightEnemies[0];
            heavyAmount = DefaultGroup.HeavyEnemies[0];

            SetCurrentAmounts();
        }

        simpleAmount = CurrentLoaded.SimpleEnemies[0];
        lightAmount = CurrentLoaded.LightEnemies[0];
        heavyAmount = CurrentLoaded.HeavyEnemies[0];

        //if (WaveCurrentlyEditing < -1) spawnGroups[WaveCurrentlyEditing] = CurrentLoaded;
        //else DefaultGroup = CurrentLoaded;
    }

    public void SetCurrentAmounts()
    {
        if (WaveCurrentlyEditing < 0) CurrentLoaded = DefaultGroup;
        else CurrentLoaded = spawnGroups[WaveCurrentlyEditing];

        CurrentLoaded.SimpleEnemies = new int[1];
        CurrentLoaded.LightEnemies = new int[1];
        CurrentLoaded.HeavyEnemies = new int[1];

        CurrentLoaded.SimpleEnemies[0] = simpleAmount;
        CurrentLoaded.LightEnemies[0] = lightAmount;
        CurrentLoaded.HeavyEnemies[0] = heavyAmount;

        if (WaveCurrentlyEditing < -1) spawnGroups[WaveCurrentlyEditing] = CurrentLoaded;
        else DefaultGroup = CurrentLoaded;
    }

    public void UpdateUI()
    {
        simpleAmountIndicator.text = simpleAmount.ToString();
        lightAmountIndicator.text = lightAmount.ToString();
        heavyAmountIndicator.text = heavyAmount.ToString();
    }

    public void OnChangeEnemyAmount()
    {
        simpleAmountIndicator.text = simpleAmount.ToString();
        lightAmountIndicator.text = lightAmount.ToString();
        heavyAmountIndicator.text = heavyAmount.ToString();

        if (WaveCurrentlyEditing < 0)
        {
            CurrentLoaded = DefaultGroup;
        }
        else
        {
            CurrentLoaded = spawnGroups[WaveCurrentlyEditing];
        }
        CurrentLoaded.SimpleEnemies = new int[1];
        CurrentLoaded.LightEnemies = new int[1];
        CurrentLoaded.HeavyEnemies = new int[1];

        CurrentLoaded.SimpleEnemies[0] = simpleAmount;
        CurrentLoaded.LightEnemies[0] = lightAmount;
        CurrentLoaded.HeavyEnemies[0] = heavyAmount;

        if (WaveCurrentlyEditing < -1)
        {
            spawnGroups[WaveCurrentlyEditing] = CurrentLoaded;
        }
        else
        {
            DefaultGroup = CurrentLoaded;
        }
        
    }

    /// <summary> (Deprecated) Turns out i couldn't get it to show up in the inspector of the event. Go figure. I guess it doesn't like 2 Variables </summary>
    public void ChangeAmountButton(int Type, int Amount)//0 = simple, 1 = light, 2 = heavy. I would use an enum but Events dont support enums.
    {
        GetCurrentAmounts();
        if(Type == 0)
        {
            //Simple
            simpleAmount += Amount;
        }
        if (Type == 1)
        {
            //Light
            lightAmount += Amount;
        }
        if (Type == 2)
        {
            //Heavy
            heavyAmount += Amount;
        }
        SetCurrentAmounts();
        UpdateUI();
    }
    public void ChangeSimpleAmountButton(int Amount)
    {
        GetCurrentAmounts();
        simpleAmount += Amount;
        simpleAmount = Mathf.Clamp(simpleAmount, 0, 30);
        SetCurrentAmounts();
        UpdateUI();
    }
    public void ChangeLightAmountButton(int Amount)
    {
        GetCurrentAmounts();
        lightAmount += Amount;
        lightAmount = Mathf.Clamp(lightAmount, 0, 30);
        SetCurrentAmounts();
        UpdateUI();
    }
    public void ChangeHeavyAmountButton(int Amount)
    {
        GetCurrentAmounts();
        heavyAmount += Amount;
        heavyAmount = Mathf.Clamp(heavyAmount, 0, 30);
        SetCurrentAmounts();
        UpdateUI();
    }

    public void ChangeSimpleAmount(TMP_InputField input)
    {
        int value = int.Parse(input.text); //for integer
        simpleAmount = Mathf.Clamp(value, 0, 30);
        OnChangeEnemyAmount();
    }
    public void ChangeLightAmount(TMP_InputField input)
    {
        int value = int.Parse(input.text); //for integer
        lightAmount = Mathf.Clamp(value, 0, 30);
        OnChangeEnemyAmount();
    }
    public void ChangeHeavyAmount(TMP_InputField input)
    {
        int value = int.Parse(input.text); //for integer
        heavyAmount = Mathf.Clamp(value, 0, 30);
        OnChangeEnemyAmount();
    }

    /// <summary> (Deprecated) Turns out i couldn't get it to show up in the inspector of the event. Go figure. I guess it doesn't like 2 Variables </summary>
    public void ChangeEnemyAmount(TMP_InputField input, int type )
    {
        int value = int.Parse(input.text); //for integer
        if (type == 0) //Simple
        {
            simpleAmount = Mathf.Clamp(value, 0, 30);
        }
        if (type == 1) //Light
        {
            lightAmount = Mathf.Clamp(value, 0, 30);
        }
        if (type == 2) //Heavy
        {
            heavyAmount = Mathf.Clamp(value, 0, 30);
        }
        OnChangeEnemyAmount();
    }

    public void TextToInt(TMP_InputField input)
    {
        numberOfWaves = int.Parse(input.text); //for integer

        numberOfWaves = Mathf.Clamp(numberOfWaves, 1, 80);
    }
}
