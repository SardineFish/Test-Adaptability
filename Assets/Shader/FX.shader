Shader "Project/FX/Test" {
    HLSLINCLUDE

        #include "Packages/com.unity.postprocessing/PostProcessing/Shaders/StdLib.hlsl"

        struct v2f
        {
            float4 vertex : SV_POSITION;
            float2 uv : TEXCOORD0;
        };

        sampler2D _MainTex;

        float4 Frag(VaryingsDefault i) : SV_Target
        {
            
            return float4(i.texcoord.xy,0,1);// tex2D(_MainTex, i.texcoord.xy).rgba;
        }

    ENDHLSL

    SubShader
    {
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            HLSLPROGRAM

                #pragma vertex VertDefault
                #pragma fragment Frag

            ENDHLSL
        }
    }
}