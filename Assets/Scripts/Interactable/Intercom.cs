using System.Linq;
using Door;
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
            if (!password.All(char.IsDigit))
                Debug.LogWarning($"Пароль {password} невозможно ввести так как интерфейс разрешает вводить только цифры." +
                                 "\nДверь никогда не откроют.");
            
            var interactable = GetComponent<Interactable>();
            interactable.interactType = InteractType.PasswordEntry;
            interactable.intercom = this;
        }

        private void CheckPassword()
        {
            
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
            if (passwd != password)
                RpcPlayIncorrectPasswordSound();
        
            if (passwd == password)
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
        public void RpcPlayButtonSound()
        {
            audioSource.PlayOneShot(buttonSound);
        }
        
    }
}
