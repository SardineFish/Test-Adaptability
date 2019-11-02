Shader "Project/SpritePreview" {
    Properties {
        _MainTex ("Main Tex", 2D) = "white" {}
        _Color ("Color", Color) = (1,1,1,1)
        _BGColor ("BG Color", Color) = (1,1,1,1) 
    }

    CGINCLUDE

    Texture2D _MainTex;
    SamplerState tex_point_clamp_sampler;

    struct a2v
    {
        float4 vertex : POSITION;
        float2 uv : TEXCOORD0;
    };

    struct v2f
    {
        float4 vertex : SV_POSITION;
        float2 uv : TEXCOORD0;
    };

    v2f vert(a2v i)
    {
        v2f o;
        o.vertex = UnityObjectToClipPos(i.vertex);
        o.uv = i.uv;
        return o;
    }

    float4 _BGColor;

    float4 frag(v2f i) : SV_TARGET
    {
        float4 color = _MainTex.Sample(tex_point_clamp_sampler, i.uv.xy).rgba;
        //return float4(color.rgb * color.a + _BGColor.rgb * (1 - color.a), 1);
        return color;
    }


    ENDCG

    SubShader {
        Cull Off
        ZWrite Off
        ZTest Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            ENDCG
        }
    }
}