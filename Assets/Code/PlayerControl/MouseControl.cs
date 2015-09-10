using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.PlayerControl
{
    class MouseControl : MonoBehaviour
    {
        public float MouseSensitivity;
        public Transform HeadTransform;
        public CameraBoom Boom;
        void Start()
        {
            Boom.SetBoomToEuler(-transform.forward + Vector3.up * .5f);
        }
        void Update()
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                Boom.RotateHorizontal(Input.GetAxis("Mouse X") * MouseSensitivity);
                Boom.RotateVertical(-Input.GetAxis("Mouse Y") * MouseSensitivity);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
            if (Input.GetMouseButton(1))
            {
                transform.rotation = Quaternion.LookRotation(Boom.Get2dForward(), Vector3.up);
                HeadTransform.rotation = Boom.Camera.transform.rotation;
            }
        }

    }
}
