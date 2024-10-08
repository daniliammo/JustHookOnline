using Mirror;
using UnityEngine;

namespace Interactable
{
    [RequireComponent(typeof(BoxCollider), typeof(NetworkIdentity))]
    public class Interactable : NetworkBehaviour
    {
        
        public string interactName;
        
        [HideInInspector]
        public InteractType interactType;
        
        [HideInInspector]
        public VehicleInteract vehicleInteract;
        [HideInInspector]
        public Intercom intercom;


        private void Start()
        {
            switch (interactType)
            {
                case InteractType.PasswordEntry:
                    intercom = GetComponent<Intercom>();
                    return;
                case InteractType.VehicleInteract:
                    vehicleInteract = GetComponent<VehicleInteract>();
                    break;
            }
        }

        [Command (requiresAuthority = false)]
        public virtual void Interact()
        {
            
        }

        [Command (requiresAuthority = false)]
        public virtual void Interact(Player.Player player)
        {
            
        }
        
    }
}
