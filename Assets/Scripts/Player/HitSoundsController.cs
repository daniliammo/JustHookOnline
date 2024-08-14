using UnityEngine;

namespace Player
{
    public class HitSoundsController : MonoBehaviour
    {

        public AudioSource hitSoundsSfx;
        public AudioClip bell;
        public AudioClip[] hitBass;
        public AudioClip[] hitMarker;


        public void PlayHitBassSound()
        {
            hitSoundsSfx.PlayOneShot(hitBass[Random.Range(0, hitBass.Length)]);
        }
        
        public void PlayHitMarkerSound()
        {
            hitSoundsSfx.PlayOneShot(hitMarker[Random.Range(0, hitMarker.Length)]);
        }
        
        public void PlayBellSound()
        {
            hitSoundsSfx.PlayOneShot(bell);
        }
        
    }
}
