Shader "Project/BackgroundPass" {
    SubShader {
        // #0
        Pass {
            ZWrite Off
            ZTest LEqual
            Cull Off

            CGPROGRAM

            #include "../Lib.hlsl"

            #pragma vertex default_vert
            #pragma fragment frag

            struct v2f_screen
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f_screen vert(appdata_full i)
            {
                v2f_screen o;
                o.pos = UnityObjectToClipPos(i.vertex);
                o.uv = i.texcoord;
                return o;
            }

            sampler2D _MainTex;
            float4 _SpriteRect; // (dx, dy, sx, sy)

            float4 frag(v2f i, out float depthOut : SV_DEPTH) : SV_TARGET
            {
                float2 uv = frac(i.worldPos);
                uv = uv * _SpriteRect.zw + _SpriteRect.xy;
                depthOut = 0;
                float4 color = tex2D(_MainTex, uv).rgba;
                return color;
            }

            ENDCG
        }
    }
}