using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInputActable {
    void Init(InputActionManager manager);
    void GetKey_A();
    void GetKey_A_Down();
    void GetKey_A_Up();
    void GetKey_B_Down();
    void ShutDown();
    void SetIInputActableItemStatu(LEUnitAnimatorPr.AnimationAttackStatue s);
    void SetUpLayer(int layer);
    void DisableCollision();
    void EnableCollision();
}
