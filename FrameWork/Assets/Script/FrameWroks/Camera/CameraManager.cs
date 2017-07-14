using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visin1_1
{
    public class CameraManager : MonoBehaviour
    {
        public CameraType cameraType = CameraType.FirstPerson;

        PlayerCamera currentCamera;

        // Use this for initialization
        void Start()
        {
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
    }
}
