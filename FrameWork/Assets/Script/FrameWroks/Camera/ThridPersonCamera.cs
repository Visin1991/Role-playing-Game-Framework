using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Visin1_1
{
    public class ThridPersonCamera : MonoBehaviour, PlayerCamera
    {
        CameraType cameraType = CameraType.ThridPerson;
        public CameraType CameraType { get { return cameraType; } }
        public Transform Transform { get { return transform; } }

        public CameraRotateModel cameraRotateModel = CameraRotateModel.Free;

        public KeyCode controlKey;

        [SerializeField] private Vector2 pitchMinMax = new Vector2(0, 85);
        [SerializeField] private float rotationSmoothTime = 0.5f;
        private Vector3 rotationSmoothVelocity;
        private Vector3 currentRotation;

        [SerializeField] private Transform target;
        [SerializeField] private Vector2 rangeToTarget = new Vector2(2, 10);
        [SerializeField] private float cameraMoveSensitivity = 10;
        private float dstToTarget = 10;

        private float yaw;  //Rotation around Y Axis
        private float pitch = 75;//Rotation around X Axis

        public bool XboxPad;
        

        public void InitialCamera()
        {

        }

        public void UpdateCamera()
        {

        }

        public void LateUpdateCamera()
        {
#if UNITY_IOS || UNITY_ANDROID
            Vector2 yawPitch = TouchLib.GetSwipe2D() * cameraMoveSensitivity;
            yaw += yawPitch.x;
            pitch -= yawPitch.y;
            pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
            dstToTarget += TouchLib.GetDeltaMagnitudeDifferent();
            dstToTarget = Mathf.Clamp(dstToTarget, rangeToTarget.x, rangeToTarget.y);
#elif UNITY_STANDALONE
            if (XboxPad)
            {
                yaw += Input.GetAxis("RXAxis") * cameraMoveSensitivity;
                pitch -= Input.GetAxis("RYAxis") * cameraMoveSensitivity;
                pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
                dstToTarget += Input.GetAxis("Trigger");
                dstToTarget = Mathf.Clamp(dstToTarget, rangeToTarget.x, rangeToTarget.y);
            }
            else
            {

                if (cameraRotateModel == CameraRotateModel.Free)
                {
                    yaw += Input.GetAxis("Mouse X") * cameraMoveSensitivity;
                    pitch -= Input.GetAxis("Mouse Y") * cameraMoveSensitivity;

                    pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
                    dstToTarget = Mathf.Clamp(dstToTarget, rangeToTarget.x, rangeToTarget.y);
                }
                else if (cameraRotateModel == CameraRotateModel.KeyBoardRestrict)
                {
                    if (Input.GetKey(controlKey))
                    {
                        yaw += Input.GetAxis("Mouse X") * cameraMoveSensitivity;
                        pitch -= Input.GetAxis("Mouse Y") * cameraMoveSensitivity;
                    }

                    dstToTarget += Input.GetAxis("Mouse ScrollWheel");
                    pitch = Mathf.Clamp(pitch, pitchMinMax.x, pitchMinMax.y);
                    dstToTarget = Mathf.Clamp(dstToTarget, rangeToTarget.x, rangeToTarget.y);
                }                
            }
#endif

            currentRotation = Vector3.SmoothDamp(currentRotation, new Vector3(pitch, yaw,0), ref rotationSmoothVelocity, rotationSmoothTime);
            transform.eulerAngles = currentRotation;
            transform.position = target.position - transform.forward * dstToTarget;
        }

        public float Yaw
        {
            get { return yaw; }
        }

        public float Pitch
        {
            get { return pitch; }
        }
    }
}
