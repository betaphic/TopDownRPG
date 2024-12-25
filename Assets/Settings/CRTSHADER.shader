Shader "Custom/CRTSHADER"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ScanLineIntensity ("Scan Line Intensity", Range(0,1)) = 0.5
        _ChromaticAberration ("Chromatic Aberration", Range(0, 0.02)) = 0.002
        _Curvature ("Curvature", Range(0, 1)) = 0.1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

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
            float _ScanLineIntensity;
            float _ChromaticAberration;
            float _Curvature;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (v2f i) : SV_Target
            {
                float2 uv = i.uv;

                // Apply curvature
                uv = uv * 2.0 - 1.0;
                uv *= 1.0 + _Curvature * dot(uv, uv);
                uv = uv * 0.5 + 0.5;

                // Chromatic aberration effect
                float3 color;
                color.r = tex2D(_MainTex, uv + float2(_ChromaticAberration, 0)).r;
                color.g = tex2D(_MainTex, uv).g;
                color.b = tex2D(_MainTex, uv - float2(_ChromaticAberration, 0)).b;

                // Scan lines
                color *= 1.0 - _ScanLineIntensity * abs(sin(uv.y * 800.0));

                return float4(color, 1.0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
