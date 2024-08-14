using TerrainGenerator.Scripts.Maps;
using UnityEngine;

namespace TerrainGenerator.Scripts.Generators
{
    public class GrassGenerator
    {
        
        private TerrainData TerrainData { get; }
        
        private uint Octaves { get; }
        
        private float Scale { get; }
        
        private float Lacunarity { get; }

        private float Persistence { get; }
        
        private uint Offset { get; }
        
        private float MinLevel { get; }
        
        private float MaxLevel { get; }

        private float MaxSteepness { get; }

        private float IslandsSize { get; }

        private int Density { get; }

        private int _index;

        private int[,] _detailLayer;
        

        public GrassGenerator(TerrainData terrainData, float islandsSize, uint octaves, float scale, uint offset, float persistence, float maxSteepness, float minLevel, float maxLevel, float lacunarity, int density)
        {
            TerrainData = terrainData;
            IslandsSize = islandsSize;
            Octaves = octaves;
            Scale = scale;
            Offset = offset;
            Persistence = persistence;
            MaxLevel = maxLevel;
            MaxSteepness = maxSteepness;
            MinLevel = minLevel;
            Lacunarity = lacunarity;
            Density = density;
        }
        
        
        public void Generate()
        {
            var noiseMap = new PerlinMap
            {
                Size = (uint)TerrainData.detailWidth,
                Octaves = Octaves,
                Scale = Scale,
                Offset = Offset,
                Persistence = Persistence,
                Lacunarity = Lacunarity
            }.Generate();

            for (_index = 0; _index < TerrainData.detailPrototypes.Length; _index++)
            {
                _detailLayer = TerrainData.GetDetailLayer(0, 0, TerrainData.detailWidth, TerrainData.detailHeight, _index);

                for (var x = 0; x < TerrainData.alphamapWidth; x++)
                {
                    for (var y = 0; y < TerrainData.alphamapHeight; y++)
                    {
                        var height = TerrainData.GetHeight(x, y);
                        var xScaled = (x + Random.Range(-1f, 1f)) / TerrainData.alphamapWidth;
                        var yScaled = (y + Random.Range(-1f, 1f)) / TerrainData.alphamapHeight;
                        var steepness = TerrainData.GetSteepness(xScaled, yScaled);

                        if (noiseMap[x, y] < IslandsSize && steepness < MaxSteepness && height > MinLevel && height < MaxLevel)
                            _detailLayer[x, y] = Density;
                        
                        else
                            _detailLayer[x, y] = 0;
                    }
                }
            }
        }

        public void SetDetailLayerFromMainThread()
        {
            TerrainData.SetDetailLayer(0, 0, _index, _detailLayer);
        }
        
    }
}
