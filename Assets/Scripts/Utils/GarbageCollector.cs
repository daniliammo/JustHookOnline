using UnityEngine;

namespace Utils
{
    public class GarbageCollector : MonoBehaviour
    {

        private readonly string[] _garbageTags = {"ExplosiveBarrelFragments", "Garbage"};
        private GameObject[] _gameObjectsToDestroy;
    
    
        private void Start()
        {
            InvokeRepeating(nameof(GarbageCollect), 30, 30);
        }

        private void GarbageCollect()
        {
            foreach (var t in _garbageTags)
            {
                _gameObjectsToDestroy = GameObject.FindGameObjectsWithTag(t);
                foreach (var i in _gameObjectsToDestroy)
                    Destroy(i);
                _gameObjectsToDestroy = null;
            }
        }
    }
}
