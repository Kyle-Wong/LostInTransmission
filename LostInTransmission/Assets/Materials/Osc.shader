Shader "Unlit/OSc"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Slices ("Slices", Int) = 2 
		_Width ("WaveWidth", float) = .1
		_Amp ("Amp", float) = 1.
		_Freq ("Freq", float) = 20.
		_Phase ("Phase", float) = 20.
		_Shift ("Shift", float) = 0.
		_LBound ("Low", float) = 0.
		_HBound ("High", float) = 1.
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
			float _Width;
			int _Slices;
			float _Amp;
			float _Freq;
			float _Phase;
			float _Shift;
			float _LBound;
			float _HBound;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			float periodic(float x, float amp, float freq, float phase, float shift, float l_bound, float h_bound) {
				return clamp(((cos((x + phase) * freq) * amp + shift) + amp) / (amp * 2.), l_bound, h_bound);
			}
			
			#define EPSILON 1e-6;
			fixed4 frag (v2f i) : SV_Target
			{
				// Modified and adapted to CGPROGRAM from a shader created by inigo quilez - iq/2013: https://www.shadertoy.com/view/Xds3Rr
                // License Creative Commons Attribution-NonCommercial-ShareAlike 3.0 Unported License.
				uint s_y = uint(_ScreenParams.y) / _Slices;

				float2 uv = float2(i.uv.x, ((uint(i.uv.y * _ScreenParams.y) % s_y) * _Slices) / _ScreenParams.y);

				fixed3 col = fixed3(0., 0., 0.);
				fixed3 base_col = fixed3(1., 0., 0.);
				
				float wave = periodic(uv.x, _Amp, _Freq, _Time * _Phase, _Shift, _LBound, _HBound) * .5;

				col += base_col -  smoothstep( 0.0, _Width, abs(wave - uv.y + 0.1) );

				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, fixed4(col, 1.));
				return fixed4(col, 1.);
			}
			ENDCG
		}
	}
}
