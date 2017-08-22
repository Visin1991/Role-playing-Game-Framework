using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{

    Image manaImage;

    private void Start()
    {
        manaImage = GetComponent<Image>();
        GameCentalPr.Instance.Adapter_Manabar -= ValueChange;
        GameCentalPr.Instance.Adapter_Manabar += ValueChange;
    }

    public void ValueChange(float current, float max)
    {
        if (manaImage != null)
        {
            manaImage.fillAmount = current / max;
        }
    }
}
