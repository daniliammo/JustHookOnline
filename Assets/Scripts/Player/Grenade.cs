using Mirror;

namespace Player
{
    public class Grenade : Explosion.Explosion
    {

        public float lifeTime;
        

        [Server]
        private void Start()
        {
            Invoke(nameof(CmdExplode), lifeTime);
        }
        
    }
}
