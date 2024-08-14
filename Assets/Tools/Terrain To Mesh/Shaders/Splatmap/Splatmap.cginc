#ifndef VACUUM_SHADERS_T2M_DEFERRED_CGINC
#define VACUUM_SHADERS_T2M_DEFERRED_CGINC



#include "../cginc/Core.cginc"


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


void surf (Input IN, inout SurfaceOutputStandard o)
{
	#if defined(_ALPHATEST_ON)
		float holesmapValue = TerrainToMeshCalculateClipValue(IN.texcoord.xy);
		clip(holesmapValue - 0.5);
	#endif


	#if defined(TERRAIN_TO_MESH_PASS_SHADOW_CASTER)
		
		o.Albedo = 0;
		o.Alpha = 1;
		o.Normal = float3(0, 0, 1);
		o.Metallic = 0;
		o.Smoothness = 0;
		o.Occlusion = 1;

	#else

		TerrainToMeshCalculateLayersBlend(IN.texcoord.xy, o.Albedo, o.Alpha, o.Normal, o.Metallic, o.Smoothness, o.Occlusion);	

	#endif
}

#endif 