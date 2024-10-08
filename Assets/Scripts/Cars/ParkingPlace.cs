using UnityEngine;

namespace Cars
{
    public class ParkingPlace : MonoBehaviour
    {
        
        public bool isEmployed;
        
        
        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(255, 0, 0, 1);
            
            Gizmos.DrawRay(transform.position, transform.forward * 1.7f);
            
            Gizmos.color = new Color(0, 255, 0, 1);
            
            Gizmos.DrawRay(transform.position, transform.forward * -1.7f);
            Gizmos.DrawRay(transform.position, transform.right);
            Gizmos.DrawRay(transform.position, -transform.right);
            Gizmos.DrawRay(transform.position, transform.up * 0.7f);
            Gizmos.DrawRay(transform.position, transform.up * -0.7f);
            
            Gizmos.color = new Color(0, 255, 0, 1);
            
            Gizmos.DrawSphere(transform.position, 0.25f);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0, 0, 255, 1);
            Gizmos.DrawSphere(transform.position, 0.5f);
        }
        
    }
}
