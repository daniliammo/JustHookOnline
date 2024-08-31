using Cars;
using Mirror;


public class Interactable : NetworkBehaviour
{

    public InteractType interactType;
    public string password;
    public string interactName;
    public Vehicle vehicle;
    public DoorController door;
    

    [Command (requiresAuthority = false)]
    public void Interact()
    {
        door!.OpenDoor();
    }

    [Command (requiresAuthority = false)]
    public void Interact(Player.Player player)
    {
        vehicle.CmdTryToSitOnADriverPlace(player);
    }
    
    [Command (requiresAuthority = false)]
    public void CheckPassword(string passwd)
    {
        if(passwd != password)
            RpcPlaySoundIncorrectPassword();
        
        if(passwd == password)
        {
            RpcPlaySoundCorrectPassword();
            Invoke(nameof(Interact), 1);
        }
    }
    
    [ClientRpc]
    public void RpcPlaySoundCorrectPassword()
    {
        
    }
    
    [ClientRpc]
    public void RpcPlaySoundIncorrectPassword()
    {
        
    }
    
}
