using Mirror;
using UnityEngine;

namespace Interactable
{
    public class Intercom : Interactable
    {
        
        [Header("Домофон и дверь")]
        public string password;
        public string passwordEntryText;
        public AudioSource audioSource;
        public AudioClip buttonSound;
        public AudioClip incorrectPasswordSound;
        public AudioClip correctPasswordSound;
        public DoorController doorController;


        private void Awake()
        {
            GetComponent<Interactable>().interactType = InteractType.PasswordEntry;
            GetComponent<Interactable>().intercom = this;
        }
        
        [Server]
        private void Start()
        {
            doorController = GetComponent<DoorController>();
            audioSource = GetComponent<AudioSource>();
        }

        public override void Interact()
        {
            doorController.CmdOpenDoor();
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
}
