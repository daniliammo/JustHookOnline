using TerrainGenerator.Scripts.Generators;
using UnityEditor;
using UnityEngine;

namespace TerrainGenerator.Scripts.Editor
{
    [CustomEditor(typeof(HeightsGenerator))]
    internal class HeightsEditor : UnityEditor.Editor
    {
        
        public override void OnInspectorGUI()
        {
            // var gen = (HeightsGenerator)target;
            //
            // if (DrawDefaultInspector())
            // {
            //     if (gen.AutoUpdate)
            //         gen.Generate();
            // }
            //
            // if (GUILayout.Button("Generate"))
            //     gen.Generate();
        }
        
    }
}
