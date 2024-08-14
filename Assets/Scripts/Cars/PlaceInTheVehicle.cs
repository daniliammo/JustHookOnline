using Mirror;

namespace Cars
{
    public class PlaceInTheVehicle : NetworkBehaviour
    {
        
        [SyncVar]
        public bool isEmployed;

        [SyncVar]
        public Player.Player owner;

    }
}
