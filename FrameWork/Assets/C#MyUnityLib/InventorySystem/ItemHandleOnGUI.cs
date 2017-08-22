using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemHandleOnGUI : MonoBehaviour {

    GameObject itemImageObj;
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

    /// <summary>
    /// OnGUIDoubleClick will be assigned when we add the ItemGUI to the Inventory
    /// </summary>
    public void DoubleClick()
    {
        if (item.OnGUIDoubleClick != null) { item.OnGUIDoubleClick.Invoke(); }
        //LEUnitProcessor processor = FindObjectOfType<LPlayer>().GetComponent<LEUnitProcessor>();
        //IInputActable sword1 = processor.InstantiateSword1();
        //swoed1.SetProporty.......
        //
        //processor.EquipWeapon(sword1);
    }

}
