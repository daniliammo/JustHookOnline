using Mirror;
using UI;
using UnityEngine;
using Utils;

namespace Player
{
	public class WeaponController : NetworkBehaviour
	{

		// TODO: Сделать комментарии

		private UIObjectsLinks _ui;

		private Transform _camera;
		
		// Оружие
		private bool _isReloading;
		private bool _isFire;
		private float _fireRate;

		private float _reloadTime;
		
		private byte _fullAmmo;
		private byte _ammo;

		private byte _damage;
		
		private Player _player;
		
		public GameObject gun;
		public GameObject muzzleFlashPrefab;
		public Transform muzzleFlashPosition;
		
		public GameObject bulletHolePrefab;
		public GameObject bloodPrefab;

		public GameObject bulletParticlePrefab;
		
		private Animator _gunAnimator;

		private RaycastHit _hit;

		private BulletFlyBySoundSpawner _bulletFlyBySoundSpawner;

		private HitMarkerController _hitMarkerController;
		private HitSoundsController _hitSoundsController;
		
		// Звуки
		[Header("Звуки")] 
		private AudioSource _playerFX;
		public AudioSource[] gunShot;
		
		private bool _isFireLightAllowed;
		

		private void Start()
		{
			GetComponents();
			StartWeapon();
			CheckPlayerPrefsKeys();
			
			_isFireLightAllowed = PlayerPrefsBoolean.GetBool("LightSettings:IsFireLightAllowed");
		}

		private static void CheckPlayerPrefsKeys()
		{
			if (!PlayerPrefs.HasKey("Kills"))
				PlayerPrefs.SetInt("Kills", 0);
		}
		
		private void GetComponents()
		{
			_player = GetComponent<Player>();

			_playerFX = GetComponent<AudioSource>();
			
			_ui = FindFirstObjectByType<UIObjectsLinks>();
			
			_camera = GameObject.FindGameObjectWithTag("MainCamera").transform;

			_bulletFlyBySoundSpawner = GetComponent<BulletFlyBySoundSpawner>();
			
			_gunAnimator = gun.GetComponent<Animator>();

			_hitMarkerController = GetComponent<HitMarkerController>();

			_hitSoundsController = GetComponent<HitSoundsController>();
		}
		
		public void StartWeapon()
		{
			// TODO: Сделать определение значений переменных автоматически под каждое оружие
			_fullAmmo = 30;
			_ammo = _fullAmmo;
			_reloadTime = 1.5f;
			_fireRate = 0.08f;
			_damage = 15;
			
			_ui.fullAmmoText.text = _fullAmmo.ToString();
			_ui.ammoText.text = _ammo.ToString();
		}
		
		private void Update()
		{
			if(RealInput.IsTouchSupported) return;
			
			if (Input.GetButton("Fire1"))
				Fire();
			
			if (Input.GetKeyDown(KeyCode.R))
				Reload();
		}

		public void OnFireButtonDown()
		{
			InvokeRepeating(nameof(Fire), 0.0001f, 0.0001f);
		}

		public void OnFireButtonUp()
		{
			CancelInvoke(nameof(Fire));
		}
		
		private void Fire()
		{
			if(!isOwned) return;
			if(_isReloading) return;
			if(_isFire) return;
			if(_player.isDeath) return;
			if(_ui.menu.isPaused) return;
			if (_ammo <= 0)
			{
				Reload();
				return;
			}
			
			_isFire = true;
			
			if (!gunShot[0].isPlaying)
				gunShot[0].Play();
			else if (!gunShot[1].isPlaying)
				gunShot[1].Play();
			else if (!gunShot[2].isPlaying)
				gunShot[2].Play();
			
			CmdSpawnMuzzleFlashPrefab(muzzleFlashPosition.position, muzzleFlashPosition.rotation);
			
			// Луч
			CmdCastRayCast(_camera.position, _camera.forward);
			
			// Спавн обьекта с партиклами летящей пули
			CmdSpawnBulletParticlePrefab(muzzleFlashPosition.position, Vector3.zero);
			
			_ammo -= 1;
			_ui.ammoText.text = _ammo.ToString();
			
			_gunAnimator.Play("Fire");
			
			Invoke(nameof(StopFire), _fireRate);
		}

		[Server]
		private void BreakingThrough(Vector3 direction, byte damageModifier)
		{
			Vector3 forwardMultiplier;
			
			if (_hit.collider.CompareTag("Player"))
				forwardMultiplier = _camera.forward / 1.25f;
			else
				forwardMultiplier = _camera.forward / 10;
			
			var adjustedPoint = _hit.point + forwardMultiplier;
			_damage -= damageModifier;
			CastRayCast(adjustedPoint, direction);
		}

		[Command (requiresAuthority = false)]
		private void CmdCastRayCast(Vector3 origin, Vector3 direction)
		{
			CastRayCast(origin, direction);
		}
		
		private void CastRayCast(Vector3 origin, Vector3 direction)
		{
			Physics.Raycast(origin, direction, out _hit, Mathf.Infinity);
			// ReSharper disable all Unity.PerformanceCriticalCodeInvocation

			if(FindGameObject.Find(_hit.transform, "Boundary") || FindGameObject.Find(_hit.transform, "DeadZone")) 
				return;
			
			if (FindGameObject.Find(_hit.transform, "Glass", out var glass))
			{
				print(glass.name);
				glass.GetComponent<BreakableWindow>().RpcBreakWindow();
				BreakingThrough(direction, 1);
				return;
			}

			if(FindGameObject.Find(_hit.transform, "Lamp", out var lampGameObject))
			{
				if(lampGameObject.TryGetComponent<Lamp>(out var lamp))
					lamp.CmdBreakLamp();
				else
					Debug.LogError($"Не получилось получить компонент Lamp на объекте: {lampGameObject.name}." 
					               + $" Игрок: {_player.playerDisplayName}");
				
				// BreakingThrough(direction, 2);
				return;
			}
			
			if (FindGameObject.Find(_hit.transform, "Player") &&
			    !FindGameObject.Find(_hit.transform,
				    "PlayerBulletFlyBy")) // Если обьект в который попали имеет тэг игрока
			{
				DamagePlayer(_hit, _damage);
				BreakingThrough(direction, 7);
				return;
			}

			if (FindGameObject.Find(_hit.transform, "PlayerBulletFlyBy"))
			{
				_bulletFlyBySoundSpawner.CmdSpawnBulletFlyBySound(_hit.point, new Quaternion());
				
				BreakingThrough(direction, 0);
				return;
			}

			if (FindGameObject.Find(_hit.transform, "ExplosiveBarrel", out var explosiveBarrel))
			{
				CmdSetVelocity(_hit.rigidbody, gameObject.transform.forward * 5);
				explosiveBarrel.GetComponent<ExplosiveBarrel>().CmdShooted(15, _hit.point, gameObject.transform.position);
			}

			if (_hit.collider.CompareTag("ExplosiveBarrelFragments"))
			{
				_hit.rigidbody.linearVelocity = _camera.forward * 20;
				
				BreakingThrough(direction, 3);
				return;
			}

			if(_hit.collider.CompareTag("PhysicalBody"))
			{
				_hit.rigidbody.linearVelocity = _camera.forward * 20;
				
				BreakingThrough(direction, 5);
				return;
			}

			if (_hit.collider.CompareTag("AirplaneCargo"))
			{
				CmdSetVelocity(_hit.rigidbody, gameObject.transform.forward * 5);
				return;
			}

			if(FindGameObject.Find(_hit.transform, "Interactable", out var findedGameObject)) // Door
			{
				BreakingThrough(direction, 7);
				findedGameObject.GetComponent<DoorController>().CmdShooted(7);
			}

			if (!FindGameObject.Find(_hit.transform, "Player") &&
			    !FindGameObject.Find(_hit.transform, "PlayerBulletFlyBy") &&
			    !FindGameObject.Find(_hit.transform, "Glass"))
			{
				CmdSpawnBulletHolePrefab(_hit.point, Quaternion.Euler(Vector3.Angle(_hit.normal, Vector3.up), 0, 0));
				
				// Рикошет
				if (Vector3.Angle(_hit.normal, _camera.forward) <= 105)
				{
					// Луч
					var old_hit_point = _hit.point;

					CastRayCast(_hit.point, Vector3.Reflect(direction, _hit.normal));

					CmdSpawnBulletHolePrefab(_hit.point, Quaternion.Euler(Vector3.Angle(_hit.normal, Vector3.up), 0, 0));
					
					// Спавн обьекта с партиклами летящей пули
					CmdSpawnBulletParticlePrefab(old_hit_point, _hit.point);
				}
			}
		}
        
		[Server]
		public void DamagePlayer(RaycastHit hit, byte damage)
		{
			var shootedPlayer = hit.collider.GetComponent<Player>();
			
			shootedPlayer.CmdChangeHp(damage, transform, _player.playerDisplayName);
			
			CmdSpawnBloodPrefab(hit.point, Quaternion.Euler(Vector3.Angle(hit.normal, Vector3.up), 0, 0));
			
			_hitMarkerController.EnableAndDisableMarker(shootedPlayer.isDeath);
			
			_hitSoundsController.PlayHitBassSound();
			_hitSoundsController.PlayHitMarkerSound();
			
			
			if(!shootedPlayer.isDeath || shootedPlayer.hp > 0) return;

			_hitMarkerController.SetPlayerKilledText(shootedPlayer.playerDisplayName);
			
			// Звук колокольчика
			_hitSoundsController.PlayBellSound();
			
			PlayerPrefs.SetInt("Kills", PlayerPrefs.GetInt("Kills") + 1);
			print("Kills:" + PlayerPrefs.GetInt("Kills"));
		}

		#region Network Methods
		[Command (requiresAuthority = false)]
		// ReSharper disable once MemberCanBeMadeStatic.Local
		private void CmdSetVelocity(Rigidbody rigidbody, Vector3 velocity)
		{
			rigidbody.GetComponent<Rigidbody>().linearVelocity = velocity;
		}

		[Command (requiresAuthority = false)]
		private void CmdSpawnBulletParticlePrefab(Vector3 position, Vector3 look)
		{
			var prefab = Instantiate(bulletParticlePrefab, position, muzzleFlashPosition.rotation);
			NetworkServer.Spawn(prefab);
			
			if(look != Vector3.zero)
				prefab.transform.LookAt(look);
		}
		
		[Command (requiresAuthority = false)]
		private void CmdSpawnMuzzleFlashPrefab(Vector3 position, Quaternion rotation)
		{
			var prefab = Instantiate(muzzleFlashPrefab, position, rotation);
			prefab.transform.SetParent(transform);
			NetworkServer.Spawn(prefab);
		}

		[Command (requiresAuthority = false)]
		public void CmdSpawnBulletHolePrefab(Vector3 position, Quaternion rotation)
		{
			var prefab = Instantiate(bulletHolePrefab, position, rotation);
			NetworkServer.Spawn(prefab);
		}
		
		[Command (requiresAuthority = false)]
		private void CmdSpawnBloodPrefab(Vector3 position, Quaternion rotation)
		{
			var prefab = Instantiate(bloodPrefab, position, rotation);
			NetworkServer.Spawn(prefab);
		}
		#endregion
		
		private void StopFire()
		{
			if(!isOwned) return;
			
			_gunAnimator.Play("Idle");
			
			_isFire = false;
			if(_ammo == 0)
				Reload();
		}
		
		public void Reload()
		{
			if(!isOwned) return;
			if(_isReloading) return;
			if(_ammo == _fullAmmo) return;
			if(_player.isDeath) return;
			if(_ui.menu.isPaused) return;
			
			_isReloading = true;
			
			_ui.reloadText.SetActive(true);
			
			_gunAnimator.Play("Reload");
			
			Invoke(nameof(StopReload), _reloadTime);
		}

		private void StopReload()
		{
			if(!isOwned) return;
			
			_ammo = _fullAmmo;
			_ui.ammoText.text = _ammo.ToString();
			_ui.reloadText.SetActive(false);
			_isReloading = false;
			_gunAnimator.Play("Idle");
		}
		
	}
}
