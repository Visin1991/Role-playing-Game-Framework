using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerCamera{
    CameraType CameraType { get; }
    Transform Transform { get; }

    void InitialCamera();
    void UpdateCamera();
    void LateUpdateCamera();
}
public enum CameraType { FirstPerson, ThridPerson, God }
