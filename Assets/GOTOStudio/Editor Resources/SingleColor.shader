// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Unlit/SingleColor"
{
	Properties
	{
		_Color ("Main Color", Color) = (1,1,1,1)
		_XMin ("XMin", Range(0.0, 2000.0)) = 0
		_YMin ("YMin", Range(0.0, 1000.0)) = 0
		_XMax ("XMax", Range(0.0, 2000.0)) = 2000
		_YMax ("YMax", Range(0.0, 1000.0)) = 1000
	}
	SubShader
	{
		Tags {"Queue"="Transparent" "RenderType"="Transparent" }
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"

			struct appdata_t
			{
				float4 vertex   : POSITION;
			};

			struct v2f
			{
				float4 vertex    : SV_POSITION;
				float2 screenPos : TEXCOORD1;
			};

			v2f vert (appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.screenPos.xy = ComputeScreenPos(OUT.vertex).xy * _ScreenParams.xy;
				return OUT;
			}

			fixed4 _Color;
			float _XMin;
			float _YMin;
			float _XMax;
			float _YMax;

			fixed4 frag (v2f IN) : SV_Target
			{
				return _Color * step(_XMin, IN.vertex.x) * step(IN.vertex.x, _XMax) * step(_YMin, IN.vertex.y) * step(IN.vertex.y, _YMax);
			}
			ENDCG
		}
	}
}