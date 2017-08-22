using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LEInventory : MonoBehaviour {

    public List<Item> items= new List<Item>();

    InventoryPanel inventoryPanel;

    private void Start()
    {
        ItemHandleOnObj[] handles = transform.GetComponentsInChildren<ItemHandleOnObj>();
        foreach (ItemHandleOnObj handle in handles)
        {
            Item item = handle.GetItem();
            items.Add(item);
            GameUIPr.Instance.AddItemToInventory(item);
        }
    }

    public void AddItem(Item item,Transform itemTransform)
    {
        items.Add(item);
        itemTransform.SetParent(transform);
        GameUIPr.Instance.AddItemToInventory(item);
    }




}
