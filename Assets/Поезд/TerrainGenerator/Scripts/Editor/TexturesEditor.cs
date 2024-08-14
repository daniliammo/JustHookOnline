using TerrainGenerator.Scripts.Generators;
using UnityEditor;
using UnityEngine;

namespace TerrainGenerator.Scripts.Editor
{
    [CustomEditor(typeof(TexturesGenerator))]
    internal class TexturesEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var gen = (TexturesGenerator)target;

            foreach (var t in gen.textures)
            {
                EditorGUILayout.BeginHorizontal();
                t.Texture = EditorGUILayout.ObjectField("Texture", t.Texture, typeof(Texture2D), false) as Texture2D;
                t.Type = EditorGUILayout.Popup(t.Type, new[] { "Height", "Angle" });

                EditorGUILayout.EndHorizontal();

                switch (t.Type)
                {
                    case 0:
                        t.HeightCurve = EditorGUILayout.CurveField("Height Curve", t.HeightCurve);
                        break;
                    case 1:
                        t.AngleCurve = EditorGUILayout.CurveField("Angle Curve", t.AngleCurve);
                        break;
                }

                t.TileSize = EditorGUILayout.Vector2Field("Tilesize", t.TileSize);
                EditorGUILayout.LabelField("");
            }

            if (gen.textures.Count > 0)
            {
                if (GUILayout.Button("Delete last"))
                {
                    gen.textures.RemoveAt(gen.textures.Count - 1);
                }
            }

            if (GUILayout.Button("Add"))
            {
                gen.textures.Add(new _Texture());
            }

            if (gen.textures.Count > 0)
            {
                if (GUILayout.Button("Generate"))
                {
                    gen.Generate();
                }
            }

            if (GUILayout.Button("Clear"))
            {
                gen.Clear();
            }
        }
    }
}
