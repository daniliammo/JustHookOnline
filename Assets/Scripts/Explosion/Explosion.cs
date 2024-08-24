using Mirror;
using UnityEngine;

namespace Explosion
{
    public class Explosion : NetworkBehaviour
    {

        public ExplosionSize explosionSize;

        public FireSize fireSize;
        public FireBehaviour fireBehaviour;

        public byte radius;
        public byte explosionForce;
        public float upwardsModifier;
        public byte maxDamageToPlayer;
        public byte maxDamageVehicle;

        public GameObject[] fragments;
        
        private ExplosionLinks _eL;
	    

        private void Start()
        {
	        _eL = FindObjectOfType<ExplosionLinks>();
        }

        [Command (requiresAuthority = false)]
        public void CmdExplode()
        {
            RpcExplode();
            
	        var colliders = Physics.OverlapSphere(transform.position, radius);
	        foreach (var t in colliders)
	        {
		        if (t.CompareTag("Glass"))
			        t.GetComponent<BreakableWindow>().RpcBreakWindow();
		        
	            if (t.CompareTag("Player"))
	            {
	                // Пуляем райкаст для того чтобы проверить игрок за стеной или нет. Чтобы не получилось так что взрыв убивает игрока через стенку
	                if (Physics.Raycast(transform.position, t.transform.position + t.transform.up / 2 - transform.position,
	                        out var hit, radius, Physics.DefaultRaycastLayers,
	                        QueryTriggerInteraction.Ignore))
	                {
		                if(hit.collider.CompareTag("Player"))
		                {
			                var normalizedDistance = Mathf.Clamp01(Vector3.Distance(t.transform.position, transform.position) / radius);
			                
			                var damage = maxDamageToPlayer * (1 - normalizedDistance);

			                var player = t.GetComponent<Player.Player>();    
	                
			                player.CmdChangeHp((byte)damage, transform, "Взрывная бочка");
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
            
            explosionGameObjectAudioSource!.GetComponent<AudioSource>().clip = _eL.audioClips[Random.Range(0, _eL.audioClips.Length)];
            explosionGameObjectAudioSource!.GetComponent<AudioSource>().Play();

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
		            fireGameObject.AddComponent<Fire>();
	            
	            else if (fireBehaviour == FireBehaviour.NoSpreadFire) 
		            fireGameObject.AddComponent<Fire>();
            }

            foreach (var t in fragments)
	            Instantiate(t, position, rotation).GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, radius, upwardsModifier);
            
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
