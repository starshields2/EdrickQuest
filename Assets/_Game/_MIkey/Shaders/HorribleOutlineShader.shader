Shader "Custom/NewUnlitShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OutlineColor ("Outline Color", Color) = (0,0,0,1)
        _OutlineWidth ("Outline Width", Float) = 1
        _SearchWidth ("Search Width", Range(1,8)) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Cull Off
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_TexelSize;
            float4 _OutlineColor;
            float _OutlineWidth;
            float _SearchWidth;

            struct appdata { float4 vertex : POSITION; float2 uv : TEXCOORD0; };
            struct v2f { float2 uv : TEXCOORD0; float4 pos : SV_POSITION; };

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float GetNeigbourWithLargestAlpha(sampler2D baseTexture, float2 baseTextureUV, float2 baseTextureTexelSize, float currentAlpha, int searchWidth, float outlineWidth)
            {
                float alpha = currentAlpha;
                float2 texelSize = (outlineWidth / searchWidth) * baseTextureTexelSize.xy;

                for (int x = -searchWidth; x <= searchWidth; x++)
                {
                    for (int y = -searchWidth; y <= searchWidth; y++)
                    {
                        if (x == 0 && y == 0) continue; // Ignore this pixel.

                        float2 offset = float2(x, y) * texelSize;
                        float4 neighbour = tex2D(baseTexture, baseTextureUV + offset);

                        alpha = max(alpha, neighbour.a);
                    }
                }

                return alpha;
            }

            // Based on current pixel and largest neighbour alpha, should current pixel be an outline pixel?
            bool IsOutline(float currentAlpha, float largestNeighbourAlpha)
            {
                if (currentAlpha < 0.5 && largestNeighbourAlpha >= 0.5)
                {
                    return true;
                }

                return false;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                int searchInt = (int)max(1, floor(_SearchWidth + 0.5));
                float largest = GetNeigbourWithLargestAlpha(_MainTex, i.uv, _MainTex_TexelSize.xy, col.a, searchInt, _OutlineWidth);

                if (IsOutline(col.a, largest))
                {
                    return _OutlineColor;
                }

                return col;
            }

            ENDCG
        }
    }

    FallBack "Diffuse"
}