using Cars;
using UnityEngine;

namespace Interactable
{
    public class VehicleInteract : Interactable
    {
        
        [Header("Машина")]
        public Vehicle vehicle;


        public override void Interact(Player.Player player)
        {
            vehicle.CmdTryToSitOnADriverPlace(player);
        }
        
    }
}