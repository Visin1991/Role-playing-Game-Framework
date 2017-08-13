using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword1 : Weapon {

    public void SetUpLayer(int layer)
    {
        GetComponentInChildren<WeaponPhysics>().SetUpLayer(layer);
    }
}
