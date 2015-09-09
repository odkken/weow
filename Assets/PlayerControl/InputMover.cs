﻿using UnityEngine;

namespace Assets
{
    public class InputMover : MonoBehaviour
    {
        public float Speed = 5;
        

        private Vector3 GetInputVector()
        {
            return Quaternion.FromToRotation(Vector3.forward, transform.forward) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        // Update is called once per frame
        void Update()
        {
            transform.position += GetInputVector().normalized * Speed * Time.deltaTime;
        }
    }
}
