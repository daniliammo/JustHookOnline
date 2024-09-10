using Cars;
using Mirror;
using UnityEngine;


[RequireComponent(typeof(BoxCollider), typeof(NetworkIdentity))]
public class Interactable : NetworkBehaviour
{

    [Header("Основа")]
    public InteractType interactType;
    public string interactName;
    
    [Header("Домофон и дверь")]
    public string password;
    public string passwordEntryText;
    public AudioSource audioSource;
    public AudioClip buttonSound;
    public AudioClip incorrectPasswordSound;
    public AudioClip correctPasswordSound;
    public DoorController doorController;
    
    [Header("Машина")]
    public Vehicle vehicle;


    [Server]
    private void Start()
    {
        doorController = GetComponent<DoorController>();
        audioSource = GetComponent<AudioSource>();
    }

    [Command (requiresAuthority = false)]
    public void Interact()
    {
        doorController.CmdOpenDoor();
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
            RpcPlayIncorrectPasswordSound();
        
        if(passwd == password)
        {
            RpcPlayCorrectPasswordSound();
            Invoke(nameof(Interact), 1);
        }
    }
    
    [ClientRpc]
    private void RpcPlayCorrectPasswordSound()
    {
        audioSource.PlayOneShot(correctPasswordSound);
    }
    
    [ClientRpc]
    private void RpcPlayIncorrectPasswordSound()
    {
        audioSource.PlayOneShot(incorrectPasswordSound);
    }
    
    [ClientRpc]
    private void RpcPlayButtonSound()
    {
        audioSource.PlayOneShot(buttonSound);
    }
    
}
