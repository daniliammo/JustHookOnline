using Door;
using Mirror;
using UnityEngine;


[RequireComponent(typeof(NetworkAnimator), typeof(Animator), typeof(NetworkIdentity))]
public class DoorController : NetworkBehaviour
{

    private Animator _animator;
    public DoorStatus doorStatus;

    public bool requirePassword;
    
    [SyncVar]
    public int hp;
    
    public GameObject[] physicComponents;
    
    
    [Server]
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    [Command (requiresAuthority = false)]
    public void CmdShooted(int damage)
    {
        hp -= damage;
        if(hp <= 0)
            RpcDestroyed();
    }

    [ClientRpc]
    private void RpcDestroyed()
    {
        foreach (var physicComponent in physicComponents)
        {
            gameObject.tag = "Untagged";
            physicComponent.transform.parent = null;
            physicComponent.AddComponent<Rigidbody>();
            physicComponent.tag = "Garbage";
        }
        Destroy(GetComponent<Interactable.Interactable>());
        Destroy(this);
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
