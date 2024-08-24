using UnityEngine;
using Utils;

namespace Cars
{
    public class RandomCarSpawner : MonoBehaviour
    {

        public int spawnChance;
        
        public GameObject[] carPrefabs;
        private ParkingPlace[] _parkingPlaces;
        public byte maxVehicles;


        private void Start()
        {
            _parkingPlaces = FindObjectsOfType<ParkingPlace>();
            SpawnCars();
            // DestroyAll();
        }

        private void SpawnCars()
        {
            carPrefabs.Shuffle();
            _parkingPlaces.Shuffle();

            for (var i = 0; i < _parkingPlaces.Length; i++)
            {
                var parkingPlace = _parkingPlaces[i];
                
                if (parkingPlace.isEmployed)
                    continue;

                if (!RandomBoolean.GetRandomBoolean(spawnChance))
                    continue;

                if(i > maxVehicles)
                    return;
                
                var carPrefab = carPrefabs[Random.Range(0, carPrefabs.Length)];

                Instantiate(carPrefab, parkingPlace.transform.position, parkingPlace.transform.rotation);
                parkingPlace.isEmployed = true;
            }
        }

        private void DestroyAll()
        {
            foreach (var parkingPlace in _parkingPlaces)
                Destroy(parkingPlace);
            
            Destroy(this);
        }
        
    }
}
