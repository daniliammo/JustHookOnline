using System.Diagnostics.Contracts;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class TerrainUtils
    {

        [Pure]
        public static TerrainLayer[] CreateTerrainLayers(Texture2D[] diffuseTextures, Vector2[] diffuseTextureSize, Texture2D[] normalMaps, float[] normalMapScale, Texture2D[] maskMaps)
        {
            var terrainLayers = diffuseTextures.Select(_ => new TerrainLayer()).ToList();

            for (var index = 0; index < diffuseTextures.Length; index++)
            {
                terrainLayers[index].normalScale = normalMapScale[index];
                terrainLayers[index].tileSize = diffuseTextureSize[index];
            }
        
            for (var index = 0; index < diffuseTextures.Length; index++)
            {
                terrainLayers[index].name = "Terrain Layer " + index;
                terrainLayers[index].diffuseTexture = diffuseTextures[index];
            }

            for (var index = 0; index < normalMaps.Length; index++)
                terrainLayers[index].normalMapTexture = normalMaps[index];

            for (var index = 0; index < maskMaps.Length; index++)
                terrainLayers[index].maskMapTexture = maskMaps[index];
            
            return terrainLayers.ToArray();
        }

        [Pure]
        public static TerrainLayer[] CreateTerrainLayers(Texture2D[] diffuseTextures, Vector2[] diffuseTextureSize, Texture2D[] normalMaps, float[] normalMapScale)
        {
            var terrainLayers = diffuseTextures.Select(_ => new TerrainLayer()).ToList();

            for (var index = 0; index < diffuseTextures.Length; index++)
            {
                terrainLayers[index].normalScale = normalMapScale[index];
                terrainLayers[index].tileSize = diffuseTextureSize[index];
            }
        
            for (var index = 0; index < diffuseTextures.Length; index++)
            {
                terrainLayers[index].name = "Terrain Layer " + index;
                terrainLayers[index].diffuseTexture = diffuseTextures[index];
            }

            for (var index = 0; index < normalMaps.Length; index++)
                terrainLayers[index].normalMapTexture = normalMaps[index];
            
            return terrainLayers.ToArray();
        }
        
        [Pure]
        public static TerrainLayer[] CreateTerrainLayers(Texture2D[] diffuseTextures, Vector2[] diffuseTextureSize)
        {
            var terrainLayers = diffuseTextures.Select(_ => new TerrainLayer()).ToList();

            for (var index = 0; index < diffuseTextures.Length; index++)
                terrainLayers[index].tileSize = diffuseTextureSize[index];
        
            for (var index = 0; index < diffuseTextures.Length; index++)
            {
                terrainLayers[index].name = "Terrain Layer " + index;
                terrainLayers[index].diffuseTexture = diffuseTextures[index];
            }
            
            return terrainLayers.ToArray();
        }
        
        [Pure]
        public static TreePrototype[] MakeTreePrototypes(GameObject[] treesGameObjects)
        {
            var treePrototypes = treesGameObjects.Select(_ => new TreePrototype()).ToList();
            
            for (var index = 0; index < treesGameObjects.Length; index++)
                treePrototypes[index].prefab = treesGameObjects[index];
            
            return treePrototypes.ToArray();
        }
        
        [Pure]
        public static DetailPrototype[] MakeDetailPrototypes(GameObject[] grassGameObjects, float[] grassDensity)
        {
            var detailPrototypes = grassGameObjects.Select(_ => new DetailPrototype()).ToList();
            
            for (var index = 0; index < grassGameObjects.Length; index++)
            {
                detailPrototypes[index].useInstancing = true;
                detailPrototypes[index].useDensityScaling = true;
                detailPrototypes[index].density = grassDensity[index];
                detailPrototypes[index].noiseSeed = Random.Range(777, 7777);
                detailPrototypes[index].usePrototypeMesh = true;
                detailPrototypes[index].renderMode = DetailRenderMode.VertexLit;
                detailPrototypes[index].prototype = grassGameObjects[index];
                detailPrototypes[index].prototype.isStatic = true;
            }
            
            return detailPrototypes.ToArray();
        }
        
    }
}
