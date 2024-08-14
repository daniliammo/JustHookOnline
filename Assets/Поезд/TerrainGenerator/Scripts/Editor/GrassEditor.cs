using TerrainGenerator.Scripts.Generators;
using UnityEditor;
using UnityEngine;

namespace TerrainGenerator.Scripts.Editor
{
    [CustomEditor(typeof(GrassGenerator))]
    internal class GrassEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            // var gen = (GrassGenerator)target;
            //
            // if (DrawDefaultInspector())
            // {
            //     if (gen.AutoUpdate)
            //         gen.Generate();
            // }
            //
            // if (GUILayout.Button("Generate"))
            //     gen.Generate();
            //
            // if (GUILayout.Button("Clear"))
            //     gen.Clear();
        }
    }
}
