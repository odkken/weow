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

        // Update is called once per frame
        void Update()
        {
            isOnGround = Physics.Raycast(new Ray(transform.position, Vector3.down), 1.01f);

            var inputVel = GetInputVector().normalized * (isOnGround ? Accel : AirAccel);
            //if (isOnGround && inputVel.sqrMagnitude > .1f)
            rigidBody.AddForce(inputVel);

            var translational = new Vector3(rigidBody.velocity.x, 0, rigidBody.velocity.z);
            if (translational.magnitude > Speed)
                translational = translational.normalized * Speed;
            rigidBody.velocity = new Vector3(translational.x, rigidBody.velocity.y, translational.z);

            if (isOnGround && Input.GetKeyDown(KeyCode.Space))
                Jump();
        }
    }
}
