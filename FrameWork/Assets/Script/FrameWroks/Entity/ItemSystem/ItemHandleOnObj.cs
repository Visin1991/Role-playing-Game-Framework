using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHandleOnObj : MonoBehaviour{

    public int itemId_InDataBase;

    [SerializeField]
    private Item item;

    WeiClickManager clickManager;

    public void Start()
    {
        item = GameDataManager.Instance.itemDatabase.getItemCopyByID(itemId_InDataBase);
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

    public void AddAttribute(string attributeName,int value)
    {

    }

    private void OnMouseDown()
    {
        if (clickManager.DoubleClick())
        {
            Debug.Log("Double Click");
        }
    }
}
