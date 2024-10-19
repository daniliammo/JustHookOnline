using System.Collections.Generic;
using Interactable;
using JetBrains.Annotations;
using Mirror;
using UI;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Player))]
    public class InteractController : NetworkBehaviour
    {

        private UIObjectsLinks _ui;
        private Player _player;

        
        public List<byte> password;
        private string _passwordString = "";

        private bool _isWritingPassword;
        
        [CanBeNull] 
        public Interactable.Interactable currentInteractable;
        

        [Client]
        private void Start()
        {
            _ui = FindFirstObjectByType<UIObjectsLinks>();
            _player = GetComponent<Player>();
            
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
            
            currentInteractable = other.GetComponent<Interactable.Interactable>();
            
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
                    _ui.simpleInteract.SetActive(true);
                    _ui.simpleInteractNameText.text = currentInteractable.interactName;
                    _ui.passwordEntryNameText.text = currentInteractable.intercom.passwordEntryText;
                    break;
            }
        }

        [Server]
        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Interactable")) return;
            
            if(currentInteractable == other.GetComponent<Interactable.Interactable>())
            {
                switch (currentInteractable!.interactType)
                {
                    case InteractType.SimpleInteract:
                        _ui.simpleInteract.SetActive(false);
                        break;
                    case InteractType.PasswordEntry:
                        _ui.passwordEntryGameObject.SetActive(false);
                        _isWritingPassword = false;
                        _ui.simpleInteract.SetActive(false);
                        break;
                }
            }

            currentInteractable = null;
        }
        
        private void Update()
        {
            if(!currentInteractable) return;
            
            if (Input.GetKeyDown(KeyCode.E) && _isWritingPassword)
            {
                _isWritingPassword = false;
                _ui.passwordEntryGameObject.SetActive(false);
                ResetPassword();
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.E))
                Interact();

            ProcessPassword();
        }

        public void Interact()
        {
            if (currentInteractable!.interactType == InteractType.OpenDoor || currentInteractable!.interactType == InteractType.SimpleInteract)
                currentInteractable!.Interact();
            
            if (currentInteractable!.interactType == InteractType.VehicleInteract)
                currentInteractable!.Interact(_player);

            if (currentInteractable!.interactType == InteractType.PasswordEntry)
            {
                _ui.passwordEntryGameObject.SetActive(true);
                
                _isWritingPassword = true;
            }
        }

        public void ProcessPassword()
        {
            if (!_isWritingPassword) return;
            
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
            {
                if(password.Count > 0)
                    password.RemoveAt(password.Count - 1);
            }
            #endif

            switch (password.Count)
            {
                case > 0:
                {
                    foreach (var b in password)
                        _passwordString += b.ToString();
                    break;
                }
                case 0:
                    _passwordString = "";
                    break;
            }


            _ui.passwordEntry.text = _passwordString;
            var passwd = currentInteractable!.intercom.password;
            
            if(_passwordString.Length == passwd.Length)
            {
                currentInteractable!.intercom.CheckPassword(_passwordString);
                if(_passwordString != passwd)
                {
                    _ui.passwordEntry.color = Color.red;
                    _isWritingPassword = false;
                    Invoke(nameof(OnPasswordIncorrect), 1.5f);
                }
            }

            if(_passwordString == passwd)
            {
                _ui.passwordEntry.color = Color.green;
                _isWritingPassword = false;
                Invoke(nameof(OnPasswordCorrect), 1.5f);
            }
        }

        private void OnPasswordCorrect()
        {
            ResetPassword();
            _ui.passwordEntry.color = Color.white;
            _ui.passwordEntryGameObject.SetActive(false);
            _isWritingPassword = true;
        }
        
        private void OnPasswordIncorrect()
        {
            ResetPassword();
            _isWritingPassword = true;
            _ui.passwordEntry.color = Color.white;
        }

        private void ResetPassword()
        {
            password = new List<byte>();
            _passwordString = "";
        }
        
    }
}
