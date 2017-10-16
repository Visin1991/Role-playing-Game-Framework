using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : MonoBehaviour, ItemOnGUIDoubleClickable {

    public float addMana = 100;

    public void ItemOnGUIDoubleClick (ItemHandleOnGUI gui)
    {
        if (GameCentalPr.Instance.PlayerProcessor.AddMana(addMana))
        {
            gui.Clean();

            LEInventory leInventory = GetComponentInParent<LEInventory>();
            Item item = GetComponentInChildren<ItemHandleOnObj>().item;
            leInventory.RemoveItem(item);

            DestroyImmediate(gameObject);
        }
    }
    

}
