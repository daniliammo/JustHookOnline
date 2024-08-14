using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AmazingAssets.TerrainToMesh.Example
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class ExportMesh : MonoBehaviour
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

            var shaderName = Utilities.GetUnityDefaultShader(); //Default shader based on used render pipeline

            var material = new Material(Shader.Find(shaderName));

            GetComponent<Renderer>().sharedMaterial = material;
        }
    }
}
