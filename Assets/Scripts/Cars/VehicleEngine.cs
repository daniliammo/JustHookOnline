using Mirror;

namespace Cars
{
    public class VehicleEngine : NetworkBehaviour
    {

        [SyncVar]
        public bool isBroken;
        
        [SyncVar]
        public int temperature;


        [Command (requiresAuthority = false)]
        public void StartEngine()
        {
            
        }

        [Command (requiresAuthority = false)]
        public void StopEngine()
        {
            
        }
        
    }
}
