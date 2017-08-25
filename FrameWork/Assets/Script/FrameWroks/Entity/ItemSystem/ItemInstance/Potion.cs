using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, ItemOnGUIDoubleClickable {

    public float addHealth = 100;

    public void ItemOnGUIDoubleClick(ItemHandleOnGUI obj)
    {
        if (GameCentalPr.Instance.PlayerProcessor.AddHealth(addHealth))
        {
            obj.Clean();
        }
    }
}
