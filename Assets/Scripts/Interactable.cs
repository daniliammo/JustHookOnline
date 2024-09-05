using Cars;
using Mirror;
using UnityEngine;


[RequireComponent(typeof(BoxCollider), typeof(NetworkIdentity))]
public class Interactable : NetworkBehaviour
{

    public InteractType interactType;
    public string password;
    public string interactName;
    public string passwordEntryText;
    public Vehicle vehicle;
    public DoorController doorController;


    [Server]
    private void Start()
    {
        doorController = GetComponent<DoorController>();
    }

    [Command (requiresAuthority = false)]
    public void Interact()
    {
        doorController!.CmdOpenDoor();
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
