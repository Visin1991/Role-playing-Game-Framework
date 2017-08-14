using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryPanel : MonoBehaviour {

    ItemHandle[] allHandles;

    [SerializeField]
    private ItemDataBaseList itemDatabase;

    // Use this for initialization
    void Start () {
        allHandles = GetComponentsInChildren<ItemHandle>();
        List<Item> items = new List<Item>();

        for (int i = 0; i < 20; i++)
        {
            items.Add(itemDatabase.getItemCopyByID(i % 2));
        }

        ReSetItems(items);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            AddItem(itemDatabase.getItemCopyByID(2));
        }
    }

    public void ReSetItems(List<Item> items)
    {
        for (int i = 0; i < items.Count; i++)
        {
            allHandles[i].ResetItem(items[i]);
        }
    }

    public void AddItem(Item item)
    {
        allHandles.First(h => h.IsEmpty).ResetItem(item); 
    }
}
