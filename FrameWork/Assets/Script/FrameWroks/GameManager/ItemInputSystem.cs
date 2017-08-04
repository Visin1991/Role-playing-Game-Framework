using UnityEngine;

public abstract class ItemInputSystem : MonoBehaviour {
    public abstract void GetKey_A();
    public abstract void GetKey_A_Up();
    public abstract void GetKey_B_Down();
    public abstract void ShutDown();
}
