Shader "Stretched Appear Effect" {
	Properties {
		_MainTex ("Main", 2D) = "white" {}
		_Center ("Center", Vector) = (0.5, 0.5, 0, 0)
		_Rate ("Rate", Float) = 0
	}
	CGINCLUDE
		#include "UnityCG.cginc"
		uniform sampler2D _MainTex;
		uniform float2 _Center;
		uniform float _Rate;
		float4 frag (v2f_img i) : SV_TARGET
		{
			float D = 10;
			float2 uv = i.uv;
#ifdef AE_TOP_BOTTOM
			float flipY = 1 - uv.y;
			float t = min(1.0, fmod(_Rate, D) / D);
			float d = (flipY - t);
			flipY = (flipY > t) ? t - d * d / D : flipY;
			uv.y = 1 - flipY;
#endif
#ifdef AE_BOTTOM_TOP
			float t = min(1.0, fmod(_Rate, D) / D);
			uv.y = 1.0 - ((uv.y > t) ? t : uv.y);
			uv.y = 1.0 - uv.y;
#endif
			return tex2D(_MainTex, uv);
		}
	ENDCG
	SubShader {
		ZTest Off Cull Off ZWrite Off Blend Off
	  	Fog { Mode off }
		Pass {
			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag
			#pragma multi_compile AE_TOP_BOTTOM AE_BOTTOM_TOP
			ENDCG
		}
	}
	FallBack Off
}