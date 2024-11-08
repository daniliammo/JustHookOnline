using Cars;
using Mirror;
using UnityEngine;

namespace Explosion
{
    public class Explosion : NetworkBehaviour
    {

        public ExplosionSize explosionSize;

        public FireSize fireSize;
        public FireBehaviour fireBehaviour;

        [Range(2, 50)]
        [Tooltip("Радиус взрыва и нанесения урона")]
        public byte radius;
        
        [Range(75, 2000)]
        [Tooltip("Сила отбрасывания объектов от взрыва")]
        public short explosionForce;
        
        [Range(1.5f, 20)]
        [Tooltip("Этот модификатор определяет насколько сильно взрыв отбросит объекты вверх")]
        public float upwardsModifier;
        
        [Range(1, 452)]
        public byte maxDamageToPlayer;
        
        [Range(1, 500)]
        public byte maxDamageToVehicle;

        [Tooltip("Эти объекты будут созданы после взрыва и к ним будет применена физическая сила на отбрасывание")]
        public GameObject[] fragments;
        
        private ExplosionLinks _eL;
	    

        [Server]
        private void Start()
        {
	        _eL = FindFirstObjectByType<ExplosionLinks>();
        }

        [Command (requiresAuthority = false)]
        protected void CmdExplode()
        {
            RpcExplode();
            
            // ReSharper disable once Unity.PreferNonAllocApi
            var colliders = Physics.OverlapSphere(transform.position, radius);
	        foreach (var t in colliders)
	        {
		        if (t.CompareTag("Glass"))
		        {
			        t.GetComponent<BreakableWindow>().RpcBreakWindow();
			        continue;
		        }

		        if (t.CompareTag("Player") || t.CompareTag("Vehicle"))
	            {
	                // Пуляем райкаст, для того чтобы проверить игрок за стеной или нет. Чтобы не получилось так что взрыв убивает игрока через стенку
	                if (Physics.Raycast(transform.position, t.transform.position + t.transform.up / 2 - transform.position,
	                        out var hit, radius, Physics.DefaultRaycastLayers,
	                        QueryTriggerInteraction.Ignore))
	                {
		                if(hit.collider.CompareTag("Player"))
		                {
			                var player = t.GetComponent<LifeEntity>();
			                var damage = CalcDamage(DamageType.Player, player.transform);
			                player.CmdSetHp(damage, "Взрыв");
			                continue;
		                }

		                if (hit.collider.CompareTag("Vehicle"))
		                {
			                var vehicle = t.GetComponent<Vehicle>();
			                var damage = CalcDamage(DamageType.Vehicle, vehicle.transform);
			                // TODO: Реализация урона по машинам
		                }
	                }
	            }
	            
	            if (!t.attachedRigidbody) continue;
	            if (TryGetComponent<NetworkIdentity>(out var unused) && t.TryGetComponent<Rigidbody>(out var rigidbodyComponent))
	            {
		            rigidbodyComponent.AddExplosionForce(explosionForce, transform.position, radius, 0);
	                
	                if (Vector3.Distance(transform.position, t.attachedRigidbody.position) < radius) 
	                {
	                    if (t.GetComponent<ExplosiveBarrel>())
	                    {
		                    if (Physics.Raycast(transform.position, t.transform.position - transform.position,
			                        out var hit, radius, Physics.DefaultRaycastLayers,
			                        QueryTriggerInteraction.Ignore))
		                    {
			                    if(hit.transform.CompareTag("ExplosiveBarrel"))
									t.GetComponent<ExplosiveBarrel>().CmdShooted((int)Vector3.Distance(transform.position, t.transform.position), hit.point, gameObject.transform.position);
		                    }
	                    }
	                }
	            }
	            t.attachedRigidbody.AddExplosionForce(explosionForce, transform.position, radius);
	        }
        }

        [Server]
        private int CalcDamage(DamageType damageType, Transform target)
        {
	        var normalizedDistance = Mathf.Clamp01(Vector3.Distance(target.position, transform.position) / radius);
	        
	        var damage = damageType switch
	        {
		        DamageType.Vehicle => maxDamageToVehicle * (1 - normalizedDistance),
		        DamageType.Player => maxDamageToPlayer * (1 - normalizedDistance),
		        _ => 0
	        };
	        
	        return (int)damage;
        }
        
        [ClientRpc]
        private void RpcExplode()
        {
	        var position = transform.position;
	        var rotation = transform.rotation;

	        GameObject explosionGameObject = null;
	        
            switch (explosionSize)
            {
	            case ExplosionSize.Big:
		            explosionGameObject = Instantiate(_eL.bigExplosionPrefab, position, rotation);
		            break;
	            case ExplosionSize.Small:
		            explosionGameObject = Instantiate(_eL.smallExplosionPrefab, position, rotation);
		            break;
	            case ExplosionSize.Tiny:
		            explosionGameObject = Instantiate(_eL.tinyExplosionPrefab, position, rotation);
		            break;
            }

            var explosionGameObjectAudioSource = explosionGameObject!.GetComponent<AudioSource>();
            
            explosionGameObjectAudioSource.clip = _eL.audioClips[Random.Range(0, _eL.audioClips.Length)];
            explosionGameObjectAudioSource.Play();

            if(fireBehaviour != FireBehaviour.NoFire && Physics.Raycast(transform.position, Vector3.down, 0.15f))
            {
	            GameObject fireGameObject = null;
	            
	            switch (fireSize)
	            {
		            case FireSize.LargeFlames:
			            fireGameObject = Instantiate(_eL.largeFlamesPrefab, position, rotation);
			            break;
		            case FireSize.MediumFlames:
			            fireGameObject = Instantiate(_eL.mediumFlamesPrefab, position, rotation);
			            break;
		            case FireSize.TinyFlames:
			            fireGameObject = Instantiate(_eL.tinyFlamesPrefab, position, rotation);
			            break;
	            }

	            if (fireBehaviour == FireBehaviour.SpreadFire)
		            fireGameObject!.AddComponent<Fire>();
	            
	            else if (fireBehaviour == FireBehaviour.NoSpreadFire) 
		            fireGameObject!.AddComponent<Fire>();
            }

            foreach (var fragment in fragments)
	            Instantiate(fragment, position, rotation).GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, radius, upwardsModifier);
            
            NetworkServer.Destroy(gameObject);
            Destroy(gameObject);
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(255, 255, 0, 1);
            Gizmos.DrawWireSphere(transform.position, radius);
        }
        #endif
        
    }
}
