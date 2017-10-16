using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : MonoBehaviour, ItemOnGUIDoubleClickable {

    public float addHealth = 100;

    public void ItemOnGUIDoubleClick(ItemHandleOnGUI guiObj)
    {
        if (GameCentalPr.Instance.PlayerProcessor.AddHealth(addHealth))
        {
            guiObj.Clean();
            LEInventory inventory = GetComponent<LEInventory>();
            Item item = GetComponentInChildren<ItemHandleOnObj>().item;
            inventory.RemoveItem(item);
            DestroyImmediate(gameObject);
        }
    }
}
