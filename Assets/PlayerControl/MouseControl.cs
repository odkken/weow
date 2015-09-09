using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.PlayerControl
{
    class MouseControl : MonoBehaviour
    {
        private CameraBoom _boom;
        void Start()
        {
            _boom = GetComponent<CameraBoom>();
            _boom.Initialize(Camera.main, transform);
            _boom.SetBoomToEuler(-transform.forward + Vector3.up * .5f);
        }


        void Update()
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                _boom.RotateHorizontal(Input.GetAxis("Mouse X"));
                _boom.RotateVertical(-Input.GetAxis("Mouse Y"));
            }
            if (Input.GetMouseButton(1))
            {
                transform.rotation = Quaternion.LookRotation(_boom.Get2dForward(), Vector3.up);
            }
        }

    }
}
