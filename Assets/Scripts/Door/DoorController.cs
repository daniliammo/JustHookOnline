using Door;
using Mirror;
using UnityEngine;


[RequireComponent(typeof(NetworkAnimator), typeof(Animator), typeof(NetworkIdentity))]
public class DoorController : NetworkBehaviour
{

    private Animator _animator;
    public DoorStatus doorStatus;

    public bool requirePassword;
    
    public byte hp;
    
    
    [Server]
    private void Start()
    {
        _animator = GetComponent<Animator>();
        
        if(doorStatus == DoorStatus.Opened)
            CmdOpenDoor();
    }

    [Command (requiresAuthority = false)]
    public void CmdOpenDoor()
    {
        if(doorStatus == DoorStatus.Opened) return;
        
        _animator.Play("Open");
        doorStatus = DoorStatus.Opened;
        if(requirePassword)
            Invoke(nameof(CmdCloseDoor), 15);
    }
    
    [Command (requiresAuthority = false)]
    public void CmdCloseDoor()
    {
        if(doorStatus == DoorStatus.Closed) return;
        
        doorStatus = DoorStatus.Closed;
        _animator.Play("Close");
    }
    
}
