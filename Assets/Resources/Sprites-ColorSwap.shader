// Modified for the VoreWar mercenary-company conversion.
// 2026-11-02: Preserve point-sampled pixel art at native/magnified size; smooth decoded colors only during strict minification (spec 15E feedback).
// See CHANGES.md for the fork change record and License.txt for GPL-3.0 terms.
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Sprites/ColorSwap"
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
        _SmoothWhenSmall ("Smooth only when smaller than native pixels", Float) = 1


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
				float2 texcoord  : TEXCOORD0;
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

            float4 _MainTex_TexelSize;
            float _SmoothWhenSmall;

            // Never average palette indices or sample their prefiltered mip levels.
            fixed4 DecodeTexel(float2 uv)
            {
                uv = clamp(uv, _MainTex_TexelSize.xy * .5, 1 - _MainTex_TexelSize.xy * .5);
                fixed4 c = tex2Dlod(_MainTex, float4(uv, 0, 0));
                if (_AlphaSplitEnabled) c.a = tex2Dlod(_AlphaTex, float4(uv, 0, 0)).r;
                fixed4 swapCol = tex2D(_SwapTex, float2(c.r, 0));
                fixed4 result = lerp(c, swapCol, swapCol.a);
                result.a = c.a;
                result.rgb *= c.a;
                return result;
            }

            fixed4 frag(v2f IN) : SV_Target
            {
                float2 texel = IN.texcoord * _MainTex_TexelSize.zw;
                float2 dx = ddx(texel), dy = ddy(texel);
                // Smallest squared singular value: smoothing is allowed only when
                // BOTH source axes shrink, including rotated/nonuniform sprites.
                float a = dot(dx, dx), b = dot(dx, dy), c = dot(dy, dy);
                float smallest = .5 * (a + c - sqrt(max(0, (a - c) * (a - c) + 4 * b * b)));
                fixed4 result;
                if (_SmoothWhenSmall < .5 || smallest <= 1.0002)
                {
                    result = DecodeTexel((floor(texel) + .5) * _MainTex_TexelSize.xy);
                }
                else
                {
                    float2 pixel = texel - .5;
                    float2 baseUV = (floor(pixel) + .5) * _MainTex_TexelSize.xy;
                    float2 weight = frac(pixel);
                    fixed4 bottom = lerp(DecodeTexel(baseUV), DecodeTexel(baseUV + float2(_MainTex_TexelSize.x, 0)), weight.x);
                    fixed4 top = lerp(DecodeTexel(baseUV + float2(0, _MainTex_TexelSize.y)), DecodeTexel(baseUV + _MainTex_TexelSize.xy), weight.x);
                    result = lerp(bottom, top, weight.y);
                }
                result.rgb *= IN.color.rgb;
                return result;
            }

		ENDCG
		}
	}
}
