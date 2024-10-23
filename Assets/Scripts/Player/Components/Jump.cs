using Mirror;
using UnityEngine;

namespace Player.Components
{
    public class Jump : NetworkBehaviour
    {

        private Player _player;
        private Hook _hookController;
        
        private Rigidbody _rigidbody;
        private const float JumpStrength = 8;
        public event System.Action Jumped;
        
        private GroundCheck _groundCheck;
        

        private void Start()
        {
            _player = GetComponent<Player>();
            _rigidbody = GetComponent<Rigidbody>();
            _hookController = GetComponent<Hook>();
            _groundCheck = GetComponent<GroundCheck>();
        }

        private void LateUpdate()
        {
            if (!isOwned) return;
            if(_player.IsDeath) return;
            if(_hookController.IsHooking) return;
            
            if (Input.GetButtonDown("Jump"))
                AddForce();
        }

        public void AddForce()
        {
            if ((_groundCheck && !_groundCheck.isGrounded) || _hookController.IsHooking) return;
            _rigidbody.AddForce(Vector3.up * (JumpStrength * 100));
            Jumped?.Invoke();
        }
        
    }
}
