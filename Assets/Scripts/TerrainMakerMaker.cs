using UnityEngine;
using Utils;


public class TerrainMakerMaker : MonoBehaviour
{
    public GameObject terrainGameObject;

    public Vector3Int terrainSize;
    
    public Material terrainMaterial;

    public float treeDensity;
    
    public Texture2D[] terrainLayersDiffuseTextures;
    public Vector2[] diffuseTextureSize;
    public Texture2D[] terrainLayersTexturesNormals;
    public float[] normalMapScale;
    
    public GameObject[] treePrototypesGameObjects;
    
    public GameObject[] grassPrototypesGameObjects;
    public float[] grassDensity;
    
    [Header("Настройки Генераторов")]
    [Header("Heights Generator")]
    public AnimationCurve heightCurve;
    public uint falloffRange;
    

    public void Start()
    {
        for (var i = 0; i < 1; i++)
        {
            // var x = Random.Range(8, 256);
            var terrainMaker = new TerrainMaker(new GameObject(),
                new Vector3Int(255, 12 , 255),
                terrainMaterial,
                treeDensity,
                TerrainUtils.CreateTerrainLayers(terrainLayersDiffuseTextures,
                    diffuseTextureSize,
                    terrainLayersTexturesNormals,
                    normalMapScale),
                TerrainUtils.MakeTreePrototypes(treePrototypesGameObjects),
                TerrainUtils.MakeDetailPrototypes(grassPrototypesGameObjects,
                    grassDensity),
                heightCurve, falloffRange);
            terrainMaker.MakeTerrain();
        }

    }
    
}
