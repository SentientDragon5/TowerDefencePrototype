using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PurchaseUISlotSet : MonoBehaviour
{
    public enum ButtonType {SelectButton, CurrentBalance, BuildButton}

    public ButtonType Type;
    BuildManager buildManager;
    public BuildingDatabase database;
    public int ID;
    public TextMeshProUGUI purchaseName;
    public TextMeshProUGUI purchaseCost;
    Button button;
    public ButtonColors NormalColors;
    public ButtonColors LowFundColors;
    private TowerProfile profile;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    private void Update()
    {
        if (Type == ButtonType.SelectButton)
        {
            profile = database.Profiles[ID];
            int cost = profile.cost;

            button = GetComponent<Button>();
            ColorBlock cb = button.colors;
            cb.normalColor = buildManager.Balance >= cost ? NormalColors.NormalColor : LowFundColors.NormalColor;
            cb.highlightedColor = buildManager.Balance >= cost ? NormalColors.HilightedColor : LowFundColors.HilightedColor;
            cb.pressedColor = buildManager.Balance >= cost ? NormalColors.PressedColor : LowFundColors.PressedColor;
            cb.selectedColor = buildManager.Balance >= cost ? NormalColors.SelectedColor : LowFundColors.SelectedColor;
            cb.disabledColor = buildManager.Balance >= cost ? NormalColors.DisabledColor : LowFundColors.DisabledColor;
            button.colors = cb;

            purchaseName.text = profile.buildLable;
            purchaseCost.text = cost.ToString();
        }
        if (Type == ButtonType.CurrentBalance)
        {
            purchaseName.text = "Balance";
            purchaseCost.text = buildManager.Balance.ToString();
        }
        if (Type == ButtonType.BuildButton)
        {
            profile = database.Profiles[buildManager.lastID];

            button = GetComponent<Button>();
            ColorBlock cb = button.colors;
            cb.normalColor = buildManager.canBuild ? NormalColors.NormalColor : LowFundColors.NormalColor;
            cb.highlightedColor = buildManager.canBuild ? NormalColors.HilightedColor : LowFundColors.HilightedColor;
            cb.pressedColor = buildManager.canBuild ? NormalColors.PressedColor : LowFundColors.PressedColor;
            cb.selectedColor = buildManager.canBuild ? NormalColors.SelectedColor : LowFundColors.SelectedColor;
            cb.disabledColor = buildManager.canBuild ? NormalColors.DisabledColor : LowFundColors.DisabledColor;
            button.colors = cb;

            purchaseName.text = "Build";
            purchaseCost.text = profile.cost.ToString();
        }
    }
}
[System.Serializable]
public class ButtonColors
{
    public Color NormalColor = Color.white;
    public Color HilightedColor = Color.white;
    public Color PressedColor = Color.white;
    public Color SelectedColor = Color.white;
    public Color DisabledColor = Color.white;
}
