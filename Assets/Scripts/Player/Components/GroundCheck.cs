using Mirror;
using UnityEngine;

namespace Player.Components
{
    public class GroundCheck : NetworkBehaviour
    {
        
        public float distanceThreshold;
       
        public bool isGrounded = true;
        public event System.Action Grounded;

        public float originOffset = .001f;
        
        private Vector3 RaycastOrigin => transform.position + Vector3.up * originOffset;
        private float RaycastDistance => distanceThreshold + originOffset;
        
        
        private void LateUpdate()
        {
            // Check if we are grounded now.
            var isGroundedNow = Physics.Raycast(RaycastOrigin, Vector3.down, distanceThreshold * 2);

            // Call event if we were in the air and we are now touching the ground.
            if (isGroundedNow && !isGrounded)
                Grounded?.Invoke();

            // Update isGrounded.
            isGrounded = isGroundedNow;
        }
        
        private void OnDrawGizmosSelected()
        {
            // Draw a line in the Editor to show whether we are touching the ground.
            Debug.DrawLine(RaycastOrigin, RaycastOrigin + Vector3.down * RaycastDistance, isGrounded ? Color.white : Color.red);
        }
    
    }
    
}
