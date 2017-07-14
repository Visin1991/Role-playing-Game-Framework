using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour, ItemInputSystem {

    public Transform GunHolder;
    public Gun[] allGuns;
    Gun currentGun;

    private void Start()
    {
        EquipGun(allGuns[2]);
    }

    public void EquipGunIndex(int index)
    {
        EquipGun(allGuns[index]);
    }

    void EquipGun(Gun gun)
    {
        if (currentGun != null)
        {
            Destroy(currentGun.gameObject);
        }
        currentGun = Instantiate(gun, GunHolder.position, GunHolder.rotation) as Gun;
        currentGun.transform.SetParent(GunHolder);
    }

    public void GetKey_A()
    {
        OnTriggerHold();
    }

    public void GetKey_A_Up()
    {
        OnTriggerRelease();
    }

    public void GetKey_B_Down()
    {
        ReLoad();
    }

    void OnTriggerHold()
    {
        if (currentGun != null)
        {
            currentGun.OnTriggerHold();
        }
        else {
            Debug.LogError("Current Gun is Null");
        }
    }

    void OnTriggerRelease()
    {
        if (currentGun != null)
        {
            currentGun.OnTriggerRelease();
        }
    }

    void ReLoad()
    {
        if (currentGun != null)
        {
            currentGun.Reload();
        }
    }

}
