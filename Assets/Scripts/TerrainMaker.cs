using TerrainGenerator.Scripts.Generators;
using UnityEngine;
using UnityEngine.Rendering;


public class TerrainMaker
{
    
    private Material TerrainMaterial { get; }

    private uint FalloffRange { get; }
    
    private float TreeDensity { get; }
    
    private TerrainLayer[] TerrainLayers { get; }

    private Terrain Terrain { get; set; }
    
    private Vector3Int TerrainSize { get; }
    
    private GameObject TerrainGameObject { get; set; }
    
    private TerrainData TerrainData { get; set; }
    
    private TreePrototype[] TreePrototypes { get; }
    
    private DetailPrototype[] GrassPrototypes { get; }

    private AnimationCurve HeightCurve { get; }
    

    public TerrainMaker(GameObject terrainGameObject, Vector3Int terrainSize, Material terrainMaterial, float treeDensity, TerrainLayer[] terrainLayers, TreePrototype[] treePrototypes, DetailPrototype[] grassPrototypes, AnimationCurve heightCurve, uint falloffRange)
    {
        TerrainGameObject = terrainGameObject;
        TerrainSize = terrainSize;
        TerrainMaterial = terrainMaterial;
        TreeDensity = treeDensity;
        TerrainLayers = terrainLayers;
        TreePrototypes = treePrototypes;
        GrassPrototypes = grassPrototypes;
        HeightCurve = heightCurve;
        FalloffRange = falloffRange;
    }
    
    public void MakeTerrain()
    {
        if (!TerrainGameObject)
        {
            TerrainGameObject = new GameObject
            {
                transform =
                {
                    // position = position,
                    parent = TerrainGameObject.transform
                },
                name = "Generated Terrain",
                isStatic = true
            };
        }

        Terrain = TerrainGameObject.AddComponent<Terrain>();
        Terrain.terrainData = new TerrainData();
        Terrain.materialTemplate = TerrainMaterial;
        
        TerrainData = Terrain.terrainData;
        TerrainData.name = "Generated Terrain Data";
        
        SetTerrainPreferences();
        
        TerrainGameObject.AddComponent<TerrainCollider>().terrainData = TerrainData;

        TerrainData.treePrototypes = TreePrototypes;
        
        TerrainData.detailPrototypes = GrassPrototypes;
        
        TerrainData.terrainLayers = TerrainLayers;
        
        Generate();
        
        Terrain.drawHeightmap = true;
        Terrain.drawTreesAndFoliage = true;
    }
    
    private async void Generate()
    {
        var heightsGenerator = new HeightsGenerator(TerrainData,
            (uint)Random.Range(777, 7777777),
            4,
            50,
            HeightCurve,
            FalloffRange,
            3,
            0.5f,
            2);
        heightsGenerator.Generate();
        heightsGenerator.SetHeightsFromMainThread();
        
        if (TreePrototypes.Length > 0)
        {
            var treeGenerator = new TreeGenerator(TerrainData,
                (uint)Random.Range(777, 7777777),
                4,
                50,
                TreeDensity,
                1,
                2,
                0.5f,
                0,
                777,
                0);
            treeGenerator.Generate();
            treeGenerator.SetTreeInstancesFromMainThread();
        }

        if (GrassPrototypes.Length > 0)
        {
            var grassGenerator = new GrassGenerator(TerrainData,
                1,
                4,
                50,
                (uint)Random.Range(777, 7777777),
                0.5f,
                0,
                -1,
                777,
                0.5f,
                100);
            grassGenerator.Generate();
            grassGenerator.SetDetailLayerFromMainThread();
        }

        // if (terrainLayersDiffuseTextures.Length > 0)
        // {
        //     var texturesGenerator = terrainGameObject.AddComponent<TexturesGenerator>();
        //     texturesGenerator.Generate();
        // }
    }
    
    private void SetTerrainPreferences()
    {
        Terrain.shadowCastingMode = ShadowCastingMode.Off;
        Terrain.drawHeightmap = false;
        Terrain.drawTreesAndFoliage = false;
        Terrain.allowAutoConnect = false;
        Terrain.drawInstanced = true;
        Terrain.enableHeightmapRayTracing = false;
        Terrain.heightmapPixelError = 200;
        TerrainData.size = new Vector3Int(TerrainSize.x / 8, TerrainSize.y, TerrainSize.z / 8);
        TerrainData.heightmapResolution = 256;
        TerrainData.alphamapResolution = 256;
        TerrainData.SetDetailResolution(TerrainSize.x, 8);
        TerrainData.wavingGrassTint = Color.clear;
        TerrainData.wavingGrassSpeed = 0;
        TerrainData.wavingGrassStrength = 0;
        Terrain.Flush();
    }
    
}
