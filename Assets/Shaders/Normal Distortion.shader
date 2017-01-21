Shader "Custom/Normal Distortion" {
	Properties{
		[Header(Color)] _Color("Color", Color) = (1,1,1,1)
		_BoostColor("BoostColor", Color) = (0,0,0,0)

		[Header(PBR)] _Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0

		[Header(Rim Light)] _RimColor("Rim Color", Color) = (1,1,1,1)
		_RimFalloff("Rim Falloff", Range(0,10)) = 3.0
		_RimDistance("Rim Distance", Range(0,1)) = 1.0
		_RimScale("Rim Scale", Range(0,1)) = 1.0

		[Header(Distortion)] _Distort("Distortion", range(-128,128)) = 0.0
	}
	SubShader{
		Tags{ "Queue" = "Transparent" "RenderType" = "Opaque" }
		Blend SrcAlpha OneMinusSrcAlpha

		GrabPass{}

		CGPROGRAM
		#pragma vertex vert
		#pragma surface surf Standard alpha

		sampler2D _GrabTexture;
		float4 _GrabTexture_TexelSize;

		struct Input {
			float3 worldNormal;
			float3 viewDir;
			float4 projection : TEXCOORD;
		};

		float4 _Color;
		float4 _BoostColor;

		half _Glossiness;
		half _Metallic;

		float4 _RimColor;
		float _RimFalloff;
		float _RimDistance;
		float _RimScale;

		float _Distort;

		void vert(inout appdata_full v, out Input o) {
			UNITY_INITIALIZE_OUTPUT(Input, o);
			float4 oPos = mul(UNITY_MATRIX_MVP, v.vertex);
			#if UNITY_UV_STARTS_AT_TOP
					float scale = -1.0;
			#else
					float scale = 1.0;
			#endif
			o.projection.xy = (float2(oPos.x, oPos.y * scale) + oPos.w) * 0.5;
			o.projection.zw = oPos.zw;
		}

		void surf(Input IN, inout SurfaceOutputStandard o) {
			float3 normal = IN.worldNormal;
			float3 viewDir = normalize(IN.viewDir);

			half rim = max(0.0001, _RimDistance - saturate(dot(viewDir, normal)));

			float2 offset = normal * _Distort * _GrabTexture_TexelSize.xy;
			IN.projection.x += offset.x * IN.projection.z;
			IN.projection.y -= offset.y * IN.projection.z;
			half4 color = tex2Dproj(_GrabTexture, UNITY_PROJ_COORD(IN.projection));

			o.Emission = _BoostColor + _RimColor.rgb * pow(rim, _RimFalloff) * _RimScale;
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Albedo = _Color.rgb * color.rgb;
			o.Alpha = _Color.a;
		}
		ENDCG
	}
		FallBack "Diffuse"
}