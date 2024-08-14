// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

// Simplified Diffuse shader. Differences from regular Diffuse one:
// - no Main Color
// - fully supports only 1 directional light. Other lights can affect it, but it will be per-vertex/SH.

// Shader "Mobile/Diffuse" 

Shader "Amazing Assets/Terrain To Mesh/Grass"
{
    Properties 
    {
        _MainTex ("Base", 2D) = "white" {}
        _Cutoff ("Cutoff", float) = 0.5
    }

    SubShader 
    {
        Tags { "RenderType"="TransparentCutout" "Queue"="AlphaTest"}
        LOD 150
        Cull Off

        CGPROGRAM
        #pragma surface surf Lambert noforwardadd addshadow nometa

        sampler2D _MainTex;
        float _Cutoff;

        struct Input 
        {
            float2 uv_MainTex;

            fixed4 color : COLOR;
        };

        void surf (Input IN, inout SurfaceOutput o) 
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex);

            clip(c.a - _Cutoff * 1.01);

            o.Albedo = c.rgb * IN.color.rgb * 2;
            o.Alpha = c.a;
            
        }
        ENDCG
    }
}
