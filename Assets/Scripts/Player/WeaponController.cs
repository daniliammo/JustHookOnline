using Door;
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

		internal Transform Camera;
		
		// Оружие
		private bool _isReloading;
		private bool _isFire;
		private float _fireRate;

		private float _reloadTime;
		
		private byte _maxAmmoMagazine;
		private byte _currentAmmoInMagazine;
		private short _allAmmo;

		private byte _damage;
		
		public GameObject grenadePrefab;
		private bool _allowGrenade = true;
		public float grenadeCooldownTime = 3;
		
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
		
		// TODO: Реализация
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
			
			Camera = GameObject.FindGameObjectWithTag("MainCamera").transform;

			_bulletFlyBySoundSpawner = GetComponent<BulletFlyBySoundSpawner>();
			
			_gunAnimator = gun.GetComponent<Animator>();

			_hitMarkerController = GetComponent<HitMarkerController>();

			_hitSoundsController = GetComponent<HitSoundsController>();
		}
		
		public void StartWeapon()
		{
			// TODO: Сделать определение значений переменных автоматически под каждое оружие
			_maxAmmoMagazine = 30;
			_currentAmmoInMagazine = _maxAmmoMagazine;
			_reloadTime = 1.5f;
			_fireRate = 0.1f;
			_damage = 30;
			
			_ui.fullAmmoText.text = _maxAmmoMagazine.ToString();
			_ui.ammoText.text = _currentAmmoInMagazine.ToString();
		}
		
		private void Update()
		{
			if (RealInput.IsTouchSupported) return;
			
			if (Input.GetButton("Fire1"))
				Fire();
			
			if (Input.GetKeyDown(KeyCode.R))
				CmdReload();
			
			if (Input.GetKeyDown(KeyCode.G))
				TryThrowGrenade();
		}

		private void TryThrowGrenade()
		{
			if (!_allowGrenade) return;
			
			CmdThrowGrenade();
            
			StartGrenadeCooldown();
		}

		[Command (requiresAuthority = false)]
		private void CmdThrowGrenade()
		{
			var prefab = Instantiate(grenadePrefab, Camera.transform.position, Camera.transform.rotation);
			NetworkServer.Spawn(prefab);
			prefab.GetComponent<Rigidbody>().AddForce(Camera.forward * 3000);
		}
		
		private void StartGrenadeCooldown()
		{
			// _ui.sliderValueChanger.ChangeFillAmount(_ui.grenadeImage, grenadeCooldownTime);
			_allowGrenade = false;
			Invoke(nameof(EndGrenadeCooldown), grenadeCooldownTime);
		}

		private void EndGrenadeCooldown()
		{
			_allowGrenade = true;
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
			if (!isOwned || _isReloading || _isFire || _player.IsDeath || _ui.menu.isPaused) return;

			if (_currentAmmoInMagazine <= 0)
			{
				CmdReload();
				return;
			}
			
			_isFire = true;
			_currentAmmoInMagazine -= 1;
			_gunAnimator.Play("Fire");
			
			// Создание объекта с эффектом выстрела.
			CmdSpawnMuzzleFlashPrefab(muzzleFlashPosition.position, muzzleFlashPosition.rotation);
			
			// Логика выстрела.
			CmdCastRayCast(Camera.position, Camera.forward);
			
			// Создание объекта с эффектами летящей пули.
			CmdSpawnBulletParticlePrefab(muzzleFlashPosition.position, Vector3.zero);
			
			Invoke(nameof(StopFire), _fireRate);

			CmdFire();
		}

		[Command (requiresAuthority = false)]
		private void CmdFire()
		{
			RpcFire();
			TargetRpcFire();
		}
		
		[ClientRpc]
		private void RpcFire()
		{
			if (!gunShot[0].isPlaying)
				gunShot[0].Play();
			else if (!gunShot[1].isPlaying)
				gunShot[1].Play();
			else if (!gunShot[2].isPlaying)
				gunShot[2].Play();
		}
		
		[TargetRpc]
		private void TargetRpcFire()
		{
			_ui.ammoText.text = _currentAmmoInMagazine.ToString();
		}
		
		private void BreakingThrough(Vector3 direction, byte damageModifier)
		{
			Vector3 forwardMultiplier;
			
			if (_hit.collider.CompareTag("Player"))
				forwardMultiplier = Camera.forward / 1.25f;
			else
				forwardMultiplier = Camera.forward / 10;
			
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

			if (FindGameObject.Find(_hit.transform, "Boundary") || FindGameObject.Find(_hit.transform, "DeadZone")) 
				return;
			
			if (FindGameObject.Find(_hit.transform, "Glass", out var glass))
			{
				glass.GetComponent<BreakableWindow>().RpcBreakWindow();
				BreakingThrough(direction, 1);
				return;
			}

			if (FindGameObject.Find(_hit.transform, "Lamp", out var lampGameObject))
			{
				if (lampGameObject.TryGetComponent<Lamp>(out var lamp))
					lamp.CmdBreakLamp();
				else
					Debug.LogError($"Не получилось получить компонент Lamp на объекте: {lampGameObject.name}." 
					               + $" Игрок: {_player.playerDisplayName}");
				
				// BreakingThrough(direction, 2);
				return;
			}
			
			if (FindGameObject.Find(_hit.transform, "Player", out var player) 
			    && !_hit.collider.CompareTag("PlayerBulletFlyBy")) // Если обьект в который попали имеет тэг игрока
			{
				DamageEntity(player.GetComponent<LifeEntity>(), _hit, _damage);
				BreakingThrough(direction, 7);
				return;
			}
			
			// if (FindGameObject.Find(_hit.transform, "LifeEntity", out var outObject)) // Если обьект в который попали имеет тэг игрока
			// {
			// 	DamageEntity(outObject.GetComponent<LifeEntity>(), _hit, _damage);
			// 	BreakingThrough(direction, 7);
			// 	return;
			// }
			
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
				_hit.rigidbody.linearVelocity = Camera.forward * 20;
				
				BreakingThrough(direction, 3);
				return;
			}

			if (_hit.collider.CompareTag("PhysicalBody"))
			{
				_hit.rigidbody.linearVelocity = Camera.forward * 20;
				
				BreakingThrough(direction, 5);
				return;
			}

			if (_hit.collider.CompareTag("AirplaneCargo"))
			{
				CmdSetVelocity(_hit.rigidbody, gameObject.transform.forward * 5);
				return;
			}

			if (FindGameObject.Find(_hit.transform, "Interactable", out var findedGameObject)) // Door
			{
				BreakingThrough(direction, 7);
				findedGameObject.GetComponent<DoorController>().CmdSetHp(7);
			}

			if (!FindGameObject.Find(_hit.transform, "Player") &&
			    !FindGameObject.Find(_hit.transform, "PlayerBulletFlyBy") &&
			    !FindGameObject.Find(_hit.transform, "Glass"))
			{
				CmdSpawnBulletHolePrefab(_hit.point, Quaternion.Euler(Vector3.Angle(_hit.normal, Vector3.up), 0, 0));
				
				// Рикошет
				if (Vector3.Angle(_hit.normal, Camera.forward) <= 105)
					Ricochet(direction);
			}
		}

		private void Ricochet(Vector3 oldDirection)
		{
			// Луч
			var old_hit_point = _hit.point;

			CastRayCast(_hit.point, Vector3.Reflect(oldDirection, _hit.normal));

			CmdSpawnBulletHolePrefab(_hit.point, Quaternion.Euler(Vector3.Angle(_hit.normal, Vector3.up), 0, 0));
					
			// Спавн обьекта с партиклами летящей пули
			CmdSpawnBulletParticlePrefab(old_hit_point, _hit.point);
		}
		
		[Command (requiresAuthority = false)]
		public void DamageEntity(LifeEntity entity, RaycastHit hit, int damage)
		{
			entity.CmdSetHp(entity.hp - damage, _player.playerDisplayName);
			
			CmdSpawnBloodPrefab(hit.point, Quaternion.Euler(Vector3.Angle(hit.normal, Vector3.up), 0, 0));
			
			TargetRpcDamageEntity(entity);
		}

		[TargetRpc]
		private void TargetRpcDamageEntity(LifeEntity entity)
		{
			_hitMarkerController.EnableAndDisableMarker(entity.IsDeath);
			
			_hitSoundsController.PlayHitBassSound();
			_hitSoundsController.PlayHitMarkerSound();
			
			// Сущность убита.
			if (!entity.IsDeath || entity.hp > 0) return;
			if (entity.TryGetComponent<Player>(out var player))
				_hitMarkerController.SetPlayerKilledText(player.playerDisplayName);
			
			// Звук колокольчика.
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
			
			if (look != Vector3.zero)
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
			if (!isOwned) return;
			
			_gunAnimator.Play("Idle");
			
			_isFire = false;
			
			if (_currentAmmoInMagazine == 0)
				CmdReload();
		}
		
		#region Reload
		[Command (requiresAuthority = false)]
		public void CmdReload()
		{
			if (!isOwned || _isReloading || _currentAmmoInMagazine == _maxAmmoMagazine || _player.IsDeath || _ui.menu.isPaused) return;
			
			_isReloading = true;
			
			Invoke(nameof(StopReload), _reloadTime);
			
			_gunAnimator.Play("Reload");
			TargetRpcReload();
		}

		[TargetRpc]
		private void TargetRpcReload()
		{
			_ui.reloadText.SetActive(true);
		}
		
		private void StopReload()
		{
			if (!isOwned) return;
			
			_currentAmmoInMagazine = _maxAmmoMagazine;
			_isReloading = false;
			
			_gunAnimator.Play("Idle");
			TargetRpcStopReload();
		}

		[TargetRpc]
		private void TargetRpcStopReload()
		{
			_ui.ammoText.text = _currentAmmoInMagazine.ToString();
			_ui.reloadText.SetActive(false);
		}
		#endregion
		
	}
}
