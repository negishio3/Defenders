﻿Shader "Custom/SilhouetteOutline" {
	Properties{
		_Color("Main Color", Color) = (.5,.5,.5,1)
		_PlayerColor("Player Color",Color) = (0,0,0,1)
		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		_Outline("Outline width", Range(.001, 0.03)) = .001
		_MainTex("Base (RGB)", 2D) = "white" { }
		_ToonShade("ToonShader Cubemap(RGB)", CUBE) = "" { }
	}

		CGINCLUDE
#include "UnityCG.cginc"

		struct appdata {
		float4 vertex : POSITION;
		float3 normal : NORMAL;
	};

	struct v2f {
		float4 pos : SV_POSITION;
		UNITY_FOG_COORDS(0)
			fixed4 color : COLOR;
	};

	uniform float _Outline;
	uniform float4 _OutlineColor;

	v2f vert(appdata v) {
		v2f o;
		o.pos = UnityObjectToClipPos(v.vertex);

		float3 norm = normalize(mul((float3x3)UNITY_MATRIX_IT_MV, v.normal));
		float2 offset = TransformViewToProjection(norm.xy);

#ifdef UNITY_Z_0_FAR_FROM_CLIPSPACE //to handle recent standard asset package on older version of unity (before 5.5)
		o.pos.xy += offset * UNITY_Z_0_FAR_FROM_CLIPSPACE(o.pos.z) * _Outline;
#else
		o.pos.xy += offset * o.pos.z * _Outline;
#endif
		o.color = _OutlineColor;
		//o.color = _PlayerColor;
		UNITY_TRANSFER_FOG(o, o.pos);
		return o;
	}
	ENDCG

		SubShader{
			Tags { "RenderType" = "Opaque" }

			Pass {
		// Don't write to the depth buffer for the silhouette pass
		ZWrite Off
		ZTest Always

		// First Pass: Silhouette
		CGPROGRAM

		#pragma vertex vert
		#pragma fragment frag

		#include "UnityCG.cginc"

		float4 _PlayerColor;

		struct vertInput {
			float4 vertex:POSITION;
			float3 normal:NORMAL;
		};

		struct fragInput {
			float4 pos:SV_POSITION;
		};

		fragInput vert(vertInput i) {
			fragInput o;
			o.pos = UnityObjectToClipPos(i.vertex);
			return o;
		}

		float4 frag(fragInput i) : COLOR {
			return _PlayerColor;
		}

		ENDCG
	}

		// Second Pass: Standard
		CGPROGRAM

		#pragma surface surf Lambert
		#include "UnityCG.cginc"

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			float4 col = tex2D(_MainTex, IN.uv_MainTex);
			o.Albedo = col.rgb;
		}

		ENDCG
		Pass{
			Name "OUTLINE"
			Tags { "LightMode" = "Always" }
			Cull Front
			ZWrite On
			ColorMask RGB
			Blend SrcAlpha OneMinusSrcAlpha

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fog
			fixed4 frag(v2f i) : SV_Target
			{
				UNITY_APPLY_FOG(i.fogCoord, i.color);
				return i.color;
			}
			ENDCG
		}
		UsePass "Toon/Basic/BASE"
	}

		FallBack "Diffuse"

				Fallback "Toon/Basic"

}
