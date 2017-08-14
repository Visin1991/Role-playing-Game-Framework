using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHandle : MonoBehaviour {

    public GameObject itemImageObj;
    public Image itemImage;

    public Item item;

    [SerializeField]
    bool isEmpty = true;

    public void Start()
    {
        itemImage = GetComponentInChildren<Image>();
        itemImageObj = itemImage.gameObject;
        if (isEmpty)
        {
            itemImageObj.SetActive(false);
        }
    }

    public bool IsEmpty
    {
        get { return isEmpty; }
    }

    public void ResetItem(Item _item)
    {
        item = _item;
        if (item != null)
        {
            isEmpty = false;
            if(itemImageObj!=null)
                itemImageObj.SetActive(true);
            if(itemImage!= null)
                itemImage.sprite = item.itemIcon;
        }
    }

    public void Clean()
    {
        item = null;
        if(itemImageObj)
            itemImageObj.SetActive(false);
        isEmpty = true;
    }

}
