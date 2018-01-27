Shader "Unlit/Ball"
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
				// sample the texture
				float2 center = float2(.5, .5);
				float r = distance(i.uv, center);
				float radius = .45;
				float3 base_col = float3(1., 1., 1.);
				float3 col = base_col * smoothstep(0., 1., r / radius) * periodic(_Time, 1., 1., 0, 0., 1.);
				float4 rgba = float4(col, 1);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, rgba);
				return rgba;
			}
			ENDCG
		}
	}
}
