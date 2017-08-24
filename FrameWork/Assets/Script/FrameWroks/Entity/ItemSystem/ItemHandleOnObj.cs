using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandleOnObj : MonoBehaviour{

    public int itemId_InDataBase;

    [SerializeField]
    private Item item;

    [SerializeField]
    public bool isInit = false;

    WeiClickManager clickManager;

    public void Start()
    {
        clickManager = new WeiClickManager();
    }

    public Item GetItem()
    {
        if (item != null)
        {
            return item;
        }
        else
        {
            return null;
        }

    }

    //Picke Upable Object. When we double click on the object in the scene.
    private void OnMouseDown()
    {
        if (!clickManager.DoubleClick()) { return; }

        if (!isInit)
        {
            FetchItemInfo();

            //Find Player and Players inventory
            LPlayer lplayer = FindObjectOfType<LPlayer>();
            LEInventory inventory = lplayer.transform.GetComponentInChildren<LEInventory>();
            inventory.AddItem(item, transform.parent);

            transform.parent.gameObject.SetActive(false);
        }
    }

    /// <summary>
    ///     Onece the Item already asigned, it should not Fetch agin.
    /// When we reload the game Object..... We should Just apply the item value from 
    /// Player Inventory, Instead of Fetch the data from data base.
    /// </summary>
    void FetchItemInfo()
    {
        isInit = true;
        item = GameDataManager.Instance.itemDatabase.getNewInstanceByID(itemId_InDataBase);
        
        /*
            item.itemAttributes = new List<ItemAttribute>();
            item.itemAttributes.Add(........)
            resign new extra Attribute for our item.
        */

        //Add GUI Double Click callback to 
        ItemOnGUIDoubleClickable iGUIDoubleClickable = GetComponentInParent<ItemOnGUIDoubleClickable>();
        if (iGUIDoubleClickable != null)
        {
            //item.OnGUIDoubleClick will be call when we double click the GUI on inventory
            item.OnGUIDoubleClick = iGUIDoubleClickable.ItemOnGUIDoubleClick;
        }
    }

    public void ReloadItemInfoFromSave()
    {
        isInit = true;
        //........
    }
}
