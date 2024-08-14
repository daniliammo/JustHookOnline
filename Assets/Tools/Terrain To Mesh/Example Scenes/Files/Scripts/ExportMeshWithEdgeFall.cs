using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AmazingAssets.TerrainToMesh.Example
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class ExportMeshWithEdgeFall : MonoBehaviour
    {
        public TerrainData terrainData;

        public int vertexCountHorizontal = 100;
        public int vertexCountVertical = 100;

        public EdgeFall edgeFall = new EdgeFall(0, true);

        public Texture2D edgeFallTexture;


        private void Start()
        {
            if (terrainData == null)
                return;


            //1. Export mesh with edge fall/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            var terrainMesh = terrainData.TerrainToMesh().ExportMesh(vertexCountHorizontal, vertexCountVertical, TerrainToMesh.Normal.CalculateFromMesh, edgeFall);

            GetComponent<MeshFilter>().sharedMesh = terrainMesh;




            //2. Create materials////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
           
            var shaderName = Utilities.GetUnityDefaultShader();                   //Default shader based on used render pipeline
            var mainTexturePropName = Utilities.GetMaterailPropMainTex();         //_MainTex property name inside shader. 

          
            var meshMaterial = new Material(Shader.Find(shaderName));      //Material for main mesh 

            var edgeFallMaterial = new Material(Shader.Find(shaderName));  //Material for edge fall (saved in sub-mesh)
            edgeFallMaterial.SetTexture(mainTexturePropName, edgeFallTexture);


            GetComponent<Renderer>().sharedMaterials = new[] { meshMaterial, edgeFallMaterial };
        }
    }
}
