using Animancer;
using Assets._Game.Scripts.Gameplay.Characters;
using UnityEngine;

namespace _Game.Scripts
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private CharacterController controller;
        [SerializeField] private Vector3 playerVelocity;
        [SerializeField] private bool groundedPlayer;
        [SerializeField] private float playerSpeed = 2.0f;
        [SerializeField] private float jumpHeight = 1.0f;
        [SerializeField] private float gravityValue = -9.81f;
        [SerializeField] private bool keyboard;

        [SerializeField] private AnimancerComponent animancer;
        // [SerializeField] private Rigidbody rb;
        [SerializeField] private Camera playerCamera;
        [SerializeField] private GameObject currentPlayerModel;
        private Vector3 movementDirection;

        // [SerializeField] private float movementSpeed;

        public PlayerAnimations plrAnimations;


        void Update()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }
            
            var _camera = playerCamera.transform;
            
            var forward = _camera.forward;
            forward.y = 0;
            forward.Normalize();
            
            var right = _camera.right;
            right.y = 0;
            right.Normalize();

            Vector3 move = Vector3.zero;
            if (keyboard)
            {
                move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            }
            else
            {
                move = movementDirection;
            }

            controller.Move(move * (Time.deltaTime * playerSpeed));

            if (move != Vector3.zero)
            {
                var normalizedMove = right * move.x + forward * move.z;
                
                // gameObject.transform.forward = move + forward;
                gameObject.transform.forward = normalizedMove;
                playerVelocity.x = normalizedMove.x * playerSpeed;
                playerVelocity.z = normalizedMove.z * playerSpeed;
                animancer.Play(plrAnimations.AnimWalk, 0.2f);
            }
            else
            {
                animancer.Play(plrAnimations.AnimIdle, 0.2f);
            }

            // Changes the height position of the player..
            if (Input.GetButtonDown("Jump") && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
            movementDirection = Vector3.zero;
            playerVelocity.x = 0;
            playerVelocity.z = 0;
        }


        // private void Update()
        // {
        //     if (movementDirection != Vector2.zero)
        //     {
        //         Move();
        //     }
        //
        //     if (keyboard && Input.GetButtonDown("Horizontal"))
        //     {
        //         movementDirection = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        //     }
        //     if (keyboard && Input.GetButtonDown("Vertical"))
        //     {
        //         movementDirection = new Vector2(0, Input.GetAxisRaw("Vertical"));
        //     }
        // }

        public void OnMove(Vector2 direction)
        {
            movementDirection = new Vector3(direction.x, 0, direction.y);
        }

        // public void Move()
        // {
        //     // Get the camera's forward and right vectors and flatten them onto the XZ plane.
        //     var _camera = playerCamera.transform;
        //
        //     var forward = _camera.forward;
        //     forward.y = 0;
        //     forward.Normalize();
        //
        //     var right = _camera.right;
        //     right.y = 0;
        //     right.Normalize();
        //
        //     // rb.transform.rotation = Quaternion.LookRotation(forward);
        //     // Debug.Log(movementDirection);
        //     // rb.AddForce(movementDirection * (movementSpeed * Time.deltaTime));
        //
        //     // animancer.Play(animWalk, 0.2f);
        //     movementDirection = Vector3.zero;
        // }

    }
}