using Mirror;
using UI;
using UnityEngine;
using Utils;

namespace Player
{
    public class Hook : NetworkBehaviour
    {

        // TODO: Сделать переменную которая если будет true то когда игрок летит на крюке и пытается хукануть крюком в воздух то ничего не происходит и игрок продолжает лететь на крюке

        public Vector2 minHookCrosshairSize;
        public Vector2 maxHookCrosshairSize;
        
        [Header("Префабы")] 
        public Transform hookPosition;
        public GameObject hookPrefab;

        [Header("Звуки")] 
        public AudioSource hookSfx;
        public AudioSource hookPool;
        public AudioClip hookShot;
        public AudioClip hookCancel;
        public AudioClip hookLanding;
        private bool _playHookPool;

        private GameObject _hookGameObject;
        private GameObject _hookedPosition;
        private Transform _camera;

        private const byte MaxDistance = 255;
        private const byte HookPoolSpeed = 23;
        private const byte HookCableSpeed = 175;
        
        private float _startTime;
        private float _journeyLength;
        private RaycastHit _hit;
        private Vector3 _currentGrapplePosition;
        
        private Rigidbody _rb;
        private Player _player;
        private LineRenderer _lr;
        private UIObjectsLinks _ui;
        private WeaponController _weaponController;
        
        // Если true, то когда игрок летит на крюке он не может бросить крюк из-за того что попал в воздух или не хватило дистанции.
        private bool _isSafeHook = true;
        // True когда крюк зацепился за пол. Если крюк на полу, то отцепится когда игрок подлетит к нему.
        private bool _isHookOnFloor;
        // True когда игрок бросил крюк и крюк летит обратно к игроку.
        private bool _isHookCanceling;
        // True когда крюк летит к цели и еще не зацепился.
        private bool _isHookOnAir;
        // True когда игрок летит к крюку
        internal bool IsHooking;
        // Если true, то рисуется линия от игрока к крюку
        private bool _drawRope;
        

        private void Start()
        {
            _player = GetComponent<Player>();
            _lr = GetComponent<LineRenderer>();
            _rb = GetComponent<Rigidbody>();
            _weaponController = GetComponent<WeaponController>();
            
            _ui = FindObjectOfType<UIObjectsLinks>();
            // ReSharper disable once PossibleNullReferenceException
            _camera = Camera.main.transform;

            CheckPlayerPrefsKeys();
            _isSafeHook = PlayerPrefsBoolean.GetBool("SafeHook");
        }

        private static void CheckPlayerPrefsKeys()
        {
            if (!PlayerPrefs.HasKey("SafeHook"))
                PlayerPrefsBoolean.SetBool("SafeHook", true);
        }
        
        private void Update()
        {
            if (!isOwned) return;
            
            if (!RealInput.IsTouchSupported) // Чтобы от нажатия по экрану не пулялся крюк
            {
                if (Input.GetMouseButtonDown(1))
                {
                    if(!_isHookOnAir)
                        StopGrapple();

                    if (_isHookOnAir)
                    {
                        StopGrapple();
                        _isHookOnAir = false;
                    }

                    InvokeRepeating(nameof(Test), 0, 0.05f);
                }

                // Бросить Крюк
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    if(!_isHookOnAir)
                        StopGrapple();

                    if (_isHookOnAir)
                    {
                        StopGrapple();
                        _isHookOnAir = false;
                    }
                }
            }
            
            CheckDistance();
            DrawRope();
        }

        private void Test()
        {
            if(!_isHookCanceling)
            {
                CancelInvoke(nameof(Test));
                JustHook();
            }
        }
        
        public void JustHook()
        {
            _isHookOnAir = false;
            if(_isHookCanceling)
                return;
            
            if(!_isSafeHook)
            {
                if(!_isHookOnAir)
                    StopGrapple();

                if (_isHookOnAir)
                {
                    _isHookOnAir = false;
                    Stop();
                }
                StartGrapple();
            }

            if (_isSafeHook)
            {
                if (_ui.hookCrosshair.activeSelf)
                {
                    Stop();
                    StartGrapple();
                }
            }
        }

        public void Stop()
        {
            if(_isHookCanceling)
            {
                _isHookCanceling = false;
                Destroy(_hookGameObject);
            }
            
            StopDrawRope();
            StopGrapple();
        }

        private void CheckDistance()
        {
            if (Physics.Raycast(_camera.position, _camera.forward, out var hit, MaxDistance,
                    Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
            {
                if (hit.transform.CompareTag("DeadZone") || hit.transform.CompareTag("Boundary"))
                {
                    _ui.hookCrosshair.SetActive(false);
                    return;
                }
                
                _ui.hookCrosshair.SetActive(true);

                var hookCrosshairSize = new Vector2
                {
                    x = minHookCrosshairSize.x * (MaxDistance / hit.distance),
                    y = minHookCrosshairSize.y * (MaxDistance / hit.distance)
                };
                    
                if (hookCrosshairSize.x < minHookCrosshairSize.x)
                    hookCrosshairSize = minHookCrosshairSize;
                    
                if (hookCrosshairSize.x > maxHookCrosshairSize.x)
                    hookCrosshairSize = maxHookCrosshairSize;
                    
                _ui.hookCrosshairRectTransform.sizeDelta = hookCrosshairSize;
                return;
            }
            
            _ui.hookCrosshair.SetActive(false);
        }

        private void DrawRope()
        {
            if (!_drawRope) return;

            var distCovered = (Time.time - _startTime) * HookCableSpeed;
            var fractionOfJourney = distCovered / _journeyLength;
            if(!_hookedPosition)
            {
                LineRendererSetPositionCount();
                StopGrapple();
                return;
            }

            _currentGrapplePosition =
                Vector3.Lerp(hookPosition.position, _hookedPosition.transform.position, fractionOfJourney);

            _hookGameObject.transform.position = _currentGrapplePosition;

            if (Vector3.Distance(_hookGameObject.transform.position, _hookedPosition.transform.position) <= 0.1f)
            {
                if (!IsHooking && !_isHookCanceling)
                    Grapple();

                if (_playHookPool)
                    CmdPlayOrStopHookPoolSound(false);
            }

            LineRendererSetPositionCount();
        }

        private void LineRendererSetPositionCount()
        {
            CmdSetPositionCount(2);
            _lr.positionCount = 2;
            _lr.SetPosition(0, hookPosition.position);
            _lr.SetPosition(1, _hookGameObject.transform.position);

            CmdSetLineRendererPosition(hookPosition.position, _hookGameObject.transform.position);
        }

        private void FixedUpdate()
        {
            if(!isOwned) return;
            if (_isHookCanceling)
            {
                _hookGameObject.transform.position = Vector3.MoveTowards(_hookGameObject.transform.position, transform.position, HookCableSpeed * Time.fixedDeltaTime);

                if (Vector3.Distance(_hookGameObject.transform.position, transform.position) <= 0.3f)
                {
                    StopDrawRope();
                    _isHookCanceling = false;
                    CmdPlayHookCancelSound();
                    Destroy(_hookGameObject);
                }
            }
            
            if(_isHookOnAir && _hookGameObject || _isHookCanceling)
                _hookGameObject.transform.Rotate(new Vector3(0, 0, 14.4f));

            if(!_isHookOnFloor) return;
            if(!IsHooking) return;
            
            // Если обьект в который попал крюк удален крюк должен полететь обратно
            if(!_hookedPosition)
                StopGrapple();
            
            var toGrapplePoint = _hookGameObject.transform.position - transform.position;
            _rb.linearVelocity = toGrapplePoint.normalized * HookPoolSpeed;
            
            var direction = _hookGameObject.transform.position  - transform.position;
            var distanceToGrapplePoint = direction.magnitude;
            
            if (distanceToGrapplePoint < 0.4f)
                StopGrapple();

            if (!(distanceToGrapplePoint <= 1)) return;
            
            _playHookPool = false;
            // Установление скорости воспроизведения звука стягивания
            CmdSetPitchHookPoolSound(_rb.linearVelocity.magnitude / HookPoolSpeed);
            CmdPlayOrStopHookPoolSound(true);
        }

        private void StartGrapple() 
        {
            if(_player.isDeath) return;
            if(_ui.menu.isPaused) return;
            
            if (!Physics.Raycast(_camera.position, _camera.forward, out _hit,
                    MaxDistance, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore)) return;
            
            if(_hit.transform.CompareTag("DeadZone") && _hit.transform.CompareTag("Boundary")) return;
            
            CmdPlayHookShotSound();
            
            _startTime = Time.time;
            _journeyLength = Vector3.Distance(hookPosition.position, _hit.point);
            
            _ui.cancelHookingButton.SetActive(true);
            
            _drawRope = true;
            _isHookOnAir = true;
            
            CmdSetPositionCount(2);
            _currentGrapplePosition = hookPosition.position;

            if (Vector3.Angle(_hit.normal, Vector3.up) is >= 35 or >= -35)
                _isHookOnFloor = true;

            _hookGameObject = Instantiate(hookPrefab);
            
            _hookGameObject.transform.LookAt(_camera);
            // _hookGameObject.transform.rotation = Quaternion.Euler(-_hookGameObject.transform.rotation.eulerAngles.x, _hookGameObject.transform.rotation.eulerAngles.y + 180, _hookGameObject.transform.rotation.eulerAngles.z);

            // _hookGameObject.transform.LookAt(_camera);
            // _hookGameObject.transform.rotation = Quaternion.Euler(0, _hookGameObject.transform.rotation.eulerAngles.y + 180, _hookGameObject.transform.rotation.eulerAngles.z);
            
            _hookedPosition = new GameObject("Hooked position")
            {
                transform =
                {
                    position = _hit.point,
                    parent = _hit.transform
                }
            };
        }
        
        private void Grapple()
        {
            _isHookOnAir = false;
            _isHookCanceling = false;
            _playHookPool = true;
            IsHooking = true;
            
            _hookGameObject.transform.LookAt(_camera);
            _hookGameObject.transform.rotation = Quaternion.Euler(-_hookGameObject.transform.rotation.eulerAngles.x, _hookGameObject.transform.rotation.eulerAngles.y + 180, _hookGameObject.transform.rotation.eulerAngles.z);
            
            if (_hit.collider.CompareTag("Player") && !_hit.collider.CompareTag("PlayerBulletFlyBy"))
                _weaponController.DamagePlayer(_hit, 5);
            
            if (!_hit.collider.CompareTag("Player"))
                _weaponController.CmdSpawnBulletHolePrefab(_hit.point, Quaternion.Euler(Vector3.Angle(_hit.normal, Vector3.up), 0, 0));
        }
        
        public void StopGrapple()
        {
            CmdPlayOrStopHookPoolSound(true);
            if(IsHooking || _isHookOnAir)
            {
                _isHookOnAir = false;
                _isHookCanceling = true;
                CmdPlayHookLandingSound();
            }
            
            _ui.cancelHookingButton.SetActive(false);
            
            IsHooking = false;
            _isHookOnFloor = false;
            
            Destroy(_hookedPosition);
        }
        
        private void StopDrawRope()
        {
            CmdSetPositionCount(0);
            _drawRope = false;
        }
        
        #region Sounds
        
        [Command (requiresAuthority = false)]
        private void CmdPlayHookCancelSound()
        {
            RpcPlayHookCancelSound();
        }

        [ClientRpc]
        private void RpcPlayHookCancelSound()
        { 
            hookSfx.PlayOneShot(hookCancel);
        }
        
        [Command (requiresAuthority = false)]
        private void CmdPlayHookLandingSound()
        {
            RpcPlayHookLandingSound();
        }

        [ClientRpc]
        private void RpcPlayHookLandingSound()
        { 
            hookSfx.PlayOneShot(hookLanding);
        }
        
        [Command (requiresAuthority = false)]
        private void CmdSetPitchHookPoolSound(float pitch)
        {
            RpcSetPitchHookPoolSound(pitch);
        }

        [ClientRpc]
        private void RpcSetPitchHookPoolSound(float pitch)
        {
            hookPool.pitch = pitch;
        }
        
        [Command (requiresAuthority = false)]
        private void CmdPlayOrStopHookPoolSound(bool stop)
        {
            RpcPlayOrStopHookPoolSound(stop);
        }

        [ClientRpc]
        private void RpcPlayOrStopHookPoolSound(bool stop)
        {
            switch (stop)
            {
                case true:
                    hookPool.Stop();
                    break;
                case false:
                    if(!hookPool.isPlaying)
                        hookPool.Play();
                    break;
            }
        }
        
        [Command (requiresAuthority = false)]
        private void CmdPlayHookShotSound()
        {
            RpcPlayHookShotSound();
        }

        [ClientRpc]
        private void RpcPlayHookShotSound()
        {
            hookSfx.PlayOneShot(hookShot);
        }
        
        #endregion

        #region NetworkLineRenderer
        
        [Command (requiresAuthority = false)]
        private void CmdSetPositionCount(int positionCount) => RpcSetPositionCount(positionCount);

        [ClientRpc]
        private void RpcSetPositionCount(int positionCount) => _lr.positionCount = positionCount;
        
        [Command (requiresAuthority = false)]
        private void CmdSetLineRendererPosition(Vector3 start, Vector3 end) =>
            RpcUpdateLineRendererPosition(start, end);

        [ClientRpc]
        private void RpcUpdateLineRendererPosition(Vector3 start, Vector3 end)
        {
            // Обновление позиции LineRenderer на всех клиентах
            _lr.SetPosition(0, start);
            _lr.SetPosition(1, end);
        }
        
        #endregion
        
    }
}
