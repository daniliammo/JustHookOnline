using System;
using Mirror;
using UnityEngine;

namespace Cars
{
    public class PlaceInTheVehicle : NetworkBehaviour
    {
        
        [SyncVar]
        public bool isEmployed;

        [SyncVar]
        public Player.Player owner;

        public Vector3 exitPlace;


        #if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(transform.position, 0.2f);
            Gizmos.color = Color.cyan;
            Gizmos.DrawRay(transform.position, transform.forward);
            
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position + exitPlace, 0.3f);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position + exitPlace, transform.forward);
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(transform.position, 0.3f);
            
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position + exitPlace, 0.4f);
        }
        #endif
        
    }
}
