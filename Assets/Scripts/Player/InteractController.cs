using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
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
        private string _passwordString = "";

        private bool _isWritingPassword;
        
        [CanBeNull] 
        public Interactable currentInteractable;
        

        [Client]
        private void Start()
        {
            _ui = FindObjectOfType<UIObjectsLinks>();
            _player = FindObjectOfType<Player>();
            
            _ui.localInteractController = this;
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
            
            switch (currentInteractable!.interactType)
            {
                case InteractType.SimpleInteract:
                    _ui.simpleInteract.SetActive(true);
                    _ui.simpleInteractNameText.text = currentInteractable.interactName;
                    break;
                case InteractType.VehicleInteract:
                    _ui.simpleInteract.SetActive(true);
                    _ui.simpleInteractNameText.text = currentInteractable.interactName;
                    break;
                case InteractType.PasswordEntry:
                    _ui.passwordEntryGameObject.SetActive(true);
                    _ui.passwordEntryNameText.text = currentInteractable.interactName;
                    break;
            }
        }

        [Server]
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Interactable")) return;
            
            if(currentInteractable == other.GetComponent<Interactable>())
            {
                switch (currentInteractable!.interactType)
                {
                    case InteractType.SimpleInteract:
                        _ui.simpleInteract.SetActive(false);
                        break;
                    case InteractType.PasswordEntry:
                        _ui.passwordEntryGameObject.SetActive(false);
                        break;
                }
            }

            currentInteractable = null;
        }
        
        private void FixedUpdate()
        {
            if(!currentInteractable) return;
            if (Input.GetKeyDown(KeyCode.E))
                Interact();

        }

        public void Interact()
        {
            if (currentInteractable!.interactType == InteractType.OpenDoor || currentInteractable!.interactType == InteractType.SimpleInteract)
                currentInteractable!.Interact();
            
            if (currentInteractable!.interactType == InteractType.VehicleInteract)
                currentInteractable!.Interact(_player);

            if (currentInteractable!.interactType == InteractType.PasswordEntry)
            {
                if (!_isWritingPassword)
                    _isWritingPassword = true;

                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E) && _isWritingPassword)
                {
                    _isWritingPassword = false;
                    _ui.passwordEntryGameObject.SetActive(false);
                }
                
                _isWritingPassword = true;

                ProcessPassword();
            }
        }

        public void ProcessPassword()
        {
            // if (!_isWritingPassword) return;
            #if !UNITY_IOS || !UNITY_ANDROID
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
            #endif

            var stringBuilder = new StringBuilder();

            foreach (var b in password)
                _passwordString = stringBuilder.Append(b).ToString();

            _ui.passwordEntry.text = _passwordString;
            
            if(_passwordString.Length == currentInteractable!.password.Length)
            {
                currentInteractable!.CheckPassword(_passwordString);
                if(currentInteractable.password != _passwordString)
                {
                    _ui.passwordEntry.color = Color.red;
                    Invoke(nameof(ResetPassword), 1);
                }
            }

            if(_passwordString.Length != currentInteractable!.password.Length)
                _ui.passwordEntry.color = Color.green;
        }
        
        private void ResetPassword()
        {
            password = new List<byte>();
            _passwordString = "";
        }
        
    }
}
