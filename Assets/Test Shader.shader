Shader "Unlit/Test Shader"
{
    Properties
    {
        _ColorA("Color A", Color) = (0,0,0,0)
        _ColorB("Color B", Color) = (1,1,1,1)
    }
        SubShader
    {
        Tags { "RenderType" = "Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            float4 _ColorA;
            float4 _ColorB;

            struct appdata
            {
                float4 vertex : POSITION;
                float3 normals : NORMAL;
                float2 uv0 : TEXCOORD0;
            };

            struct v2f
            {
                float3 normals : TEXCOORD1;
                float4 vertex : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.normals = v.normals;
                o.uv0 = v.uv0;
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                float4 outputColor = lerp(_ColorA, _ColorB, i.uv0.x);
                return outputColor;
            }
            ENDCG
        }
    }
}
