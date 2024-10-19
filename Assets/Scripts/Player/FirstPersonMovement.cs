using Mirror;
using Player.Components;
using UI;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class FirstPersonMovement : NetworkBehaviour
    {
        
        private Vector2 _targetVelocity;
    
        private UIObjectsLinks _ui;

        private Joystick _joystick;
        
        private const byte Speed = 8;
    
        private bool _isOnLadder;
        
        private Rigidbody _rigidbody;

        private Hook _hookController;
        private Player _player;
        private GroundCheck _groundCheck;

        public bool allowToWalkOnAir = false;
        
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _player = GetComponent<Player>();
            _hookController = GetComponent<Hook>();
            _groundCheck = GetComponent<GroundCheck>();
            
            _ui = FindObjectOfType<UIObjectsLinks>();
            _joystick = FindObjectOfType<Joystick>();
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Ladder")) return;
            
            _isOnLadder = true;
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.useGravity = false;
        }
    
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Ladder")) return;
            
            _isOnLadder = false;
            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.useGravity = true;
        }
            
        private void Update()
        {
            if (!isOwned) return;
            if (_player.isDeath) return;
            if (_ui.menu.isPaused) return;
            if (_hookController.IsHooking) return;
            if (!_groundCheck.isGrounded && !allowToWalkOnAir) return;
            
            if (_isOnLadder)
            {
                // Подъем вверх и спуск вниз по оси Y
                _targetVelocity = RealInput.IsTouchSupported switch
                {
                    true => new Vector2(_joystick.Horizontal * Speed, _joystick.Vertical * Speed),
                    false => new Vector2(Input.GetAxis("Horizontal") * Speed, Input.GetAxis("Vertical") * Speed)
                };
    
                _rigidbody.linearVelocity = new Vector3(_rigidbody.linearVelocity.x, _targetVelocity.y, _rigidbody.linearVelocity.z);
                return;
            }

            // Движение по горизонтали и вертикали
            _targetVelocity = RealInput.IsTouchSupported switch
            {
                true => new Vector2(_joystick.Horizontal * Speed, _joystick.Vertical * Speed),
                false => new Vector2(Input.GetAxis("Horizontal") * Speed, Input.GetAxis("Vertical") * Speed)
            };
            
            _rigidbody.linearVelocity = transform.rotation * new Vector3(_targetVelocity.x, _rigidbody.linearVelocity.y, _targetVelocity.y);
        }
        
    }
}
