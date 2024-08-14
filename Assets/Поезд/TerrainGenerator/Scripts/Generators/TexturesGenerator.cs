using System;
using System.Collections.Generic;
using TerrainGenerator.Scripts.Abstract;
using UnityEngine;

namespace TerrainGenerator.Scripts.Generators
{
    public class TexturesGenerator : MonoBehaviour, IGenerator
    {
        
        public List<_Texture> textures = new();

        
        public void Generate()
        {
            if (textures == null)
                throw new NullReferenceException("Textures list not setted");

            var terrainData = GetComponent<Terrain>().terrainData;

            var splatPrototypes = new SplatPrototype[textures.Count];

            for (var i = 0; i < textures.Count; i++)
                splatPrototypes[i] = new SplatPrototype { texture = textures[i].Texture, tileSize = textures[i].TileSize };
            
            terrainData.splatPrototypes = splatPrototypes;

            if (terrainData.alphamapResolution != terrainData.size.x)
                Debug.LogError("terrainData.alphamapResolution must fit terrain size");

            var splatmaps = new float[terrainData.alphamapWidth, terrainData.alphamapHeight, terrainData.alphamapLayers];

            var terrainMaxHeight = terrainData.size.y;

            var x = 0;
            while (x < terrainData.alphamapHeight)
            {
                var y = 0;
                while (y < terrainData.alphamapWidth)
                {
                    var height = terrainData.GetHeight(x, y);
                    var heightScaled = height / terrainMaxHeight;

                    var xS = x / terrainData.heightmapResolution;
                    var yS = y / terrainData.heightmapResolution;

                    var steepness = terrainData.GetSteepness(xS, yS);
                    var angleScaled = steepness / 90;

                    for (var i = 0; i < terrainData.alphamapLayers; i++)
                    {
                        switch (textures[i].Type)
                        {
                            case 0:
                                if (i != 0)
                                {
                                    splatmaps[y, x, i] = textures[i].HeightCurve.Evaluate(heightScaled);
                                    for (var hi = 0; hi < i; hi++)
                                        splatmaps[y, x, hi] *= (splatmaps[y, x, i] - 1) / -1;
                                }
                                else
                                    splatmaps[y, x, i] = textures[i].HeightCurve.Evaluate(heightScaled);
                                break;
                            case 1:
                                splatmaps[y, x, i] = textures[i].AngleCurve.Evaluate(angleScaled);
                                for (var ai = 0; ai < i; ai++)
                                    splatmaps[y, x, ai] *= (splatmaps[y, x, i] - 1) / -1;
                                break;
                        }

                        if (splatmaps[y, x, i] > 1) { splatmaps[y, x, i] = 1; }
                    }
                    y++;
                }
                x++;
            }

            terrainData.SetAlphamaps(0, 0, splatmaps);
        }

        public void Clear()
        {
            textures = new List<_Texture>();
            Generate();
        }
    }

    public class _Texture
    {
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public Vector2 TileSize = new(1, 1);
        public int Type { get; set; }
        public AnimationCurve HeightCurve = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 1.0f);
        public AnimationCurve AngleCurve = AnimationCurve.Linear(0.0f, 1.0f, 1.0f, 1.0f);
    }
    
}
