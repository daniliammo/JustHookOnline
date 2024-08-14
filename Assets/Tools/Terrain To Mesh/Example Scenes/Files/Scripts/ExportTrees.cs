using UnityEngine;


namespace AmazingAssets.TerrainToMesh.Example
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class ExportTrees : MonoBehaviour
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
           
            var shaderName = Utilities.GetUnityDefaultShader();   //Default shader based on used render pipeline

            var material = new Material(Shader.Find(shaderName));

            GetComponent<Renderer>().sharedMaterial = material;




            //3. Export trees//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            var treePrototypesData = terrainData.TerrainToMesh().ExportTreeData(vertexCountHorizontal, vertexCountVertical, 1, 1);

                        
            for (var t = 0; t < treePrototypesData.Length; t++)
            {
                for (var p = 0; p < treePrototypesData[t].position.Count; p++)
                {
                    //Instantiate tree prefab
                    var tree = Instantiate(treePrototypesData[t].prefab);    

                    //Set position
                    tree.transform.position = treePrototypesData[t].position[p];

                    //Add random rotation
                    //tree.transform.rotation = Quaternion.Euler(0, Random.value * 360, 0);

                    //Scale
                    tree.transform.localScale = treePrototypesData[t].scale[p];


                    //Add parent
                    tree.transform.SetParent(this.gameObject.transform, false);
                }
            }
        }
    }
}
