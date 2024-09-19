Shader "Unlit/PolygonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        Tags { "RenderType"="transparent" }
        LOD 100
        
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col;
               /* col.r = 227.0f/255.0f;//1.0f;//0.0f;
                col.g = 69.0f/255.0f;// 1.0f;
                col.b = 96.0f/255.0f;//1.0f;//0.0f;
               */ col.r = 0.0f;
                col.g = 1.0f;
                col.b = 0.0f;
/*
                 col.r = 0.0f;
                col.g = 1.0f;
                col.b = 0.0f;*/
                col.a = 0.5f;//0.45f;//0.4f;//0.3f;//0.2f;//0.4f;//0.5f;
                return col;
            }
            ENDCG
        }
    }
}
