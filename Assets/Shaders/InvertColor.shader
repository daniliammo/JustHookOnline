Shader "Custom/InvertColor" 
{
    Properties { _Color ("Tint Color", Color) = (1, 1, 1, 1) }
   
    SubShader
    {
        Tags { "Queue" = "Transparent" }
 
        Pass
        {
           ZWrite On
           ColorMask 0
        }
        Blend OneMinusDstColor OneMinusSrcAlpha
        BlendOp Add
       
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            uniform float4 _Color; // Не переименовывать
             
            struct vertex_input
            {
                float4 vertex: POSITION;
                float4 color : COLOR;
            };
             
            struct fragment_input
            {
                float4 pos : SV_POSITION;
                float4 color : COLOR0;
            };
             
            fragment_input vert(const vertex_input i)
            {
                fragment_input o;
                o.pos = UnityObjectToClipPos(i.vertex);
                o.color = _Color;
                return o;
            }
             
            half4 frag(fragment_input i) : COLOR
            {
                return i.color;
            }
             
            ENDCG
        }
    }

}

