using UnityEngine;
using TMPro;
public class WaveSelection : MonoBehaviour
{
    public int ID;
    TextMeshProUGUI Text;
    private void Start()
    {
        Text = GetComponentInChildren<TextMeshProUGUI>();
        if (ID == -1) return;
        Text.text = ID.ToString();
    }
    public void SwitchEditor()
    {
        WaveEditor.instance.WaveCurrentlyEditing = ID;
        WaveEditor.instance.OnSwitchWaveEditing();
    }
}
