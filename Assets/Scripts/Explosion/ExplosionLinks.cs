using UnityEngine;

namespace Explosion
{
    public class ExplosionLinks : MonoBehaviour
    {
        public GameObject bigExplosionPrefab;
        public GameObject smallExplosionPrefab;
        public GameObject tinyExplosionPrefab;
        public GameObject dustExplosionPrefab;

        public GameObject largeFlamesPrefab;
        public GameObject mediumFlamesPrefab;
        public GameObject tinyFlamesPrefab;

        public AudioClip[] audioClips;
        public AudioSource impactGrenade;
    }
}
