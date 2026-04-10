Shader "UI/StencilCutoutAlpha"
{
    Properties 
    { 
        _MainTex("Texture", 2D) = "white" {} 
        _Cutoff("Alpha Cutoff", Range(0,1)) = 0.5
    }
    SubShader 
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        Cull Off 
        ZWrite Off
        Blend SrcAlpha OneMinusSrcAlpha

        Stencil 
        {
            Ref 1
            Comp Always
            Pass Replace
        }

        Pass 
        {
            ColorMask 0 // Still invisible, only writing to Stencil
            ZTest Always

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata 
            { 
                float4 vertex : POSITION; 
                float2 uv : TEXCOORD0; 
                float4 color : COLOR; 
            };

            struct v2f 
            { 
                float4 pos : SV_POSITION; 
                float2 uv : TEXCOORD0; 
                float4 color : COLOR; 
            };

            sampler2D _MainTex; 
            float4 _MainTex_ST;
            fixed _Cutoff;

            v2f vert(appdata v) 
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.color = v.color;
                return o;
            }

            fixed4 frag(v2f i) : SV_Target 
            {
                fixed4 tex = tex2D(_MainTex, i.uv) * i.color;
                
                // This is the magic line:
                // If alpha - _Cutoff < 0, the pixel is discarded 
                // and the stencil buffer is NOT updated.
                clip(tex.a - _Cutoff);
                
                return tex;
            }
            ENDCG
        }
    }
}