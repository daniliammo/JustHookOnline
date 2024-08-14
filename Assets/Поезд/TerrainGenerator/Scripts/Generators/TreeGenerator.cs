using System.Collections.Generic;
using TerrainGenerator.Scripts.Maps;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TerrainGenerator.Scripts.Generators
{
    public class TreeGenerator
    {
        
        private TerrainData TerrainData { get; }
        
        private uint Octaves { get; }
        
        private uint Scale { get; }
        
        private float Lacunarity { get; }

        private float Persistence { get; }
        
        private uint Offset { get; }
        
        private float MinLevel { get; }
        
        private float MaxLevel { get; }
        
        private float MaxSteepness { get; }
        
        private float IslandsSize { get; }
        
        private float Density { get; }

        private TreeInstance[] TreeInstances { get; set; }

        private readonly uint _alphaMapWidth;
        private readonly uint _alphaMapHeight;
        

        public TreeGenerator(TerrainData terrainData, uint offset, uint octaves, uint scale, float density, float islandsSize, float lacunarity, float persistence, float minLevel, float maxLevel, float maxSteepness)
        {
            TerrainData = terrainData;
            _alphaMapWidth = (uint)TerrainData.alphamapWidth;
            _alphaMapHeight = (uint)TerrainData.alphamapHeight;
            Octaves = octaves;
            Offset = offset;
            Scale = scale;
            Density = density;
            IslandsSize = islandsSize;
            Lacunarity = lacunarity;
            Persistence = persistence;
            MinLevel = minLevel;
            MaxLevel = maxLevel;
            MaxSteepness = maxSteepness;
        }
        
        public void Generate()
        {
            var treePosition = new List<Vector3>();

            var noiseMap = new PerlinMap
            {
                Size = _alphaMapWidth,
                Octaves = Octaves,
                Scale = Scale,
                Offset = Offset,
                Persistence = Persistence,
                Lacunarity = Lacunarity
            }.Generate(out _, out _);

            for (var x = 0; x < _alphaMapWidth; x++)
            {
                for (var y = 0; y < TerrainData.alphamapHeight; y++)
                {
                    var height = TerrainData.GetHeight(x, y);
                    var heightScaled = height / TerrainData.size.y;
                    var xScaled = (x + Random.Range(-1f, 1f)) / _alphaMapWidth;
                    var yScaled = (y + Random.Range(-1f, 1f)) / _alphaMapHeight;
                    var steepness = TerrainData.GetSteepness(xScaled, yScaled);
                    
                    var noiseStep = Random.Range(0f, 1f);
                    var noiseVal = noiseMap[x, y];

                    if
                    (
                        noiseStep < Density &&
                        noiseVal < IslandsSize &&
                        steepness < MaxSteepness &&
                        height > MinLevel &&
                        height < MaxLevel
                    )
                        treePosition.Add(new Vector3(xScaled, heightScaled, yScaled));
                }
            }

            TreeInstances = new TreeInstance[treePosition.Count];

            for (var ii = 0; ii < TreeInstances.Length; ii++)
            {
                TreeInstances[ii].position = treePosition[ii];
                TreeInstances[ii].prototypeIndex = Random.Range(0, TerrainData.treePrototypes.Length);
                TreeInstances[ii].color = new Color(Random.Range(100, 255), Random.Range(100, 255), Random.Range(100, 255));
                TreeInstances[ii].lightmapColor = Color.white;
                TreeInstances[ii].heightScale = 1.0f + Random.Range(-0.25f, 0.5f);
                TreeInstances[ii].widthScale = 1.0f + Random.Range(-0.5f, 0.25f);
            }
            
            Debug.Log(TreeInstances.Length + " trees were created");
        }

        public void SetTreeInstancesFromMainThread()
        {
            TerrainData.treeInstances = TreeInstances;
        }
        
    }
}
