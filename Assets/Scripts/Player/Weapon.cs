using Mirror;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Player
{
    public class Weapon : NetworkBehaviour
    {

        private AudioSource _weaponFX;
        public AudioClip[] magazineOut;
        public AudioClip[] magazineIn;
        public GameObject magazinePrefab;
        public Transform magazinePrefabPosition;

        
        private void Start()
        {
            _weaponFX = GetComponent<AudioSource>();
        }

        public void OnMagazineOut()
        {
            _weaponFX.PlayOneShot(magazineOut[Random.Range(0, magazineOut.Length)]);
            Invoke(nameof(SpawnMagazinePrefab), 0.4f);
        }

        private void SpawnMagazinePrefab()
        {
            Instantiate(magazinePrefab, magazinePrefabPosition.position, Quaternion.identity).transform.SetParent(null);
        }
        
        public void OnMagazineIn()
        {
            _weaponFX.PlayOneShot(magazineIn[Random.Range(0, magazineIn.Length)]);
        }
        
    }
}
