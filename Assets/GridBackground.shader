Shader "Unlit/GridBackground"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _LineColor ("Line Color", Color) = (0, 0, 1, 1)
    }
    SubShader
    {
        Tags { "Queue" = "Background" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _LineColor;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                float2 grid = frac(i.uv * 10); // Creare quadrati 1x1
                float lineWidth = 0.05;
                if (grid.x < lineWidth || grid.y < lineWidth)
                {
                    return _LineColor;
                }
                return fixed4(1, 1, 1, 1); // Sfondo bianco
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
