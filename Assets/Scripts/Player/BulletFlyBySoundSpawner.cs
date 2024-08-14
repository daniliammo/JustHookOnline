using Mirror;
using UnityEngine;

namespace Player
{
    public class BulletFlyBySoundSpawner : NetworkBehaviour
    {

        public GameObject[] bulletFlyBySoundsPrefabs;
        
        
        [Command (requiresAuthority = false)]
        public void CmdSpawnBulletFlyBySound(Vector3 position, Quaternion rotation)
        {
            var prefab = Instantiate(bulletFlyBySoundsPrefabs[Random.Range(0, bulletFlyBySoundsPrefabs.Length)], position, rotation);
            NetworkServer.Spawn(prefab);
        }
        
    }
}
