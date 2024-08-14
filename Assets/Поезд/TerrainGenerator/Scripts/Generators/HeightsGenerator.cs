using TerrainGenerator.Scripts.Maps;
using UnityEngine;

namespace TerrainGenerator.Scripts.Generators
{
    public class HeightsGenerator
    {
        
        private uint Width { get; }
        
        private uint Height { get; }
        
        private TerrainData TerrainData { get; }
        
        private uint Octaves { get; }
        
        private uint Scale { get; }
        
        private float Lacunarity { get; }
        
        private float Persistence { get; }
        
        private AnimationCurve HeightCurve { get; }
        
        private uint Offset { get; }
        
        private float FalloffDirection { get; }
        
        private uint FalloffRange { get; }

        private float[,] _noiseMap;
        

        public HeightsGenerator(TerrainData terrainData, uint offset, uint octaves, uint scale, AnimationCurve heightCurve, uint falloffRange, float falloffDirection, float persistence, float lacunarity)
        {
            TerrainData = terrainData;
            Scale = scale;
            Octaves = octaves;
            HeightCurve = heightCurve;
            Offset = offset;
            FalloffRange = falloffRange;
            FalloffDirection = falloffDirection;
            Persistence = persistence;
            Lacunarity = lacunarity;
            
            Width = (uint)TerrainData.size.x;
            Height = (uint)TerrainData.size.z;
        }

        public void Generate()
        {
            float[,] falloff = null;
            if (FalloffRange > 0)
            {
                falloff = new FalloffMap
                {
                    FalloffDirection = FalloffDirection,
                    FalloffRange = FalloffRange,
                    Size = Width
                }.Generate();
            }
            
            var heightCurve = new AnimationCurve(HeightCurve.keys);

            _noiseMap = new PerlinMap
            {
                Size = Width,
                Octaves = Octaves,
                Scale = Scale,
                Offset = Offset,
                Persistence = Persistence,
                Lacunarity = Lacunarity
            }.Generate(out var maxLocalNoiseHeight, out var minLocalNoiseHeight);

            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    var lerp = Mathf.InverseLerp(minLocalNoiseHeight, maxLocalNoiseHeight, _noiseMap[x, y]);

                    if (falloff != null)
                        lerp -= falloff[x, y];

                    if (lerp >= 0)
                        _noiseMap[x, y] = heightCurve.Evaluate(lerp);
                    else
                        _noiseMap[x, y] = 0;
                }
            }
        }

        public void SetHeightsFromMainThread()
        {
            TerrainData.SetHeights(0, 0, _noiseMap);
        }
        
    }
}
