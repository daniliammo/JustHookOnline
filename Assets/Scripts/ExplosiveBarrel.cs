using Mirror;
using UnityEngine;


public class ExplosiveBarrel : Explosion.Explosion
{
	
	[SyncVar]
	public int hp = 100;
	[SyncVar]
	private bool _isShooted;
	
	[Header("Префабы")]
	public GameObject flameStream;

	
	// ReSharper disable Unity.PerformanceAnalysis
	[Command (requiresAuthority = false)]
	public void CmdShooted(int damage, Vector3 hitPoint, Vector3 rotation)
	{
		hp -= damage;
		if (_isShooted) return;
		_isShooted = true;
		RpcSpawnFlameStream(hitPoint, rotation);
		InvokeRepeating(nameof(StartBurning), 0, 0.4f);
	}

	private void StartBurning()
	{
		hp -= 25;
		
		if (hp > 0) return;
		CmdExplode();
	}

	[ClientRpc]
	private void RpcSpawnFlameStream(Vector3 hitPoint, Vector3 rotation)
	{
		var prefab = Instantiate(flameStream, hitPoint, Quaternion.identity);
		NetworkServer.Spawn(prefab);
		prefab.transform.LookAt(rotation);
		prefab.transform.SetParent(transform);
		GetComponent<AudioSource>().Play();
	}
	
}
