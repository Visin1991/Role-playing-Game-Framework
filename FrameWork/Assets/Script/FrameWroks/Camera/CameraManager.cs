using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Put this Script on the Camera
/// Put the Camera as a child of the Player root
/// </summary>
namespace Visin1_1
{
    public class CameraManager : MonoBehaviour
    {
        public LEUnitCentralPanel cp;

        public CameraType cameraType = CameraType.FirstPerson;

        PlayerCamera currentCamera;

        // Use this for initialization
        void Start()
        {

            cp = transform.root.GetComponent<LEUnitCentralPanel>();

            cp.Bind_lE_CameraManager_Event_MailBox(MailBox_LE_CameraManager_Event);

            if (cameraType == CameraType.FirstPerson)
            {
                currentCamera = GetComponent<FirstPersonCamera>();
            }
            else if (cameraType == CameraType.ThridPerson)
            {
                currentCamera = GetComponent<ThridPersonCamera>();
            }
            currentCamera.InitialCamera();
        }

        // Update is called once per frame
        void Update()
        {
            currentCamera.UpdateCamera();
        }

        private void LateUpdate()
        {
            currentCamera.LateUpdateCamera();
        }

        public void MailBox_LE_CameraManager_Event(LE_Camera_Event e)
        {
            if (e.Type == LE_Camera_EventType.UpdateValue)
            {
                currentCamera.SetCameraDetal((LE_Camera_Event_UpdateVlaue)e);
            }
        }
    }
}
