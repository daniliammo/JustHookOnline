using TerrainGenerator.Scripts.Abstract;
using UnityEngine;

namespace TerrainGenerator.Scripts.Maps
{
    public class FalloffMap : IMap
    {
        
        public float FalloffDirection;
        public float FalloffRange;
        public uint Size;

        public float[,] Generate()
        {
            var map = new float[Size, Size];

            for (var i = 0; i < Size; i++)
            {
                for (var j = 0; j < Size; j++)
                {
                    var x = i / (float)Size * 2 - 1;
                    var y = j / (float)Size * 2 - 1;

                    var value = Mathf.Max(Mathf.Abs(x), Mathf.Abs(y));
                    map[i, j] = Evaluate(value);
                }
            }

            return map;
        }

        private float Evaluate(float value)
        {
            return Mathf.Pow(value, FalloffDirection) / (Mathf.Pow(value, FalloffDirection) + Mathf.Pow(FalloffRange - FalloffRange * value, FalloffDirection));
        }
        
    }
}
