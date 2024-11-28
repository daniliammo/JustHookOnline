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
        
        public string passwordString = string.Empty;

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
            
            if (currentInteractable == other.GetComponent<Interactable.Interactable>())
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
            if (!currentInteractable) return;
            
            if (Input.GetKeyDown(KeyCode.E) && _isWritingPassword)
            {
                _isWritingPassword = false;
                _ui.passwordEntryGameObject.SetActive(false);
                ResetPassword();
                return;
            }
            
            if (Input.GetKeyDown(KeyCode.E))
                Interact();

            #if !UNITY_IOS || !UNITY_ANDROID
            KeyboardInputPassword();
            #endif
        }

        public void Interact()
        {
            if (currentInteractable!.interactType == InteractType.OpenDoor || currentInteractable!.interactType == InteractType.SimpleInteract)
                currentInteractable!.Interact();
            
            if (currentInteractable!.interactType == InteractType.VehicleInteract)
                currentInteractable!.Interact(_player);

            if (currentInteractable!.interactType != InteractType.PasswordEntry) return;
            _ui.passwordEntryGameObject.SetActive(true);
                
            _isWritingPassword = true;
        }

        private void KeyboardInputPassword()
        {
            if (!_isWritingPassword) return;
            
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                passwordString += '0';
                OnPasswordUpdated();
            }
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                passwordString += '1';
                OnPasswordUpdated();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                passwordString += '2';
                OnPasswordUpdated();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                passwordString += '3';
                OnPasswordUpdated();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                passwordString += '4';
                OnPasswordUpdated();
            }
            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
                passwordString += '5';
                OnPasswordUpdated();
            }
            if (Input.GetKeyDown(KeyCode.Alpha6))
            {
                passwordString += '6';
                OnPasswordUpdated();
            }
            if (Input.GetKeyDown(KeyCode.Alpha7))
            {
                passwordString += '7';
                OnPasswordUpdated();
            }
            if (Input.GetKeyDown(KeyCode.Alpha8))
            {
                passwordString += '8';
                OnPasswordUpdated();
            }
            if (Input.GetKeyDown(KeyCode.Alpha9))
            {
                passwordString += '9';
                OnPasswordUpdated();
            }

            if (!Input.GetKeyDown(KeyCode.Backspace)) return;
            if (passwordString.Length <= 0) return;
            passwordString.Remove(passwordString.Length - 1);
            OnPasswordUpdated();
        }

        public void OnPasswordUpdated()
        {
            _ui.passwordEntry.text = passwordString; // Отображает пароль в интерфейсе.
            
            var passwd = currentInteractable!.intercom.password;
            
            if (passwordString.Length == passwd.Length)
            {
                currentInteractable!.intercom.CheckPassword(passwordString);
                if (passwordString != passwd)
                {
                    _ui.passwordEntry.color = Color.red;
                    _isWritingPassword = false;
                    Invoke(nameof(OnPasswordIncorrect), 1.5f);
                }
            }

            if (passwordString != passwd) return;
            _ui.passwordEntry.color = Color.green;
            _isWritingPassword = false;
            Invoke(nameof(OnPasswordCorrect), 1.5f);
        }
        
        private void OnPasswordCorrect()
        {
            ResetPassword();
            _ui.passwordEntryGameObject.SetActive(false);
            _isWritingPassword = true;
        }
        
        private void OnPasswordIncorrect()
        {
            ResetPassword();
            _isWritingPassword = true;
        }

        private void ResetPassword()
        {
            _ui.passwordEntry.color = Color.white;
            passwordString = string.Empty;
            OnPasswordUpdated();
        }
        
    }
}
