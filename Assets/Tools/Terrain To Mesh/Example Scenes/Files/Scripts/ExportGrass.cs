using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AmazingAssets.TerrainToMesh.Example
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class ExportGrass : MonoBehaviour
    {
        public TerrainData terrainData;

        public int vertexCountHorizontal = 100;
        public int vertexCountVertical = 100;

        private void Start()
        {
            if (terrainData == null)
                return;


            //1. Export mesh from terrain////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            var terrainMesh = terrainData.TerrainToMesh().ExportMesh(vertexCountHorizontal, vertexCountVertical, TerrainToMesh.Normal.CalculateFromMesh);

            GetComponent<MeshFilter>().sharedMesh = terrainMesh;

            


            //2. Create material////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            var shaderName = Utilities.GetUnityDefaultShader();  //Default shader based on used render pipeline

            var material = new Material(Shader.Find(shaderName));

            GetComponent<Renderer>().sharedMaterial = material;




            //3. Export grass//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            var detailPrototypesData = terrainData.TerrainToMesh().ExportGrassData(vertexCountHorizontal, vertexCountVertical, 1, 1, 8, 1);

            
            var grassMesh = Utilities.CreateGrassMesh(1);     //Create grass quad mesh


            var grassShader = Shader.Find(Constants.shaderGrass);          //Simple Mobile/Diffuse shader with Alpha cutout
            var mainTexturePropName = Utilities.GetMaterailPropMainTex();  //_MainTex property name inside shader. 


            //Instantiate grass objects
            for (var t = 0; t < detailPrototypesData.Length; t++)
            {
                
                var grassParent = new GameObject("Grass");   //Used as parent for all current type of grass objects
                grassParent.transform.SetParent(this.gameObject.transform, false);


                //Each type of grass needs its own material with texture from prototype
                var grassMaterial = new Material(grassShader);
                grassMaterial.SetTexture(mainTexturePropName, detailPrototypesData[t].detailPrototype.prototypeTexture);
                
                
                for (var p = 0; p < detailPrototypesData[t].position.Count; p++)
                {
                    var grass = new GameObject("Grass");

                    //Use quad mesh for grass
                    grass.AddComponent<MeshFilter>().sharedMesh = grassMesh;

                    //Assign material
                    grass.AddComponent<MeshRenderer>().sharedMaterial = grassMaterial;



                    //Assign parent
                    grass.transform.SetParent(grassParent.transform, true);


                    //Position
                    grass.transform.localPosition = detailPrototypesData[t].position[p];

                    //Add random rotation
                    grass.transform.localRotation = Quaternion.Euler(0, Random.value * 360, 0);

                    //Scale
                    grass.transform.localScale = detailPrototypesData[t].scale[p];
                }


                //After all grass objects of the current type are created, it is better to combine them into one mesh
                var combinedMeshes = Utilities.CombineGameObjects(grassParent, grassMaterial, "Grass", detailPrototypesData[t].detailPrototype.prototypeTexture.name, UnityEngine.Rendering.IndexFormat.UInt16);


                //Bake grass Healthy and Dry color inside vertex color
                foreach (var mesh in combinedMeshes)
                {
                    BakeHealthyAndDryColorsInsideVertexColor(mesh, detailPrototypesData[t].detailPrototype);
                }
            }            
        }

        private void BakeHealthyAndDryColorsInsideVertexColor(Mesh mesh, DetailPrototype detailPrototype)
        {

            var vertexPosition = mesh.vertices;
            var colors = mesh.colors;  //Mesh already contains 'Alpha' value for Top and Bottom position


            var healthyColor = detailPrototype.healthyColor;
            healthyColor.a = 1;

            var dryColor = detailPrototype.dryColor;
            dryColor.a = 1;


            for (var i = 0; i < vertexPosition.Length; i++)
            {
                var pNoise = Mathf.PerlinNoise(vertexPosition[i].x * detailPrototype.noiseSpread, vertexPosition[i].z * detailPrototype.noiseSpread);

                //Multiply with existing color
                colors[i] *= Color.Lerp(dryColor, healthyColor, pNoise);
            }


            mesh.colors = colors;
        }
    }
}
