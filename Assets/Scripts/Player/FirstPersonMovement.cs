using Mirror;
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
        
        
        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _player = GetComponent<Player>();
            _hookController = GetComponent<Hook>();
            
            _ui = FindObjectOfType<UIObjectsLinks>();
            _joystick = FindObjectOfType<Joystick>();
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Ladder")) return;
            
            _isOnLadder = true;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.useGravity = false;
        }
    
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Ladder")) return;
            
            _isOnLadder = false;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.useGravity = true;
        }
            
        private void Update()
        {
            if (!isOwned) return;
            if (_player.isDeath) return;
            if (_ui.menu.isPaused) return;
            if (_hookController.IsHooking) return;
    
            if (_isOnLadder)
            {
                // Подъем вверх и спуск вниз по оси Y
                _targetVelocity = RealInput.IsTouchSupported switch
                {
                    true => new Vector2(_joystick.Horizontal * Speed, _joystick.Vertical * Speed),
                    false => new Vector2(Input.GetAxis("Horizontal") * Speed, Input.GetAxis("Vertical") * Speed)
                };
    
                _rigidbody.velocity = new Vector3(_rigidbody.velocity.x, _targetVelocity.y, _rigidbody.velocity.z);
                return;
            }

            // Движение по горизонтали и вертикали
            _targetVelocity = RealInput.IsTouchSupported switch
            {
                true => new Vector2(_joystick.Horizontal * Speed, _joystick.Vertical * Speed),
                false => new Vector2(Input.GetAxis("Horizontal") * Speed, Input.GetAxis("Vertical") * Speed)
            };
            
            _rigidbody.velocity = transform.rotation * new Vector3(_targetVelocity.x, _rigidbody.velocity.y, _targetVelocity.y);
        }
        
    }
}
