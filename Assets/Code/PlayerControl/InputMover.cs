using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets
{
    [RequireComponent(typeof(Rigidbody), typeof(PhysicMaterial))]
    public class InputMover : MonoBehaviour
    {
        public float Accel = 100;
        public float AirAccel = 50;
        public float Speed = 5;
        public float JumpStrength = 5;
        public Transform OrientationTransform;
        private Rigidbody rigidBody;
        void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
        }

        private Vector3 GetInputVector()
        {
            return Quaternion.FromToRotation(Vector3.forward, OrientationTransform.forward) * new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        }

        private void Jump()
        {
            rigidBody.velocity = new Vector3(rigidBody.velocity.x, JumpStrength, rigidBody.velocity.z);
        }

        private bool isOnGround;

        private Dictionary<Collider, List<ContactPoint>> currentOverlappingColliders = new Dictionary<Collider, List<ContactPoint>>();

        void OnCollisionEnter(Collision collision)
        {
            currentOverlappingColliders.Add(collision.collider, collision.contacts.ToList());
        }

        void OnCollisionStay(Collision collision)
        {
            currentOverlappingColliders[collision.collider] = new List<ContactPoint>(collision.contacts);
        }

        void OnCollisionExit(Collision collision)
        {
            currentOverlappingColliders.Remove(collision.collider);
        }


        // Update is called once per frame
        void Update()
        {
            isOnGround = Physics.Raycast(new Ray(transform.position, Vector3.down), 1.01f);

            var inputVel = GetInputVector().normalized * (isOnGround ? Accel : AirAccel);
            //Debug.Log(inputVel);
            foreach (var currentOverlappingCollider in currentOverlappingColliders)
            {
                foreach (var contactPoint in currentOverlappingCollider.Value)
                {
                    var playerDot = Vector3.Dot(contactPoint.point - transform.position, contactPoint.normal);
                    var dot = Vector3.Dot(inputVel, contactPoint.normal);

                    if (playerDot < 0 && dot > 0)
                        continue;

                    inputVel -= contactPoint.normal * (dot);
                }

            }

            if (isOnGround || inputVel.sqrMagnitude > .05)
                rigidBody.velocity = new Vector3(inputVel.x, rigidBody.velocity.y, inputVel.z);

            //var translational = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
            //if (translational.magnitude > Speed)
            //    translational = translational.normalized * Speed;
            //rigidBody.velocity = new Vector3(translational.x, rigidBody.velocity.y, translational.z);

            if (isOnGround && Input.GetKeyDown(KeyCode.Space))
                Jump();
        }
    }
}
