using UnityEngine;

namespace Utils
{
    public class GarbageCollector : MonoBehaviour
    {

        private readonly string[] _garbageTags = {"ExplosiveBarrelFragments", "Garbage", "PhysicalBody"};
        private GameObject[] _gameObjectsToDestroy;
        public byte repeatRate;
    
    
        private void Start()
        {
            InvokeRepeating(nameof(GarbageCollect), repeatRate, repeatRate);
        }

        private void GarbageCollect()
        {
            foreach (var tag in _garbageTags)
            {
                _gameObjectsToDestroy = GameObject.FindGameObjectsWithTag(tag);
                
                foreach (var i in _gameObjectsToDestroy)
                    Destroy(i);
                
                _gameObjectsToDestroy = null;
            }
        }
    }
}
