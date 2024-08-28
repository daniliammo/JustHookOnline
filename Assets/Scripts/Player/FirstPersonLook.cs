using GameSettings.Control;
using Mirror;
using UI;
using UnityEngine;
using Utils;

namespace Player
{
    public class FirstPersonLook : NetworkBehaviour
    {
        
        // Для управление камерой с тачскрина
    
        private Vector3 _firstPoint;
        private Vector3 _secondPoint;
        private float _xAngle;
        private float _yAngle;
        private float _xAngleTemp;
        private float _yAngleTemp;
        
        
        public Transform character;
        private float _sensitivity;

        public Light spotLight;
        
        private Vector2 _velocity;
        private Vector2 _frameVelocity;
        
        private UIObjectsLinks _ui;
        private ControlSettings _controlSettings;
        
        private const float Smoothing = 1.5f;

        private bool _isFlashLightAllowed;
        
        private Camera _camera;
        
        
        public override void OnStartLocalPlayer()
        {
            _camera = Camera.main;
            // ReSharper disable once PossibleNullReferenceException
            _camera.transform.SetParent(transform, false);
            _camera.transform.SetLocalPositionAndRotation(new Vector3(0, 0, 0), new Quaternion());
        }

        public override void OnStartClient()
        {
            _isFlashLightAllowed = PlayerPrefsBoolean.GetBool("LightSettings:IsFlashLightAllowed");
        }
        
        private void Start()
        {
            CursorController.SetCursorLockState(CursorLockMode.Locked);
            
            _sensitivity = PlayerPrefs.GetFloat("ControlSettings:Sensitivity");
            
            _controlSettings = FindObjectOfType<ControlSettings>();
            _ui = FindObjectOfType<UIObjectsLinks>();

            _controlSettings.OnSensitivityChanged += ChangeSensitivityTo;
        }
        
        private void ChangeSensitivityTo(float newSensitivity)
        {
            _sensitivity = newSensitivity;
        }
        
        private void Update()
        {
            if (!isOwned) return;
            if(_ui.menu.isPaused) return;
            
            // Если есть тачскрин то вызывается метод TouchInput(); и функция дальше не выполняется
            if (RealInput.IsTouchSupported)
            {
                TouchInput();
                return;
            }

            if (Input.GetKeyDown(KeyCode.F))
                CmdEnableFlashlight();
            
            // Get smooth velocity.
            var mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            var rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * _sensitivity);
            _frameVelocity = Vector2.Lerp(_frameVelocity, rawFrameVelocity, 1 / Smoothing);
            _velocity += _frameVelocity;
            _velocity.y = Mathf.Clamp(_velocity.y, -90, 90);
            
            // Rotate camera up-down and controller left-right from velocity.
            transform.localRotation = Quaternion.AngleAxis(-_velocity.y, Vector3.right);
            character.localRotation = Quaternion.AngleAxis(_velocity.x, Vector3.up);
        }

        [Command (requiresAuthority = false)]
        public void CmdEnableFlashlight() => RpcEnableFlashlight();
        
        [ClientRpc]
        private void RpcEnableFlashlight()
        {
            if(!_isFlashLightAllowed) return;
            spotLight.enabled = !spotLight.enabled;
        }
        
        private void TouchInput()
        {
            foreach (var touch in Input.touches)
            {
                if (touch.position.x > Screen.width / 2.25f && touch.phase == TouchPhase.Began)
                {
                    _firstPoint = touch.position;
                    _xAngleTemp = _xAngle;
                    _yAngleTemp = _yAngle;
                }

                if (touch.position.x > Screen.width / 2.25f && touch.phase == TouchPhase.Moved)
                {
                    _secondPoint = touch.position;
                    var delta = _secondPoint - _firstPoint;
                    _xAngle = _xAngleTemp - delta.y / Screen.height * 90 * _sensitivity;
                    _yAngle = _yAngleTemp + delta.x / Screen.width * 180 * _sensitivity;

                    _xAngle = Mathf.Clamp(_xAngle, -90, 90);

                    character.rotation = Quaternion.Euler(0, _yAngle, 0);
                    transform.rotation = Quaternion.Euler(_xAngle, _yAngle, 0);
                }
            }
        }
    
    }
}
