// Added by the VoreWar mercenary-company conversion fork.
// 2026-11-02: Comparison-only palette-aware smoothing, derived from Assets/Resources/Sprites-ColorSwap.shader (spec 15E).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/PortraitPaletteSmooth"
{
	Properties
	{
		[PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
		_SwapTex("Color Data", 2D) = "transparent" {}
		_Color ("Tint", Color) = (1,1,1,1)
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0

		_StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

		_ColorMask ("Color Mask", Float) = 15
        _PortraitUVRect ("Sprite bounds", Vector) = (0,0,1,1)


	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		 Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp]
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }


		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		ColorMask [_ColorMask]



		Pass
		{
		CGPROGRAM
			#pragma target 3.0
            #pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				half2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			float _AlphaSplitEnabled;

			sampler2D _SwapTex;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv);
				if (_AlphaSplitEnabled)
					color.a = tex2D (_AlphaTex, uv).r;

				return color;
			}

            float4 _MainTex_TexelSize;
            float4 _PortraitUVRect;
            fixed4 Decode(float2 uv)
            {
                uv = clamp(uv, _PortraitUVRect.xy + _MainTex_TexelSize.xy * .5,
                    _PortraitUVRect.zw - _MainTex_TexelSize.xy * .5);
                fixed4 c = tex2Dlod(_MainTex, float4(uv, 0, 0));
                if (_AlphaSplitEnabled) c.a = tex2Dlod(_AlphaTex, float4(uv, 0, 0)).r;
                fixed4 replacement = tex2D(_SwapTex, float2(c.r, 0));
                fixed4 result = lerp(c, replacement, replacement.a);
                result.a = c.a;
                result.rgb *= c.a;
                return result;
            }
            fixed4 frag(v2f IN) : SV_Target
            {
                float2 pixel = IN.texcoord * _MainTex_TexelSize.zw - .5;
                float2 baseUV = (floor(pixel) + .5) * _MainTex_TexelSize.xy;
                float2 weight = frac(pixel);
                fixed4 bottom = lerp(Decode(baseUV), Decode(baseUV + float2(_MainTex_TexelSize.x, 0)), weight.x);
                fixed4 top = lerp(Decode(baseUV + float2(0, _MainTex_TexelSize.y)), Decode(baseUV + _MainTex_TexelSize.xy), weight.x);
                fixed4 result = lerp(bottom, top, weight.y);
                result.rgb *= IN.color.rgb;
                return result;
            }

		ENDCG
		}
	}
}
