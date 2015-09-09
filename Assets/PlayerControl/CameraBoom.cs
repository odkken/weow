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

        void Start()
        {
            Camera = GetComponent<Camera>();
            _boomOrigin = transform.parent.transform;
            SetBoomToEuler(2 * Vector3.back + Vector3.up);
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
            if (Camera != null && Camera.transform != null)
                Camera.transform.position = _boomOrigin.position + (Length * (Camera.transform.position - _boomOrigin.position).normalized);
        }
        public Camera Camera { get; private set; }

        public void RotateHorizontal(float angle)
        {
            Camera.transform.RotateAround(_boomOrigin.position, Vector3.up, angle);
            Camera.transform.LookAt(_boomOrigin, Vector3.up);
        }

        public void RotateVertical(float angle)
        {
            Camera.transform.RotateAround(_boomOrigin.position, Camera.transform.right, angle);
            Camera.transform.LookAt(_boomOrigin, Vector3.up);
        }
    }
}
