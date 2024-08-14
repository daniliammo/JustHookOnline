Shader "Custom/FadeOutBoundaryUnlit" 
{
	Properties 
	{
		_Color ("Tint", Vector) = (0, 0, 0, 1)
		_MainTex ("Texture", 2D) = "white" { }
		_Sharpness ("Blend sharpness", Range(1, 64)) = 1
		_MinDistance ("Minimum Distance", Float) = 2
		_MaxDistance ("Maximum Distance", Float) = 3
	}
	SubShader 
	{
		Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
		Pass 
		{
			Tags { "QUEUE" = "Transparent" "RenderType" = "Transparent" }
			Blend SrcAlpha OneMinusSrcAlpha, SrcAlpha OneMinusSrcAlpha
			ZWrite Off
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			struct v2_f
			{
				float4 position : SV_POSITION0;
				float3 texcoord : TEXCOORD0;
				float3 normal : NORMAL0;
			};
			struct fout
			{
				float4 sv_target : SV_TARGET0;
			};
			// $Globals ConstantBuffers for Vertex Shader
			// $Globals ConstantBuffers for Fragment Shader
			float4 _MainTex_ST;
			float4 _Color;
			float _Sharpness;
			float _MinDistance;
			float _MaxDistance;
			// Custom ConstantBuffers for Vertex Shader
			// Custom ConstantBuffers for Fragment Shader
			// Texture params for Vertex Shader
			// Texture params for Fragment Shader
			sampler2D _MainTex;
			
			// Keywords:
			v2_f vert(appdata_full v)
			{
                v2_f o;
                float4 tmp0 = v.vertex.yyyy * unity_ObjectToWorld._m01_m11_m21_m31;
                tmp0 = unity_ObjectToWorld._m00_m10_m20_m30 * v.vertex.xxxx + tmp0;
                tmp0 = unity_ObjectToWorld._m02_m12_m22_m32 * v.vertex.zzzz + tmp0;
                float4 tmp1 = tmp0 + unity_ObjectToWorld._m03_m13_m23_m33;
                o.texcoord.xyz = unity_ObjectToWorld._m03_m13_m23 * v.vertex.www + tmp0.xyz;
                tmp0 = tmp1.yyyy * unity_MatrixVP._m01_m11_m21_m31;
                tmp0 = unity_MatrixVP._m00_m10_m20_m30 * tmp1.xxxx + tmp0;
                tmp0 = unity_MatrixVP._m02_m12_m22_m32 * tmp1.zzzz + tmp0;
                o.position = unity_MatrixVP._m03_m13_m23_m33 * tmp1.wwww + tmp0;
                tmp0.x = dot(v.normal.xyz, unity_WorldToObject._m00_m10_m20);
                tmp0.y = dot(v.normal.xyz, unity_WorldToObject._m01_m11_m21);
                tmp0.z = dot(v.normal.xyz, unity_WorldToObject._m02_m12_m22);
                tmp0.w = dot(tmp0.xyz, tmp0.xyz);
                tmp0.w = rsqrt(tmp0.w);
                o.normal.xyz = tmp0.www * tmp0.xyz;
                return o;
			}
			// Keywords: 
			fout frag(v2_f inp)
			{
                fout o;
                float4 tmp0;
                tmp0.xyz = log(abs(inp.normal.xyz));
                tmp0.xyz = tmp0.xyz * _Sharpness.xxx;
                tmp0.xyz = exp(tmp0.xyz);
                tmp0.w = tmp0.y + tmp0.x;
                tmp0.w = tmp0.z + tmp0.w;
                tmp0.xyz = tmp0.xyz / tmp0.www;
                float4 tmp1 = inp.texcoord.xyzy * _MainTex_ST + _MainTex_ST;
                float4 tmp2 = tex2D(_MainTex, tmp1.zw);
                tmp1 = tex2D(_MainTex, tmp1.xy);
                tmp2 = tmp0.xxxx * tmp2;
                tmp1 = tmp1 * tmp0.zzzz + tmp2;
                tmp0.xz = inp.texcoord.xz * _MainTex_ST.xy + _MainTex_ST.zw;
                tmp2 = tex2D(_MainTex, tmp0.xz);
                tmp0 = tmp2 * tmp0.yyyy + tmp1;
                tmp0 = tmp0 * _Color;
                tmp1.xyz = inp.texcoord.xyz - _WorldSpaceCameraPos;
                tmp1.x = dot(tmp1.xyz, tmp1.xyz);
                tmp1.x = sqrt(tmp1.x);
                tmp1.x = tmp1.x - _MinDistance;
                tmp1.x = saturate(tmp1.x / _MaxDistance);
                tmp1.x = 1.0 - tmp1.x;
                o.sv_target.w = tmp0.w * tmp1.x;
                o.sv_target.xyz = tmp0.xyz;
                return o;
			}
			ENDCG
		}
	}
	Fallback "Standard"
}