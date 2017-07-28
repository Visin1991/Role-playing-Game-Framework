using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerCamera{
    CameraType CameraType { get; }
    Transform Transform { get; }

    void InitialCamera();
    void UpdateCamera();
    void LateUpdateCamera();
    void SetCameraDetal(LE_Camera_Event_UpdateVlaue e);
}

public enum CameraType { FirstPerson, ThridPerson, God }
public enum CameraRotateModel { Fixed,Free,KeyBoardRestrict}
