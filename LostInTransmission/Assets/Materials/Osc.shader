Shader "Unlit/OSc"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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

			float periodic(float x, float amp, float freq, float phase, float shift, float bound) {
				return clamp(((cos((x + phase) * freq) * amp + shift) + 1.) / 2., 0., bound);
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				// Modified and adapted to CGPROGRAM from a shader created by inigo quilez - iq/2013: https://www.shadertoy.com/view/Xds3Rr
                // License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
				// sample the texture
				fixed3 col = fixed3(0., 0., 0.);
				fixed3 base_col = fixed3(1., 0., 0.);

				float wave = periodic(i.uv.x, .2, 20., _Time * 20., .5, 1.) * .5;
				
				float width = .01;

				col += base_col -  smoothstep( 0.0, width, abs(wave - i.uv.y + 0.1) );

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, fixed4(col, 1.));
				return fixed4(col, 1.);
			}
			ENDCG
		}
	}
}
