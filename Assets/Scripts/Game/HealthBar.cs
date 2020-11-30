using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public DestructableObject obj;
    public Image Bar;
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        if(obj == null)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        if (obj.Health == 0)
        {
            DestroyImmediate(this.gameObject);
            return;
        }
        transform.position = Camera.main.WorldToScreenPoint(obj.transform.position);
        Bar.fillAmount = obj.Health / obj.MaxHealth;
    }
}
