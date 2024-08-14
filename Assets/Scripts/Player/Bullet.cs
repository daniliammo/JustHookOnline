using UnityEngine;

namespace Player
{
    public class Bullet : MonoBehaviour
    {

        public AudioSource bulletFX;
        public AudioClip[] gunShot;
        
        
        private void Start()
        {
            bulletFX.PlayOneShot(gunShot[Random.Range(0, gunShot.Length)]);
        }

    }
}
