using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

    Slider healthSlider;

    private void Start()
    {
        healthSlider = GetComponent<Slider>();
        GameCentalPr.Instance.Adapter_Healthbar -= ValueChange;
        GameCentalPr.Instance.Adapter_Healthbar += ValueChange;
    }

    public void ValueChange(float current,float max)
    {
        if (healthSlider != null)
        {
            healthSlider.value = current / max;
        }
    }
}
