using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class MouseOrbitWithTransparency : MonoBehaviour
    {
        private Transform cameraTarget;
        public float distance = 10;
        public float xSpeed = 250;
        public float ySpeed = 120;
        public float yMinLimit = -10;
        public float yMaxLimit = 50;

        private float x = 0;
        private float y = 0;

        private Transform lastCollidedObjectByCamera;
        private CameraObstructionWatcher cameraObstructionWatcher;

        void Start()
        {
            var angles = transform.eulerAngles;
            x = angles.y;
            y = angles.x;
            cameraTarget = GameObject.FindGameObjectWithTag("CameraTarget").GetComponent<Transform>();
            cameraObstructionWatcher = new CameraObstructionWatcher();
        }

        void LateUpdate()
        {
            if (cameraTarget)
            {
               // lastCollidedObjectByCamera.GetComponent<Renderer>().material.


                x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

                y = ClampAngle(y, yMinLimit, yMaxLimit);

                var rotation = Quaternion.Euler(y, x, 0);
                var position = rotation * new Vector3(0, 0, -distance) + cameraTarget.position;

                transform.rotation = rotation;
                transform.position = position;
            }
        }

        private float ClampAngle(float angle, float yMinLimit, float yMaxLimit)
        {
            if (angle < -360)
                angle += 360;
            if (angle > 360)
                angle -= 360;
            return Mathf.Clamp(angle, yMinLimit, yMaxLimit);
        }
    }
}
