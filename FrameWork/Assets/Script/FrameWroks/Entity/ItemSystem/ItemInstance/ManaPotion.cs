using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : MonoBehaviour, ItemOnGUIDoubleClickable {

    public float addMana = 100;

    public void ItemOnGUIDoubleClick (ItemHandleOnGUI obj)
    {
        if (GameCentalPr.Instance.PlayerProcessor.AddMana(addMana))
        {
            obj.Clean();
        }
    }
    

}
