using System;
using _Game.Scripts.Gameplay.Characters;
using Animancer;
using Assets._Game.Scripts.Gameplay.Characters;
using Assets._Game.Scripts.Gameplay.Powerups;
using TiltFive;
using UnityEngine;
using UnityEngine.InputSystem;

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

        [SerializeField] private AnimancerComponent animancer;
        // [SerializeField] private Rigidbody rb;
        public Transform playerCamera;
        [SerializeField] private GameObject currentPlayerModel;
        private Vector3 movementDirection;
        public PlayerGrip playerGrip;


        [SerializeField] WeaponShoot weaponCrossBow, weaponCannonBow, weaponBomb;

        private WeaponShoot currentWeapon;

        // [SerializeField] private float movementSpeed;

        public PlayerAnimations plrAnimations;
        public PlayerHealth playerHealth;

        private void Start()
        {
            currentWeapon = weaponCrossBow;

            _forward = Vector3.forward;
            _right = Vector3.right;
            
            if (playerCamera == null)
            {
                playerCamera = Camera.main.transform;
            }
        }

        public void WeaponPickup(WeaponType weaponType)
        {
            if (currentWeapon.ammoCurrent > 0)
            {
                currentWeapon.ShootThrowWeapon();
            }

            switch (weaponType)
            {
                case WeaponType.crossbow:
                    currentWeapon = weaponCrossBow;
                    break;
                case WeaponType.cannonBow:
                    currentWeapon = weaponCannonBow;
                    break;
                case WeaponType.bomb:
                    currentWeapon = weaponBomb;
                    break;
            }
            currentWeapon.gameObject.SetActive(true);
            currentWeapon.WeaponEnable();
        }

        private void Jump()
        {
            if (groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }
        }

        public void OnJump(InputAction value)
        {
            var jump = value.ReadValue<bool>();
            
            Debug.Log("jump " + value.ReadValue<bool>());
            
            if (groundedPlayer && jump)
            {
                Jump();
            }
        }
        
        public void OnJump(InputAction.CallbackContext value)
        {
            var jump = value.ReadValueAsButton();
            
            Debug.Log("jump " + jump);
            
            if (jump)
            {
                Jump();
            }
        }

        public void OnControlsChanged(PlayerInput player)
        {
            Debug.Log("Player  " + player.name + " has joined the party.");
        }

        public void Grip()
        {
            playerGrip.OnGripping();
        }

        public void GripRelease()
        {
            playerGrip.OnGrippingRelease();
        }

        public void MenuButton()
        {
            StaticReferences.Instance.RestartScene();
        }

        private Vector3 _forward;
        private Vector3 _right;
        private Vector3 _normalizedMove;
        
        void Update()
        {
            groundedPlayer = controller.isGrounded;
            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            //When placing a player directly for debugging this prevents null ref
            if (playerCamera != null)
            {
                _forward = playerCamera.forward;
                _forward.y = 0;
                _forward.Normalize();

                _right = playerCamera.right;
                _right.y = 0;
                _right.Normalize();
            };


            // Vector3 move = Vector3.zero;
            // if (StaticReferences.Instance.isKeyboard)
            // {
            //     move = new Vector3(UnityEngine.Input.GetAxis("Horizontal"), 0, UnityEngine.Input.GetAxis("Vertical"));
            // }
            // else
            // {
            //     move = movementDirection;
            // }

            if (movementDirection != Vector3.zero)
            {
                _normalizedMove = _right * movementDirection.x + _forward * movementDirection.z;

                // gameObject.transform.forward = move + forward;
                gameObject.transform.forward = _normalizedMove;
                playerVelocity.x = _normalizedMove.x * playerSpeed;
                playerVelocity.z = _normalizedMove.z * playerSpeed;
                animancer.Play(groundedPlayer ? plrAnimations.AnimWalk : plrAnimations.AnimJump, 0.2f);
            }
            else
            {
                animancer.Play(groundedPlayer ? plrAnimations.AnimIdle : plrAnimations.AnimJump, 0.2f);
            }

            // // Changes the height position of the player..
            // if (groundedPlayer && UnityEngine.Input.GetButtonDown("Jump"))
            // {
            //     Jump();
            // }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
            movementDirection = Vector3.zero;
            playerVelocity.x = 0;
            playerVelocity.z = 0;
        }

        // public void OnMove(Vector2 direction)
        // {
        //     
        //     movementDirection = new Vector3(direction.x, 0, direction.y);
        // }
        
        private Vector2 _direction;
        public void OnMove(InputValue value)
        {
            _direction = value.Get<Vector2>();
            Debug.Log("move " + _direction);
            movementDirection = new Vector3(_direction.x, 0, _direction.y);
        }

        public void OnShoot(InputValue value)
        {
            float shootValue = value.Get<float>();
            Debug.Log("Shoot value " + shootValue);
            if (shootValue < 0.2f)
            {
                currentWeapon.FireWeapon();
            }

            if (shootValue < 0.1f)
            {
                GripRelease();
            } else if (shootValue > 0.9f)
            {
                Grip();
            }
            
        }
        
        public void OnJump(InputValue value)
        {
            bool shouldJump = value.Get<float>() > 0.5f;
            Debug.Log("Jumo value " + value.Get<float>());
            if (shouldJump)
            {
                Jump();
            }
        }

        public void RespawnPlayerWithPlayerModel(GameObject model, Vector3 respawnPos)
        {
            var oldModel = currentPlayerModel;
            var newModel = Instantiate(model, currentPlayerModel.transform.position, currentPlayerModel.transform.rotation);
            currentPlayerModel.gameObject.SetActive(false);


            currentPlayerModel = newModel;
            currentPlayerModel.transform.parent = transform;
            currentPlayerModel.gameObject.SetActive(true);

            animancer.Animator = newModel.GetComponent<Animator>();
            plrAnimations = newModel.GetComponent<PlayerAnimations>();

            transform.position = respawnPos;

            playerHealth.PlayerRespawned();

            Destroy(oldModel);
        }
        
        public void RespawnPlayer(Vector3 respawnPos, Quaternion respawnRot)
        {
            transform.position = respawnPos;
            transform.rotation = respawnRot;
            playerHealth.PlayerRespawned();
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