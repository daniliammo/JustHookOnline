using Mirror;
using UnityEngine;

namespace Player
{
    public class Grenade : Explosion.Explosion
    {

        public float smokeLifeTime;
        
        public GrenadeType grenadeType;
        public float lifeTime;
        

        [Server]
        private void Start()
        {
            if(lifeTime <= 0)
            {
                Debug.LogWarning("Grenade lifetime is zero");
                return;
            }

            if (grenadeType != GrenadeType.Smoke || grenadeType != GrenadeType.Molotov)
                Invoke(nameof(CmdExplode), lifeTime);
        }

        [Server]
        private void OnCollisionEnter(Collision collision)
        {
            if (grenadeType == GrenadeType.Impact)
                CmdExplode();
        }

        [Command (requiresAuthority = false)]
        private void CmdSmoke()
        {
            RpcSmoke();
        }

        [ClientRpc]
        private void RpcSmoke()
        {
            
        }
        
    }
}
