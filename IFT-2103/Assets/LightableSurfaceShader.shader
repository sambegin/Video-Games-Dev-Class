Shader "Custom/LightableSurfaceShader" {
	Properties{
		_MainTex("Texture", 2D) = "white" {}
	_PointsShotInfo("pointsShotInfo", 2D) = "defaulttexture" {}
	}
		SubShader{
		Tags{ "RenderType" = "Opaque" }
		CGPROGRAM
#pragma surface surf Lambert
	struct Input {
		float2 uv_MainTex;
		float2 uv_PointsShotInfo;
	};
	sampler2D _MainTex;
	sampler2D _PointsShotInfo;
	void surf(Input IN, inout SurfaceOutput o) {
		o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
		o.Albedo *= tex2D(_PointsShotInfo, IN.uv_PointsShotInfo).rgb;
	}
	ENDCG
	}
		Fallback "Diffuse"
}
