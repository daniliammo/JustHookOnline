using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mirror.Examples.MultipleAdditiveScenes
{
    internal class Spawner
    {
        [ServerCallback]
        internal static void InitialSpawn(Scene scene)
        {
            for (var i = 0; i < 10; i++)
                SpawnReward(scene);
        }

        [ServerCallback]
        internal static void SpawnReward(Scene scene)
        {
            var spawnPosition = new Vector3(Random.Range(-19, 20), 1, Random.Range(-19, 20));
            var reward = Object.Instantiate(((MultiSceneNetManager)NetworkManager.singleton).rewardPrefab, spawnPosition, Quaternion.identity);
            SceneManager.MoveGameObjectToScene(reward, scene);
            NetworkServer.Spawn(reward);
        }
    }
}
