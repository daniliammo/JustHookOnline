using Door;
using Mirror;
using UnityEngine;


[RequireComponent(typeof(NetworkAnimator), typeof(Animator))]
public class DoorController : NetworkBehaviour
{

    public Animator animator;
    
    public DoorStatus doorStatus;
    
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        if(doorStatus == DoorStatus.Opened) return;
        
        animator.Play("Open");
        doorStatus = DoorStatus.Opened;
        if(doorStatus == DoorStatus.RequirePassword)
            Invoke(nameof(CloseDoor), 15);
    }
    
    public void CloseDoor()
    {
        if(doorStatus == DoorStatus.Closed) return;
        
        doorStatus = DoorStatus.Closed;
        animator.Play("Close");
    }
    
}
