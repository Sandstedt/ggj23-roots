// Animancer // https://kybernetik.com.au/animancer // Copyright 2018-2023 Kybernetik //

#pragma warning disable CS0649 // Field is never assigned to, and will always have its default value.

using Animancer.FSM;
using Animancer.Units;
using System;
using UnityEngine;

namespace Animancer.Examples.StateMachines
{
    /// <summary>Uses player input to control a <see cref="Character"/>.</summary>
    /// <example><see href="https://kybernetik.com.au/animancer/docs/examples/fsm/weapons">Weapons</see></example>
    /// https://kybernetik.com.au/animancer/api/Animancer.Examples.StateMachines/WeaponsCharacterBrain
    /// 
    [AddComponentMenu(Strings.ExamplesMenuPrefix + "Weapons - Weapons Character Brain")]
    [HelpURL(Strings.DocsURLs.ExampleAPIDocumentation + nameof(StateMachines) + "/" + nameof(WeaponsCharacterBrain))]
    public sealed class WeaponsCharacterBrain : MonoBehaviour
    {
        /************************************************************************************************************************/

        [SerializeField] private bool keyboard;
        
        [SerializeField] private Character _Character;
        [SerializeField] private CharacterState _Move;
        [SerializeField] private CharacterState _Attack;
        [SerializeField, Seconds] private float _InputTimeOut = 0.5f;
        [SerializeField] private EquipState _Equip;
        [SerializeField] private Weapon[] _Weapons;
        [SerializeField] private Camera playerCamera;
        

        private StateMachine<CharacterState>.InputBuffer _InputBuffer;

        /************************************************************************************************************************/

        private void Awake()
        {
            _InputBuffer = new StateMachine<CharacterState>.InputBuffer(_Character.StateMachine);
        }
        

        /************************************************************************************************************************/

        private void Update()
        {
            if (keyboard)
            {
                UpdateMovement();
            }
            else
            {
                T5UpdateMovement();
            }
            UpdateEquip();
            UpdateAction();

            _InputBuffer.Update();
        }

        /************************************************************************************************************************/
        
        [SerializeField] private Vector3 movementDirection;


        public void OnStickMoved(Vector2 direction)
        {
            Debug.Log("OnStickMoved x: " + direction.x + " y: " + direction.y);
            movementDirection = direction;
        }

        private void T5UpdateMovement()
        {
               
            var input = movementDirection;
            if (input != default)
            {
                // Get the camera's forward and right vectors and flatten them onto the XZ plane.
                var camera = playerCamera.transform;

                var forward = camera.forward;
                forward.y = 0;
                forward.Normalize();

                var right = camera.right;
                right.y = 0;
                right.Normalize();

                // Build the movement vector by multiplying the input by those axes.
                _Character.Parameters.MovementDirection =
                    right * input.x +
                    forward * input.y;

                // Enter the locomotion state if we aren't already in it.
                _Character.StateMachine.TrySetState(_Move);
                movementDirection = Vector3.zero;
            }
            else
            {
                _Character.Parameters.MovementDirection = default;
                _Character.StateMachine.TrySetDefaultState();
            }

            // Indicate whether the character wants to run or not.
            // _Character.Parameters.WantsToRun = ExampleInput.LeftShiftHold;
        }

        private void UpdateMovement()// This method is identical to the one in MovingCharacterBrain.
        {
            var input = ExampleInput.WASD;
            if (input != default)
            {
                // Get the camera's forward and right vectors and flatten them onto the XZ plane.
                var camera = playerCamera.transform;

                var forward = camera.forward;
                forward.y = 0;
                forward.Normalize();

                var right = camera.right;
                right.y = 0;
                right.Normalize();

                // Build the movement vector by multiplying the input by those axes.
                _Character.Parameters.MovementDirection =
                   right * input.x +
                   forward * input.y;

                // Enter the locomotion state if we aren't already in it.
                _Character.StateMachine.TrySetState(_Move);
            }
            else
            {
                _Character.Parameters.MovementDirection = default;
                _Character.StateMachine.TrySetDefaultState();
            }

            // Indicate whether the character wants to run or not.
            _Character.Parameters.WantsToRun = ExampleInput.LeftShiftHold;
        }

        /************************************************************************************************************************/
        public void Weapon1()
        {
            Debug.Log("Weapon1");
            _Equip.NextWeapon = _Weapons[1];
            _InputBuffer.Buffer(_Equip, _InputTimeOut);
            _InputBuffer.Buffer(_Attack, _InputTimeOut);
        }
        
        public void Weapon2()
        {
            Debug.Log("Weapon2");
            _Equip.NextWeapon = _Weapons[2];
            _InputBuffer.Buffer(_Equip, _InputTimeOut);
            _InputBuffer.Buffer(_Attack, _InputTimeOut);
        }
        
        private void UpdateEquip()
        {
            if (ExampleInput.RightMouseDown)
            {
                var equippedWeaponIndex = Array.IndexOf(_Weapons, _Character.Equipment.Weapon);

                equippedWeaponIndex++;
                if (equippedWeaponIndex >= _Weapons.Length)
                    equippedWeaponIndex = 0;

                _Equip.NextWeapon = _Weapons[equippedWeaponIndex];
                _InputBuffer.Buffer(_Equip, _InputTimeOut);
            }
        }

        /************************************************************************************************************************/

        private void UpdateAction()
        {
            if (ExampleInput.LeftMouseDown)
            {
                _InputBuffer.Buffer(_Attack, _InputTimeOut);
            }
        }

        /************************************************************************************************************************/
    }
}
