Shader "Amazing Assets/Terrain To Mesh/Splatmap (Holes)" 
{
    Properties
    {

//[HideInInspector][CurvedWorldBendSettings] _CurvedWorldBendSettings("0|1|1", Vector) = (0, 0, 0, 0)


//Terrain To Mesh Properties/////////////////////////////////////////////////////////////////////////////////////////////////////////
[HideInInspector] [TerrainToMeshLayerCounter] _T2M_Layer_Count ("Layer Count", float) = 0		

[Space]
[HideInInspector] [NoScaleOffset] _T2M_SplatMap_0 ("Splat Map #10 (RGBA)", 2D) = "black" {}

[HideInInspector] _T2M_Layer_0_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_0_Diffuse ("Paint Map 1 (R)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_0_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_0_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_0_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_0_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_0_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_0_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_0_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_0_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_0_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_1_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_1_Diffuse ("Paint Map 1 (R)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_1_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_1_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_1_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_1_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_1_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_1_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_1_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_1_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_1_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_2_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_2_Diffuse ("Paint Map 2 (G)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_2_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_2_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_2_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_2_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_2_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_2_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_2_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_2_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_2_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_3_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_3_Diffuse ("Paint Map 3 (B)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_3_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_3_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_3_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_3_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_3_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_3_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_3_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_3_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_3_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)


[HideInInspector] [NoScaleOffset] _T2M_SplatMap_1 ("Splat Map #1 (RGBA)", 2D) = "black" {}

[HideInInspector] _T2M_Layer_4_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_4_Diffuse ("Paint Map 4 (A)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_4_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_4_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_4_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_4_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_4_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_4_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_4_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_4_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_4_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_5_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_5_Diffuse ("Paint Map 5 (R)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_5_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_5_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_5_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_5_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_5_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_5_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_5_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_5_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_5_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_6_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_6_Diffuse ("Paint Map 6 (G)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_6_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_6_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_6_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_6_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_6_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_6_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_6_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_6_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_6_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_7_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_7_Diffuse ("Paint Map 7 (B)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_7_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_7_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_7_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_7_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_7_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_7_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_7_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_7_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_7_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)


[HideInInspector] [NoScaleOffset] _T2M_SplatMap_2 ("Splat Map #2 (RGBA)", 2D) = "black" {}

[HideInInspector] _T2M_Layer_8_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_8_Diffuse ("Paint Map 8 (A)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_8_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_8_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_8_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_8_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_8_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_8_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_8_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_8_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_8_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)
			
[HideInInspector] _T2M_Layer_9_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_9_Diffuse ("Paint Map 9 (R)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_9_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_9_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_9_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_9_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_9_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_9_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_9_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_9_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_9_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_10_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_10_Diffuse ("Paint Map 10 (G)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_10_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_10_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_10_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_10_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_10_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_10_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_10_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_10_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_10_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_11_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_11_Diffuse ("Paint Map 11 (B)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_11_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_11_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_11_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_11_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_11_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_11_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_11_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_11_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_11_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)


[HideInInspector] [NoScaleOffset] _T2M_SplatMap_3 ("Splat Map #3 (RGBA)", 2D) = "black" {}

[HideInInspector] _T2M_Layer_12_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_12_Diffuse ("Paint Map 12 (A)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_12_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_12_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_12_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_12_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_12_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_12_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_12_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_12_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_12_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)
			
[HideInInspector] _T2M_Layer_13_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_13_Diffuse ("Paint Map 13 (R)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_13_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_13_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_13_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_13_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_13_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_13_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_13_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_13_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_13_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_14_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_14_Diffuse ("Paint Map 14 (G)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_14_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_14_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_14_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_14_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_14_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_14_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_14_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_14_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_14_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)

[HideInInspector] _T2M_Layer_15_ColorTint ("Color Tint", Color) = (1, 1, 1, 1)
[HideInInspector] [NoScaleOffset] _T2M_Layer_15_Diffuse ("Paint Map 15 (B)", 2D) = "white" {}
[HideInInspector] _T2M_Layer_15_NormalScale("Strength", float) = 1
[HideInInspector] [NoScaleOffset] _T2M_Layer_15_NormalMap("Bump", 2D) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_Layer_15_Mask ("Mask", 2D) = "white" {}
[HideInInspector] _T2M_Layer_15_uvScaleOffset("UV Scale", Vector) = (1, 1, 0, 0)
[HideInInspector] _T2M_Layer_15_MapsUsage("Maps Usage", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_15_MetallicOcclusionSmoothness("Metallic (R), Occlusion (G), Smoothness(A)", Vector) = (0, 1, 0, 0)
[HideInInspector] _T2M_Layer_15_SmoothnessFromDiffuseAlpha("", float) = 0
[HideInInspector] _T2M_Layer_15_MaskMapRemapMin("Maskmap Remap Min", Vector) = (0, 0, 0, 0)
[HideInInspector] _T2M_Layer_15_MaskMapRemapMax("Maskmap Remap Max", Vector) = (1, 1, 1, 1)


//Texture 2D Array
[HideInInspector] [NoScaleOffset] _T2M_SplatMaps2DArray("SplatMaps 2D Array", 2DArray) = "black" {}
[HideInInspector] [NoScaleOffset] _T2M_DiffuseMaps2DArray("DiffuseMaps 2D Array", 2DArray) = "white" {}
[HideInInspector] [NoScaleOffset] _T2M_NormalMaps2DArray("NormalMaps 2D Array", 2DArray) = "bump" {}
[HideInInspector] [NoScaleOffset] _T2M_MaskMaps2DArray("MaskMaps 2D Array", 2DArray) = "white" {}	 		 
		 
//Holesmap
[HideInInspector] [NoScaleOffset] _T2M_HolesMap ("Holes Map", 2D) = "white" {}

//Fallback use only
[HideInInspector] _Color("Color", Color) = (1, 1, 1, 1)								//Not used
[HideInInspector] [NoScaleOffset] _MainTex("Fallback Diffuse", 2D) = "white" {}		
[HideInInspector] [NoScaleOffset] _BumpMap("Fallback Normal", 2D) = "bump" {}

/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }


    //Main subshader
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        
	// ------------------------------------------------------------
	// Surface shader code generated out of a CGPROGRAM block:
	

	// ---- forward rendering base pass:
	Pass {
		Name "FORWARD"
		Tags { "LightMode" = "ForwardBase" }

CGPROGRAM
// compile directives
#pragma vertex vert_surf
#pragma fragment frag_surf
#pragma target 3.5
#pragma multi_compile_instancing
#pragma multi_compile_fog
#pragma multi_compile_fwdbase
#include "HLSLSupport.cginc"
#define UNITY_INSTANCED_LOD_FADE
#define UNITY_INSTANCED_SH
#define UNITY_INSTANCED_LIGHTMAPSTS
#include "UnityShaderVariables.cginc"
#include "UnityShaderUtilities.cginc"

#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "UnityPBSLighting.cginc"
#include "AutoLight.cginc"

#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
#define WorldNormalVector(data,normal) fixed3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))



        struct Input
        {
			float2 texcoord;
        };



//Curved World
//#define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE
//#define CURVEDWORLD_BEND_ID_1
//#pragma shader_feature_local CURVEDWORLD_DISABLED_ON
//#pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON
//#include "Assets/Amazing Assets/Curved World/Shaders/Core/CurvedWorldTransform.cginc"


#define _ALPHATEST_ON 

#pragma shader_feature_local _ _T2M_TEXTURE_SAMPLE_TYPE_ARRAY

#pragma shader_feature_local _ _T2M_LAYER_COUNT_3 _T2M_LAYER_COUNT_4 _T2M_LAYER_COUNT_5 _T2M_LAYER_COUNT_6 _T2M_LAYER_COUNT_7 _T2M_LAYER_COUNT_8 _T2M_LAYER_COUNT_9 _T2M_LAYER_COUNT_10 _T2M_LAYER_COUNT_11 _T2M_LAYER_COUNT_12 _T2M_LAYER_COUNT_13 _T2M_LAYER_COUNT_14 _T2M_LAYER_COUNT_15 _T2M_LAYER_COUNT_16
		
#pragma shader_feature_local _T2M_LAYER_0_NORMAL
#pragma shader_feature_local _T2M_LAYER_1_NORMAL
#pragma shader_feature_local _T2M_LAYER_2_NORMAL
#pragma shader_feature_local _T2M_LAYER_3_NORMAL
#pragma shader_feature_local _T2M_LAYER_4_NORMAL
#pragma shader_feature_local _T2M_LAYER_5_NORMAL
#pragma shader_feature_local _T2M_LAYER_6_NORMAL
#pragma shader_feature_local _T2M_LAYER_7_NORMAL
#pragma shader_feature_local _T2M_LAYER_8_NORMAL
#pragma shader_feature_local _T2M_LAYER_9_NORMAL
#pragma shader_feature_local _T2M_LAYER_10_NORMAL
#pragma shader_feature_local _T2M_LAYER_11_NORMAL
#pragma shader_feature_local _T2M_LAYER_12_NORMAL
#pragma shader_feature_local _T2M_LAYER_13_NORMAL
#pragma shader_feature_local _T2M_LAYER_14_NORMAL
#pragma shader_feature_local _T2M_LAYER_15_NORMAL

#pragma shader_feature_local _T2M_LAYER_0_MASK
#pragma shader_feature_local _T2M_LAYER_1_MASK
#pragma shader_feature_local _T2M_LAYER_2_MASK
#pragma shader_feature_local _T2M_LAYER_3_MASK
#pragma shader_feature_local _T2M_LAYER_4_MASK
#pragma shader_feature_local _T2M_LAYER_5_MASK
#pragma shader_feature_local _T2M_LAYER_6_MASK
#pragma shader_feature_local _T2M_LAYER_7_MASK
#pragma shader_feature_local _T2M_LAYER_8_MASK
#pragma shader_feature_local _T2M_LAYER_9_MASK
#pragma shader_feature_local _T2M_LAYER_10_MASK
#pragma shader_feature_local _T2M_LAYER_11_MASK
#pragma shader_feature_local _T2M_LAYER_12_MASK
#pragma shader_feature_local _T2M_LAYER_13_MASK
#pragma shader_feature_local _T2M_LAYER_14_MASK
#pragma shader_feature_local _T2M_LAYER_15_MASK


#define TERRAIN_TO_MESH_NEED_NORMAL
#define TERRAIN_TO_MESH_NEED_METALLIC_SMOOTHNESS_OCCLUSION
#include "Splatmap.cginc"


// vertex-to-fragment interpolation data
// no lightmaps:
#ifndef LIGHTMAP_ON
// half-precision fragment shader registers:
#ifdef UNITY_HALF_PRECISION_FRAGMENT_SHADER_REGISTERS
#define FOG_COMBINED_WITH_TSPACE
struct v2f_surf {
  UNITY_POSITION(pos);
  float4 tSpace0 : TEXCOORD0;
  float4 tSpace1 : TEXCOORD1;
  float4 tSpace2 : TEXCOORD2;
  float2 custompack0 : TEXCOORD3; // texcoord
  #if UNITY_SHOULD_SAMPLE_SH
  half3 sh : TEXCOORD4; // SH
  #endif
  UNITY_LIGHTING_COORDS(5,6)
  #if SHADER_TARGET >= 30
  float4 lmap : TEXCOORD7;
  #endif
  UNITY_VERTEX_INPUT_INSTANCE_ID
  UNITY_VERTEX_OUTPUT_STEREO
};
#endif
// high-precision fragment shader registers:
#ifndef UNITY_HALF_PRECISION_FRAGMENT_SHADER_REGISTERS
struct v2f_surf {
  UNITY_POSITION(pos);
  float4 tSpace0 : TEXCOORD0;
  float4 tSpace1 : TEXCOORD1;
  float4 tSpace2 : TEXCOORD2;
  float2 custompack0 : TEXCOORD3; // texcoord
  #if UNITY_SHOULD_SAMPLE_SH
  half3 sh : TEXCOORD4; // SH
  #endif
  UNITY_FOG_COORDS(5)
  UNITY_SHADOW_COORDS(6)
  #if SHADER_TARGET >= 30
  float4 lmap : TEXCOORD7;
  #endif
  UNITY_VERTEX_INPUT_INSTANCE_ID
  UNITY_VERTEX_OUTPUT_STEREO
};
#endif
#endif
// with lightmaps:
#ifdef LIGHTMAP_ON
// half-precision fragment shader registers:
#ifdef UNITY_HALF_PRECISION_FRAGMENT_SHADER_REGISTERS
#define FOG_COMBINED_WITH_TSPACE
struct v2f_surf {
  UNITY_POSITION(pos);
  float4 tSpace0 : TEXCOORD0;
  float4 tSpace1 : TEXCOORD1;
  float4 tSpace2 : TEXCOORD2;
  float2 custompack0 : TEXCOORD3; // texcoord
  float4 lmap : TEXCOORD4;
  UNITY_LIGHTING_COORDS(5,6)
  UNITY_VERTEX_INPUT_INSTANCE_ID
  UNITY_VERTEX_OUTPUT_STEREO
};
#endif
// high-precision fragment shader registers:
#ifndef UNITY_HALF_PRECISION_FRAGMENT_SHADER_REGISTERS
struct v2f_surf {
  UNITY_POSITION(pos);
  float4 tSpace0 : TEXCOORD0;
  float4 tSpace1 : TEXCOORD1;
  float4 tSpace2 : TEXCOORD2;
  float2 custompack0 : TEXCOORD3; // texcoord
  float4 lmap : TEXCOORD4;
  UNITY_FOG_COORDS(5)
  UNITY_SHADOW_COORDS(6)
  UNITY_VERTEX_INPUT_INSTANCE_ID
  UNITY_VERTEX_OUTPUT_STEREO
};
#endif
#endif

// vertex shader
v2f_surf vert_surf (appdata_full v) {
  UNITY_SETUP_INSTANCE_ID(v);
  v2f_surf o;
  UNITY_INITIALIZE_OUTPUT(v2f_surf,o);
  UNITY_TRANSFER_INSTANCE_ID(v,o);
  UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
  Input customInputData;
  vert (v, customInputData);
  o.custompack0.xy = customInputData.texcoord;
  o.pos = UnityObjectToClipPos(v.vertex);
  float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
  float3 worldNormal = UnityObjectToWorldNormal(v.normal);
  fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
  fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
  fixed3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
  o.tSpace0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
  o.tSpace1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
  o.tSpace2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);
  #ifdef DYNAMICLIGHTMAP_ON
  o.lmap.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
  #endif
  #ifdef LIGHTMAP_ON
  o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
  #endif

  // SH/ambient and vertex lights
  #ifndef LIGHTMAP_ON
    #if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
      o.sh = 0;
      // Approximated illumination from non-important point lights
      #ifdef VERTEXLIGHT_ON
        o.sh += Shade4PointLights (
          unity_4LightPosX0, unity_4LightPosY0, unity_4LightPosZ0,
          unity_LightColor[0].rgb, unity_LightColor[1].rgb, unity_LightColor[2].rgb, unity_LightColor[3].rgb,
          unity_4LightAtten0, worldPos, worldNormal);
      #endif
      o.sh = ShadeSHPerVertex (worldNormal, o.sh);
    #endif
  #endif // !LIGHTMAP_ON

  UNITY_TRANSFER_LIGHTING(o,v.texcoord1.xy); // pass shadow and, possibly, light cookie coordinates to pixel shader
  #ifdef FOG_COMBINED_WITH_TSPACE
    UNITY_TRANSFER_FOG_COMBINED_WITH_TSPACE(o,o.pos); // pass fog coordinates to pixel shader
  #elif defined (FOG_COMBINED_WITH_WORLD_POS)
    UNITY_TRANSFER_FOG_COMBINED_WITH_WORLD_POS(o,o.pos); // pass fog coordinates to pixel shader
  #else
    UNITY_TRANSFER_FOG(o,o.pos); // pass fog coordinates to pixel shader
  #endif
  return o;
}

// fragment shader
fixed4 frag_surf (v2f_surf IN) : SV_Target {
  UNITY_SETUP_INSTANCE_ID(IN);
  // prepare and unpack data
  Input surfIN;
  #ifdef FOG_COMBINED_WITH_TSPACE
    UNITY_EXTRACT_FOG_FROM_TSPACE(IN);
  #elif defined (FOG_COMBINED_WITH_WORLD_POS)
    UNITY_EXTRACT_FOG_FROM_WORLD_POS(IN);
  #else
    UNITY_EXTRACT_FOG(IN);
  #endif
  #ifdef FOG_COMBINED_WITH_TSPACE
    UNITY_RECONSTRUCT_TBN(IN);
  #else
    UNITY_EXTRACT_TBN(IN);
  #endif
  UNITY_INITIALIZE_OUTPUT(Input,surfIN);
  surfIN.texcoord.x = 1.0;
  surfIN.texcoord = IN.custompack0.xy;
  float3 worldPos = float3(IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w);
  #ifndef USING_DIRECTIONAL_LIGHT
    fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
  #else
    fixed3 lightDir = _WorldSpaceLightPos0.xyz;
  #endif
  float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
  #ifdef UNITY_COMPILER_HLSL
  SurfaceOutputStandard o = (SurfaceOutputStandard)0;
  #else
  SurfaceOutputStandard o;
  #endif
  o.Albedo = 0.0;
  o.Emission = 0.0;
  o.Alpha = 0.0;
  o.Occlusion = 1.0;
  fixed3 normalWorldVertex = fixed3(0,0,1);
  o.Normal = fixed3(0,0,1);

  // call surface function
  surf (surfIN, o);

  // compute lighting & shadowing factor
  UNITY_LIGHT_ATTENUATION(atten, IN, worldPos)
  fixed4 c = 0;
  float3 worldN;
  worldN.x = dot(_unity_tbn_0, o.Normal);
  worldN.y = dot(_unity_tbn_1, o.Normal);
  worldN.z = dot(_unity_tbn_2, o.Normal);
  worldN = normalize(worldN);
  o.Normal = worldN;

  // Setup lighting environment
  UnityGI gi;
  UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
  gi.indirect.diffuse = 0;
  gi.indirect.specular = 0;
  gi.light.color = _LightColor0.rgb;
  gi.light.dir = lightDir;
  // Call GI (lightmaps/SH/reflections) lighting function
  UnityGIInput giInput;
  UNITY_INITIALIZE_OUTPUT(UnityGIInput, giInput);
  giInput.light = gi.light;
  giInput.worldPos = worldPos;
  giInput.worldViewDir = worldViewDir;
  giInput.atten = atten;
  #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
    giInput.lightmapUV = IN.lmap;
  #else
    giInput.lightmapUV = 0.0;
  #endif
  #if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
    giInput.ambient = IN.sh;
  #else
    giInput.ambient.rgb = 0.0;
  #endif
  giInput.probeHDR[0] = unity_SpecCube0_HDR;
  giInput.probeHDR[1] = unity_SpecCube1_HDR;
  #if defined(UNITY_SPECCUBE_BLENDING) || defined(UNITY_SPECCUBE_BOX_PROJECTION)
    giInput.boxMin[0] = unity_SpecCube0_BoxMin; // .w holds lerp value for blending
  #endif
  #ifdef UNITY_SPECCUBE_BOX_PROJECTION
    giInput.boxMax[0] = unity_SpecCube0_BoxMax;
    giInput.probePosition[0] = unity_SpecCube0_ProbePosition;
    giInput.boxMax[1] = unity_SpecCube1_BoxMax;
    giInput.boxMin[1] = unity_SpecCube1_BoxMin;
    giInput.probePosition[1] = unity_SpecCube1_ProbePosition;
  #endif
  LightingStandard_GI(o, giInput, gi);

  // realtime lighting: call lighting function
  c += LightingStandard (o, worldViewDir, gi);
  UNITY_APPLY_FOG(_unity_fogCoord, c); // apply fog
  UNITY_OPAQUE_ALPHA(c.a);
  return c;
}



ENDCG

}

	// ---- forward rendering additive lights pass:
	Pass {
		Name "FORWARD"
		Tags { "LightMode" = "ForwardAdd" }
		ZWrite Off Blend One One

CGPROGRAM
// compile directives
#pragma vertex vert_surf
#pragma fragment frag_surf
#pragma target 3.5
#pragma multi_compile_instancing
#pragma multi_compile_fog
#pragma skip_variants INSTANCING_ON
#pragma multi_compile_fwdadd_fullshadows
#include "HLSLSupport.cginc"
#define UNITY_INSTANCED_LOD_FADE
#define UNITY_INSTANCED_SH
#define UNITY_INSTANCED_LIGHTMAPSTS
#include "UnityShaderVariables.cginc"
#include "UnityShaderUtilities.cginc"


#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "UnityPBSLighting.cginc"
#include "AutoLight.cginc"

#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
#define WorldNormalVector(data,normal) fixed3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))


        struct Input
        {
			float2 texcoord;
        };




//Curved World
//#define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE
//#define CURVEDWORLD_BEND_ID_1
//#pragma shader_feature_local CURVEDWORLD_DISABLED_ON
//#pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON
//#include "Assets/Amazing Assets/Curved World/Shaders/Core/CurvedWorldTransform.cginc"


#define _ALPHATEST_ON

#pragma shader_feature_local _ _T2M_TEXTURE_SAMPLE_TYPE_ARRAY

#pragma shader_feature_local _ _T2M_LAYER_COUNT_3 _T2M_LAYER_COUNT_4 _T2M_LAYER_COUNT_5 _T2M_LAYER_COUNT_6 _T2M_LAYER_COUNT_7 _T2M_LAYER_COUNT_8 _T2M_LAYER_COUNT_9 _T2M_LAYER_COUNT_10 _T2M_LAYER_COUNT_11 _T2M_LAYER_COUNT_12 _T2M_LAYER_COUNT_13 _T2M_LAYER_COUNT_14 _T2M_LAYER_COUNT_15 _T2M_LAYER_COUNT_16
		
#pragma shader_feature_local _T2M_LAYER_0_NORMAL
#pragma shader_feature_local _T2M_LAYER_1_NORMAL
#pragma shader_feature_local _T2M_LAYER_2_NORMAL
#pragma shader_feature_local _T2M_LAYER_3_NORMAL
#pragma shader_feature_local _T2M_LAYER_4_NORMAL
#pragma shader_feature_local _T2M_LAYER_5_NORMAL
#pragma shader_feature_local _T2M_LAYER_6_NORMAL
#pragma shader_feature_local _T2M_LAYER_7_NORMAL
#pragma shader_feature_local _T2M_LAYER_8_NORMAL
#pragma shader_feature_local _T2M_LAYER_9_NORMAL
#pragma shader_feature_local _T2M_LAYER_10_NORMAL
#pragma shader_feature_local _T2M_LAYER_11_NORMAL
#pragma shader_feature_local _T2M_LAYER_12_NORMAL
#pragma shader_feature_local _T2M_LAYER_13_NORMAL
#pragma shader_feature_local _T2M_LAYER_14_NORMAL
#pragma shader_feature_local _T2M_LAYER_15_NORMAL

#pragma shader_feature_local _T2M_LAYER_0_MASK
#pragma shader_feature_local _T2M_LAYER_1_MASK
#pragma shader_feature_local _T2M_LAYER_2_MASK
#pragma shader_feature_local _T2M_LAYER_3_MASK
#pragma shader_feature_local _T2M_LAYER_4_MASK
#pragma shader_feature_local _T2M_LAYER_5_MASK
#pragma shader_feature_local _T2M_LAYER_6_MASK
#pragma shader_feature_local _T2M_LAYER_7_MASK
#pragma shader_feature_local _T2M_LAYER_8_MASK
#pragma shader_feature_local _T2M_LAYER_9_MASK
#pragma shader_feature_local _T2M_LAYER_10_MASK
#pragma shader_feature_local _T2M_LAYER_11_MASK
#pragma shader_feature_local _T2M_LAYER_12_MASK
#pragma shader_feature_local _T2M_LAYER_13_MASK
#pragma shader_feature_local _T2M_LAYER_14_MASK
#pragma shader_feature_local _T2M_LAYER_15_MASK


#define TERRAIN_TO_MESH_NEED_NORMAL
#define TERRAIN_TO_MESH_NEED_METALLIC_SMOOTHNESS_OCCLUSION
#include "Splatmap.cginc"

        

// vertex-to-fragment interpolation data
struct v2f_surf {
  UNITY_POSITION(pos);
  float3 tSpace0 : TEXCOORD0;
  float3 tSpace1 : TEXCOORD1;
  float3 tSpace2 : TEXCOORD2;
  float3 worldPos : TEXCOORD3;
  float2 custompack0 : TEXCOORD4; // texcoord
  UNITY_LIGHTING_COORDS(5,6)
  UNITY_FOG_COORDS(7)
  UNITY_VERTEX_INPUT_INSTANCE_ID
  UNITY_VERTEX_OUTPUT_STEREO
};

// vertex shader
v2f_surf vert_surf (appdata_full v) {
  UNITY_SETUP_INSTANCE_ID(v);
  v2f_surf o;
  UNITY_INITIALIZE_OUTPUT(v2f_surf,o);
  UNITY_TRANSFER_INSTANCE_ID(v,o);
  UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
  Input customInputData;
  vert (v, customInputData);
  o.custompack0.xy = customInputData.texcoord;
  o.pos = UnityObjectToClipPos(v.vertex);
  float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
  float3 worldNormal = UnityObjectToWorldNormal(v.normal);
  fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
  fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
  fixed3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
  o.tSpace0 = float3(worldTangent.x, worldBinormal.x, worldNormal.x);
  o.tSpace1 = float3(worldTangent.y, worldBinormal.y, worldNormal.y);
  o.tSpace2 = float3(worldTangent.z, worldBinormal.z, worldNormal.z);
  o.worldPos.xyz = worldPos;

  UNITY_TRANSFER_LIGHTING(o,v.texcoord1.xy); // pass shadow and, possibly, light cookie coordinates to pixel shader
  UNITY_TRANSFER_FOG(o,o.pos); // pass fog coordinates to pixel shader
  return o;
}

// fragment shader
fixed4 frag_surf (v2f_surf IN) : SV_Target {
  UNITY_SETUP_INSTANCE_ID(IN);
  // prepare and unpack data
  Input surfIN;
  #ifdef FOG_COMBINED_WITH_TSPACE
    UNITY_EXTRACT_FOG_FROM_TSPACE(IN);
  #elif defined (FOG_COMBINED_WITH_WORLD_POS)
    UNITY_EXTRACT_FOG_FROM_WORLD_POS(IN);
  #else
    UNITY_EXTRACT_FOG(IN);
  #endif
  #ifdef FOG_COMBINED_WITH_TSPACE
    UNITY_RECONSTRUCT_TBN(IN);
  #else
    UNITY_EXTRACT_TBN(IN);
  #endif
  UNITY_INITIALIZE_OUTPUT(Input,surfIN);
  surfIN.texcoord.x = 1.0;
  surfIN.texcoord = IN.custompack0.xy;
  float3 worldPos = IN.worldPos.xyz;
  #ifndef USING_DIRECTIONAL_LIGHT
    fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
  #else
    fixed3 lightDir = _WorldSpaceLightPos0.xyz;
  #endif
  float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
  #ifdef UNITY_COMPILER_HLSL
  SurfaceOutputStandard o = (SurfaceOutputStandard)0;
  #else
  SurfaceOutputStandard o;
  #endif
  o.Albedo = 0.0;
  o.Emission = 0.0;
  o.Alpha = 0.0;
  o.Occlusion = 1.0;
  fixed3 normalWorldVertex = fixed3(0,0,1);
  o.Normal = fixed3(0,0,1);

  // call surface function
  surf (surfIN, o);
  UNITY_LIGHT_ATTENUATION(atten, IN, worldPos)
  fixed4 c = 0;
  float3 worldN;
  worldN.x = dot(_unity_tbn_0, o.Normal);
  worldN.y = dot(_unity_tbn_1, o.Normal);
  worldN.z = dot(_unity_tbn_2, o.Normal);
  worldN = normalize(worldN);
  o.Normal = worldN;

  // Setup lighting environment
  UnityGI gi;
  UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
  gi.indirect.diffuse = 0;
  gi.indirect.specular = 0;
  gi.light.color = _LightColor0.rgb;
  gi.light.dir = lightDir;
  gi.light.color *= atten;
  c += LightingStandard (o, worldViewDir, gi);
  c.a = 0.0;
  UNITY_APPLY_FOG(_unity_fogCoord, c); // apply fog
  UNITY_OPAQUE_ALPHA(c.a);
  return c;
}



ENDCG

}

	// ---- deferred shading pass:
	Pass {
		Name "DEFERRED"
		Tags { "LightMode" = "Deferred" }

CGPROGRAM
// compile directives
#pragma vertex vert_surf
#pragma fragment frag_surf
#pragma target 3.5
#pragma multi_compile_instancing
#pragma exclude_renderers nomrt
#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
#pragma multi_compile_prepassfinal
#include "HLSLSupport.cginc"
#define UNITY_INSTANCED_LOD_FADE
#define UNITY_INSTANCED_SH
#define UNITY_INSTANCED_LIGHTMAPSTS
#include "UnityShaderVariables.cginc"
#include "UnityShaderUtilities.cginc"


#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "UnityPBSLighting.cginc"

#define INTERNAL_DATA half3 internalSurfaceTtoW0; half3 internalSurfaceTtoW1; half3 internalSurfaceTtoW2;
#define WorldReflectionVector(data,normal) reflect (data.worldRefl, half3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal)))
#define WorldNormalVector(data,normal) fixed3(dot(data.internalSurfaceTtoW0,normal), dot(data.internalSurfaceTtoW1,normal), dot(data.internalSurfaceTtoW2,normal))

        struct Input
        {
			float2 texcoord;
        };



//Curved World
//#define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE
//#define CURVEDWORLD_BEND_ID_1
//#pragma shader_feature_local CURVEDWORLD_DISABLED_ON
//#pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON
//#include "Assets/Amazing Assets/Curved World/Shaders/Core/CurvedWorldTransform.cginc"


#define _ALPHATEST_ON

#pragma shader_feature_local _ _T2M_TEXTURE_SAMPLE_TYPE_ARRAY

#pragma shader_feature_local _ _T2M_LAYER_COUNT_3 _T2M_LAYER_COUNT_4 _T2M_LAYER_COUNT_5 _T2M_LAYER_COUNT_6 _T2M_LAYER_COUNT_7 _T2M_LAYER_COUNT_8 _T2M_LAYER_COUNT_9 _T2M_LAYER_COUNT_10 _T2M_LAYER_COUNT_11 _T2M_LAYER_COUNT_12 _T2M_LAYER_COUNT_13 _T2M_LAYER_COUNT_14 _T2M_LAYER_COUNT_15 _T2M_LAYER_COUNT_16
		
#pragma shader_feature_local _T2M_LAYER_0_NORMAL
#pragma shader_feature_local _T2M_LAYER_1_NORMAL
#pragma shader_feature_local _T2M_LAYER_2_NORMAL
#pragma shader_feature_local _T2M_LAYER_3_NORMAL
#pragma shader_feature_local _T2M_LAYER_4_NORMAL
#pragma shader_feature_local _T2M_LAYER_5_NORMAL
#pragma shader_feature_local _T2M_LAYER_6_NORMAL
#pragma shader_feature_local _T2M_LAYER_7_NORMAL
#pragma shader_feature_local _T2M_LAYER_8_NORMAL
#pragma shader_feature_local _T2M_LAYER_9_NORMAL
#pragma shader_feature_local _T2M_LAYER_10_NORMAL
#pragma shader_feature_local _T2M_LAYER_11_NORMAL
#pragma shader_feature_local _T2M_LAYER_12_NORMAL
#pragma shader_feature_local _T2M_LAYER_13_NORMAL
#pragma shader_feature_local _T2M_LAYER_14_NORMAL
#pragma shader_feature_local _T2M_LAYER_15_NORMAL

#pragma shader_feature_local _T2M_LAYER_0_MASK
#pragma shader_feature_local _T2M_LAYER_1_MASK
#pragma shader_feature_local _T2M_LAYER_2_MASK
#pragma shader_feature_local _T2M_LAYER_3_MASK
#pragma shader_feature_local _T2M_LAYER_4_MASK
#pragma shader_feature_local _T2M_LAYER_5_MASK
#pragma shader_feature_local _T2M_LAYER_6_MASK
#pragma shader_feature_local _T2M_LAYER_7_MASK
#pragma shader_feature_local _T2M_LAYER_8_MASK
#pragma shader_feature_local _T2M_LAYER_9_MASK
#pragma shader_feature_local _T2M_LAYER_10_MASK
#pragma shader_feature_local _T2M_LAYER_11_MASK
#pragma shader_feature_local _T2M_LAYER_12_MASK
#pragma shader_feature_local _T2M_LAYER_13_MASK
#pragma shader_feature_local _T2M_LAYER_14_MASK
#pragma shader_feature_local _T2M_LAYER_15_MASK


#define TERRAIN_TO_MESH_NEED_NORMAL
#define TERRAIN_TO_MESH_NEED_METALLIC_SMOOTHNESS_OCCLUSION
#include "Splatmap.cginc"

        

// vertex-to-fragment interpolation data
struct v2f_surf {
  UNITY_POSITION(pos);
  float4 tSpace0 : TEXCOORD0;
  float4 tSpace1 : TEXCOORD1;
  float4 tSpace2 : TEXCOORD2;
  float2 custompack0 : TEXCOORD3; // texcoord
#ifndef DIRLIGHTMAP_OFF
  float3 viewDir : TEXCOORD4;
#endif
  float4 lmap : TEXCOORD5;
#ifndef LIGHTMAP_ON
  #if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
    half3 sh : TEXCOORD6; // SH
  #endif
#else
  #ifdef DIRLIGHTMAP_OFF
    float4 lmapFadePos : TEXCOORD6;
  #endif
#endif
  UNITY_VERTEX_INPUT_INSTANCE_ID
  UNITY_VERTEX_OUTPUT_STEREO
};

// vertex shader
v2f_surf vert_surf (appdata_full v) {
  UNITY_SETUP_INSTANCE_ID(v);
  v2f_surf o;
  UNITY_INITIALIZE_OUTPUT(v2f_surf,o);
  UNITY_TRANSFER_INSTANCE_ID(v,o);
  UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
  Input customInputData;
  vert (v, customInputData);
  o.custompack0.xy = customInputData.texcoord;
  o.pos = UnityObjectToClipPos(v.vertex);
  float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
  float3 worldNormal = UnityObjectToWorldNormal(v.normal);
  fixed3 worldTangent = UnityObjectToWorldDir(v.tangent.xyz);
  fixed tangentSign = v.tangent.w * unity_WorldTransformParams.w;
  fixed3 worldBinormal = cross(worldNormal, worldTangent) * tangentSign;
  o.tSpace0 = float4(worldTangent.x, worldBinormal.x, worldNormal.x, worldPos.x);
  o.tSpace1 = float4(worldTangent.y, worldBinormal.y, worldNormal.y, worldPos.y);
  o.tSpace2 = float4(worldTangent.z, worldBinormal.z, worldNormal.z, worldPos.z);
  float3 viewDirForLight = UnityWorldSpaceViewDir(worldPos);
  #ifndef DIRLIGHTMAP_OFF
  o.viewDir.x = dot(viewDirForLight, worldTangent);
  o.viewDir.y = dot(viewDirForLight, worldBinormal);
  o.viewDir.z = dot(viewDirForLight, worldNormal);
  #endif
#ifdef DYNAMICLIGHTMAP_ON
  o.lmap.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
#else
  o.lmap.zw = 0;
#endif
#ifdef LIGHTMAP_ON
  o.lmap.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
  #ifdef DIRLIGHTMAP_OFF
    o.lmapFadePos.xyz = (mul(unity_ObjectToWorld, v.vertex).xyz - unity_ShadowFadeCenterAndType.xyz) * unity_ShadowFadeCenterAndType.w;
    o.lmapFadePos.w = (-UnityObjectToViewPos(v.vertex).z) * (1.0 - unity_ShadowFadeCenterAndType.w);
  #endif
#else
  o.lmap.xy = 0;
    #if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
      o.sh = 0;
      o.sh = ShadeSHPerVertex (worldNormal, o.sh);
    #endif
#endif
  return o;
}
#ifdef LIGHTMAP_ON
float4 unity_LightmapFade;
#endif
fixed4 unity_Ambient;

// fragment shader
void frag_surf (v2f_surf IN,
    out half4 outGBuffer0 : SV_Target0,
    out half4 outGBuffer1 : SV_Target1,
    out half4 outGBuffer2 : SV_Target2,
    out half4 outEmission : SV_Target3
#if defined(SHADOWS_SHADOWMASK) && (UNITY_ALLOWED_MRT_COUNT > 4)
    , out half4 outShadowMask : SV_Target4
#endif
) {
  UNITY_SETUP_INSTANCE_ID(IN);
  // prepare and unpack data
  Input surfIN;
  #ifdef FOG_COMBINED_WITH_TSPACE
    UNITY_EXTRACT_FOG_FROM_TSPACE(IN);
  #elif defined (FOG_COMBINED_WITH_WORLD_POS)
    UNITY_EXTRACT_FOG_FROM_WORLD_POS(IN);
  #else
    UNITY_EXTRACT_FOG(IN);
  #endif
  #ifdef FOG_COMBINED_WITH_TSPACE
    UNITY_RECONSTRUCT_TBN(IN);
  #else
    UNITY_EXTRACT_TBN(IN);
  #endif
  UNITY_INITIALIZE_OUTPUT(Input,surfIN);
  surfIN.texcoord.x = 1.0;
  surfIN.texcoord = IN.custompack0.xy;
  float3 worldPos = float3(IN.tSpace0.w, IN.tSpace1.w, IN.tSpace2.w);
  #ifndef USING_DIRECTIONAL_LIGHT
    fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
  #else
    fixed3 lightDir = _WorldSpaceLightPos0.xyz;
  #endif
  float3 worldViewDir = normalize(UnityWorldSpaceViewDir(worldPos));
  #ifdef UNITY_COMPILER_HLSL
  SurfaceOutputStandard o = (SurfaceOutputStandard)0;
  #else
  SurfaceOutputStandard o;
  #endif
  o.Albedo = 0.0;
  o.Emission = 0.0;
  o.Alpha = 0.0;
  o.Occlusion = 1.0;
  fixed3 normalWorldVertex = fixed3(0,0,1);
  o.Normal = fixed3(0,0,1);

  // call surface function
  surf (surfIN, o);
fixed3 originalNormal = o.Normal;
  float3 worldN;
  worldN.x = dot(_unity_tbn_0, o.Normal);
  worldN.y = dot(_unity_tbn_1, o.Normal);
  worldN.z = dot(_unity_tbn_2, o.Normal);
  worldN = normalize(worldN);
  o.Normal = worldN;
  half atten = 1;

  // Setup lighting environment
  UnityGI gi;
  UNITY_INITIALIZE_OUTPUT(UnityGI, gi);
  gi.indirect.diffuse = 0;
  gi.indirect.specular = 0;
  gi.light.color = 0;
  gi.light.dir = half3(0,1,0);
  // Call GI (lightmaps/SH/reflections) lighting function
  UnityGIInput giInput;
  UNITY_INITIALIZE_OUTPUT(UnityGIInput, giInput);
  giInput.light = gi.light;
  giInput.worldPos = worldPos;
  giInput.worldViewDir = worldViewDir;
  giInput.atten = atten;
  #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
    giInput.lightmapUV = IN.lmap;
  #else
    giInput.lightmapUV = 0.0;
  #endif
  #if UNITY_SHOULD_SAMPLE_SH && !UNITY_SAMPLE_FULL_SH_PER_PIXEL
    giInput.ambient = IN.sh;
  #else
    giInput.ambient.rgb = 0.0;
  #endif
  giInput.probeHDR[0] = unity_SpecCube0_HDR;
  giInput.probeHDR[1] = unity_SpecCube1_HDR;
  #if defined(UNITY_SPECCUBE_BLENDING) || defined(UNITY_SPECCUBE_BOX_PROJECTION)
    giInput.boxMin[0] = unity_SpecCube0_BoxMin; // .w holds lerp value for blending
  #endif
  #ifdef UNITY_SPECCUBE_BOX_PROJECTION
    giInput.boxMax[0] = unity_SpecCube0_BoxMax;
    giInput.probePosition[0] = unity_SpecCube0_ProbePosition;
    giInput.boxMax[1] = unity_SpecCube1_BoxMax;
    giInput.boxMin[1] = unity_SpecCube1_BoxMin;
    giInput.probePosition[1] = unity_SpecCube1_ProbePosition;
  #endif
  LightingStandard_GI(o, giInput, gi);

  // call lighting function to output g-buffer
  outEmission = LightingStandard_Deferred (o, worldViewDir, gi, outGBuffer0, outGBuffer1, outGBuffer2);
  #if defined(SHADOWS_SHADOWMASK) && (UNITY_ALLOWED_MRT_COUNT > 4)
    outShadowMask = UnityGetRawBakedOcclusions (IN.lmap.xy, worldPos);
  #endif
  #ifndef UNITY_HDR_ON
  outEmission.rgb = exp2(-outEmission.rgb);
  #endif
}



ENDCG

}

	// ---- shadow caster pass:
	Pass {
		Name "ShadowCaster"
		Tags { "LightMode" = "ShadowCaster" }
		ZWrite On ZTest LEqual

CGPROGRAM
// compile directives
#pragma vertex vert_surf
#pragma fragment frag_surf
#pragma target 3.0
#pragma multi_compile_instancing
#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
#pragma multi_compile_shadowcaster
#include "HLSLSupport.cginc"
#define UNITY_INSTANCED_LOD_FADE
#define UNITY_INSTANCED_SH
#define UNITY_INSTANCED_LIGHTMAPSTS
#include "UnityShaderVariables.cginc"
#include "UnityShaderUtilities.cginc"


#include "UnityCG.cginc"
#include "Lighting.cginc"
#include "UnityPBSLighting.cginc"

#define INTERNAL_DATA
#define WorldReflectionVector(data,normal) data.worldRefl
#define WorldNormalVector(data,normal) normal


        struct Input
        {
			float2 texcoord;
        };



//Curved World
//#define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE
//#define CURVEDWORLD_BEND_ID_1
//#pragma shader_feature_local CURVEDWORLD_DISABLED_ON
//#pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON
//#include "Assets/Amazing Assets/Curved World/Shaders/Core/CurvedWorldTransform.cginc"

#define _ALPHATEST_ON

#define TERRAIN_TO_MESH_PASS_SHADOW_CASTER
#include "Splatmap.cginc"
        

// vertex-to-fragment interpolation data
struct v2f_surf {
  V2F_SHADOW_CASTER;
  float3 worldPos : TEXCOORD1;
  float2 custompack0 : TEXCOORD2; // texcoord
  UNITY_VERTEX_INPUT_INSTANCE_ID
  UNITY_VERTEX_OUTPUT_STEREO
};

// vertex shader
v2f_surf vert_surf (appdata_full v) {
  UNITY_SETUP_INSTANCE_ID(v);
  v2f_surf o;
  UNITY_INITIALIZE_OUTPUT(v2f_surf,o);
  UNITY_TRANSFER_INSTANCE_ID(v,o);
  UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
  Input customInputData;
  vert (v, customInputData);
  o.custompack0.xy = customInputData.texcoord;
  float3 worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
  float3 worldNormal = UnityObjectToWorldNormal(v.normal);
  o.worldPos.xyz = worldPos;
  TRANSFER_SHADOW_CASTER_NORMALOFFSET(o)
  return o;
}

// fragment shader
fixed4 frag_surf (v2f_surf IN) : SV_Target {
  UNITY_SETUP_INSTANCE_ID(IN);
  // prepare and unpack data
  Input surfIN;
  #ifdef FOG_COMBINED_WITH_TSPACE
    UNITY_EXTRACT_FOG_FROM_TSPACE(IN);
  #elif defined (FOG_COMBINED_WITH_WORLD_POS)
    UNITY_EXTRACT_FOG_FROM_WORLD_POS(IN);
  #else
    UNITY_EXTRACT_FOG(IN);
  #endif
  UNITY_INITIALIZE_OUTPUT(Input,surfIN);
  surfIN.texcoord.x = 1.0;
  surfIN.texcoord = IN.custompack0.xy;
  float3 worldPos = IN.worldPos.xyz;
  #ifndef USING_DIRECTIONAL_LIGHT
    fixed3 lightDir = normalize(UnityWorldSpaceLightDir(worldPos));
  #else
    fixed3 lightDir = _WorldSpaceLightPos0.xyz;
  #endif
  #ifdef UNITY_COMPILER_HLSL
  SurfaceOutputStandard o = (SurfaceOutputStandard)0;
  #else
  SurfaceOutputStandard o;
  #endif
  o.Albedo = 0.0;
  o.Emission = 0.0;
  o.Alpha = 0.0;
  o.Occlusion = 1.0;
  fixed3 normalWorldVertex = fixed3(0,0,1);

  // call surface function
  surf (surfIN, o);
  SHADOW_CASTER_FRAGMENT(IN)
}


ENDCG

}

}
    

	//"Mobile/Diffuse"
    SubShader 
    {
        Tags { "RenderType"="Opaque" }
        LOD 150

        CGPROGRAM
        #pragma surface surf Lambert fullforwardshadows addshadow vertex:vert


//Curved World
//#define CURVEDWORLD_BEND_TYPE_CLASSICRUNNER_X_POSITIVE
//#define CURVEDWORLD_BEND_ID_1
//#pragma shader_feature_local CURVEDWORLD_DISABLED_ON
//#pragma shader_feature_local CURVEDWORLD_NORMAL_TRANSFORMATION_ON
//#include "Assets/Amazing Assets/Curved World/Shaders/Core/CurvedWorldTransform.cginc"


        #define _ALPHATEST_ON


        sampler2D _MainTex;
        sampler2D _T2M_HolesMap;
		sampler2D _BumpMap;

        struct Input 
        {
            float2 texcoord;
        };


		void vert (inout appdata_full v, out Input o) 
        {
	        UNITY_INITIALIZE_OUTPUT(Input,o); 

            o.texcoord = v.texcoord.xy;

	        //Curved World
			#if defined(CURVEDWORLD_IS_INSTALLED) && !defined(CURVEDWORLD_DISABLED_ON)
				#ifdef CURVEDWORLD_NORMAL_TRANSFORMATION_ON
					CURVEDWORLD_TRANSFORM_VERTEX_AND_NORMAL(v.vertex, v.normal, v.tangent)
				#else
					CURVEDWORLD_TRANSFORM_VERTEX(v.vertex)
				#endif
			#endif
        }

        void surf (Input IN, inout SurfaceOutput o) 
        {
            o.Alpha = 1;
            #if defined(_ALPHATEST_ON)
                o.Alpha = tex2D(_T2M_HolesMap, IN.texcoord).r;
                clip(o.Alpha - 0.5);
            #endif
            
			o.Albedo = tex2D(_MainTex, IN.texcoord).rgb;
            o.Normal = UnpackNormal(tex2D(_BumpMap, IN.texcoord));
            
        }
        ENDCG
    }


	CustomEditor "AmazingAssets.TerrainToMesh.Editor.SplatmapShaderGUI"
}
