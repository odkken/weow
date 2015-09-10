using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.PlayerControl
{
    [RequireComponent(typeof(Camera))]
    class CameraBoom : MonoBehaviour
    {
        private Transform _boomOrigin;

        void Awake()
        {
            Camera = GetComponent<Camera>();
            _boomOrigin = transform.parent.transform;
            SetBoomToEuler(2 * Vector3.back + Vector3.up);
            Camera.transform.LookAt(_boomOrigin, Vector3.up);
        }

        public Vector3 Get2dForward()
        {
            var camForward = Camera.transform.forward;
            return new Vector3(camForward.x, 0, camForward.z);
        }

        public void SetBoomToEuler(Vector3 eulerDirection)
        {
            Camera.transform.position = _boomOrigin.position + (eulerDirection.normalized * Length);
        }

        public float Length;

        void Update()
        {
            //Debug.Log(Camera.transform.rotation.eulerAngles);
        }
        public Camera Camera { get; private set; }

        public void RotateHorizontal(float angle)
        {
            Camera.transform.RotateAround(_boomOrigin.position, Vector3.up, angle);
        }

        public void RotateVertical(float angle)
        {
            var currentHorizontalAngle = Camera.transform.rotation.eulerAngles.x;
            var newAngle = currentHorizontalAngle + angle;
            if (newAngle > 89.5 && newAngle < 120)
                angle -= (newAngle - 90);
            if (newAngle < 270 && newAngle > 230)
            {
                Debug.Log("old angle: " + angle);
                angle -= (newAngle - 270);
                Debug.Log("new angle: " + angle);
            }
            Camera.transform.RotateAround(_boomOrigin.position, Camera.transform.right, angle);
        }
    }
}
