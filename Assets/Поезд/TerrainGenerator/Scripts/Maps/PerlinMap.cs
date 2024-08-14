using TerrainGenerator.Scripts.Abstract;
using UnityEngine;

namespace TerrainGenerator.Scripts.Maps
{
    public class PerlinMap : IMap
    {
        
        public uint Size { get; set; }

        public uint Octaves { get; set; }

        public float Scale { get; set; }
        
        public uint Offset { get; set; }
        
        public float Persistence { get; set; }
        
        public float Lacunarity { get; set; }

        
        public float[,] Generate()
        {
            return GenerateNoise(out _, out _);
        }

        public float[,] Generate(out float maxLocalNoiseHeight, out float minLocalNoiseHeight)
        {
            return GenerateNoise(out maxLocalNoiseHeight, out minLocalNoiseHeight);
        }

        private float[,] GenerateNoise(out float maxLocalNoiseHeight, out float minLocalNoiseHeight)
        {
            var noiseMap = new float[Size, Size];

            var octaveOffsets = new Vector2[Octaves];

            float amplitude = 1;

            for (var i = 0; i < Octaves; i++)
            {
                octaveOffsets[i] = new Vector2(Offset, Offset);

                amplitude *= Persistence;
            }

            if (Scale <= 0)
                Scale = 0.0001f;

            maxLocalNoiseHeight = float.MinValue;
            minLocalNoiseHeight = float.MaxValue;

            var halfSize = Size / 2f;

            for (var y = 0; y < Size; y++)
            {
                for (var x = 0; x < Size; x++)
                {
                    amplitude = 1;
                    float frequency = 1;
                    float noiseHeight = 0;

                    for (var i = 0; i < Octaves; i++)
                    {
                        var sampleX = (x - halfSize + octaveOffsets[i].x) / Scale * frequency;
                        var sampleY = (y - halfSize + octaveOffsets[i].y) / Scale * frequency;

                        var perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                        noiseHeight += perlinValue * amplitude;

                        amplitude *= Persistence;
                        frequency *= Lacunarity;
                    }

                    if (noiseHeight > maxLocalNoiseHeight)
                        maxLocalNoiseHeight = noiseHeight;
                    
                    else if (noiseHeight < minLocalNoiseHeight)
                        minLocalNoiseHeight = noiseHeight;
                    noiseMap[x, y] = noiseHeight;
                }
            }

            return noiseMap;
        }
        
    }
}
