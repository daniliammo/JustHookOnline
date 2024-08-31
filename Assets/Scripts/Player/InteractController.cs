using System.Collections.Generic;
using Mirror;
using UI;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(BoxCollider))]
    public class InteractController : NetworkBehaviour
    {

        private UIObjectsLinks _ui;

        private Player _player;

        public List<byte> password;
        private string _passwordString;

        private bool _isWritingPassword;
        
        public Interactable currentInteractable;
        

        private void Start()
        {
            _ui = FindObjectOfType<UIObjectsLinks>();
            _player = FindObjectOfType<Player>();
        }


        [Server]
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Interactable")) return;

            if (!Physics.Raycast(transform.position,
                    other.transform.position + other.transform.up / 2 - transform.position,
                    out var hit, 5, Physics.DefaultRaycastLayers,
                    QueryTriggerInteraction.Ignore)) return;
            
            if (hit.transform.gameObject != gameObject &&
                hit.transform.gameObject != other.transform.gameObject) return;
            
            currentInteractable = other.GetComponent<Interactable>();
            
            switch (currentInteractable.interactType)
            {
                case InteractType.SimpleInteract:
                    _ui.simpleInteract.SetActive(true);
                    _ui.simpleInteractText.text = currentInteractable.interactName;
                    break;
                case InteractType.PasswordEntry:
                    _ui.passwordEntry.SetActive(true);
                    break;
            }
        }

        [Server]
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Interactable")) return;
            
            if(currentInteractable == other.GetComponent<Interactable>())
            {
                switch (currentInteractable.interactType)
                {
                    case InteractType.SimpleInteract:
                        _ui.simpleInteract.SetActive(false);
                        break;
                    case InteractType.PasswordEntry:
                        _ui.passwordEntry.SetActive(false);
                        break;
                }
            }

            currentInteractable = null;
        }
        
        private void FixedUpdate()
        {
            if (currentInteractable!.interactType == InteractType.OpenDoor || currentInteractable!.interactType == InteractType.SimpleInteract && Input.GetKeyDown(KeyCode.E))
                currentInteractable!.Interact();
            
            if (currentInteractable!.interactType == InteractType.VehicleInteract && Input.GetKeyDown(KeyCode.E))
                currentInteractable!.Interact(_player);
            
            if (currentInteractable!.interactType == InteractType.PasswordEntry)
            {
                if (Input.GetKeyDown(KeyCode.E) && !_isWritingPassword)
                    _isWritingPassword = true;

                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E) && _isWritingPassword)
                {
                    _isWritingPassword = false;
                    _ui.passwordEntry.SetActive(false);
                }
                
                if (Input.GetKeyDown(KeyCode.E))
                    _isWritingPassword = true;
                
                if (!_isWritingPassword) return;
                if(Input.GetKeyDown(KeyCode.Alpha0))
                    password.Add(0);
                if(Input.GetKeyDown(KeyCode.Alpha1))
                    password.Add(1);
                if(Input.GetKeyDown(KeyCode.Alpha2))
                    password.Add(2);
                if(Input.GetKeyDown(KeyCode.Alpha3))
                    password.Add(3);
                if(Input.GetKeyDown(KeyCode.Alpha4))
                    password.Add(4);
                if(Input.GetKeyDown(KeyCode.Alpha5))
                    password.Add(5);
                if(Input.GetKeyDown(KeyCode.Alpha6))
                    password.Add(6);
                if(Input.GetKeyDown(KeyCode.Alpha7))
                    password.Add(7);
                if(Input.GetKeyDown(KeyCode.Alpha8))
                    password.Add(8);
                if(Input.GetKeyDown(KeyCode.Alpha9))
                    password.Add(9);
                
                if (Input.GetKeyDown(KeyCode.Backspace))
                    password.RemoveAt(password.Count);

                for (var index = 0; index < password.Count; index++)
                    _passwordString = _passwordString.Insert(index, password[index].ToString());
            
                _ui.passwordEntryText.text = _passwordString;
                
                if(_passwordString.Length == currentInteractable!.password.Length)
                {
                    currentInteractable!.CheckPassword(_passwordString);
                    if(currentInteractable.password != _passwordString)
                        _ui.passwordEntryText.color = Color.red;
                }

                if(_passwordString.Length != currentInteractable!.password.Length)
                    _ui.passwordEntryText.color = Color.green;
            }
        }

        private void ResetPassword()
        {
            password = new List<byte>();
            _passwordString = "";
        }
        
    }
}
