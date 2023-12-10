Shader "Custom/BlueOverlayShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _OverlayColor ("Overlay Color", Color) = (0,0,0,0)
    }
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" "IgnoreProjector"="True" }
        LOD 100

        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog
            
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            fixed4 _OverlayColor;

            v2f vert(appdata_t v) {
                v2f o;
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                return o;
            }
            
            fixed4 frag(v2f i) : SV_Target {
                fixed4 texColor = tex2D(_MainTex, i.uv);
                fixed4 overlayColor = _OverlayColor;
                
                // Überprüfe, ob die Textur transparent ist
                if (texColor.a < 0.01) discard;
                
                // Berechne die Farbe mit Overlay nur auf nicht-transparente Bereiche
                texColor.rgb = lerp(texColor.rgb, texColor.rgb * overlayColor.rgb, overlayColor.a);
                return texColor;
            }
            ENDCG
        }
    }
    FallBack "Sprite/Default"
}
