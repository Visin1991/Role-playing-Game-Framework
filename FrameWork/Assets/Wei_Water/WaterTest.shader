// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "PostEffect/Water"
{
	Properties{
		_ReflectionTex("Reflection", 2D) = "white" {}
		_RefractionTex("Refraction", 2D) = "white" {}
		_RefColor("Color",Color) = (1,1,1,1)
	}
	SubShader{
	Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Opaque" }
	ZWrite On Lighting Off Cull Off Fog{ Mode Off } Blend One Zero
	LOD 100
	Pass{
	CGPROGRAM
	#pragma vertex vert
	#pragma fragment frag
	#include "UnityCG.cginc"

	uniform float4x4 _ProjMatrix;
	uniform float _RefType;
	sampler2D _ReflectionTex;
	sampler2D _RefractionTex;
	float4 _RefColor;

	struct outvertex {
		float4 pos : SV_POSITION;
		float4 uv0 : TEXCOORD0;
		float4 refparam : COLOR0;//r:fresnel,g:none,b:none,a:none
		float4 clipSpace : WTF;
	};

	outvertex vert(appdata_tan v) {
		outvertex o;
		o.pos = UnityObjectToClipPos(v.vertex);
		o.clipSpace = o.pos;

		//float4 posProj = mul(_ProjMatrix, v.vertex);
		//o.uv0 = posProj;

		float3 r = normalize(ObjSpaceViewDir(v.vertex)); //Returns object space direction (not normalized) from given object space vertex position towards the camera.

		float d = saturate(dot(r,normalize(v.normal)));//r+(1-r)*pow(d,5)

		//d += 0.5;
		//d = clamp(d, 0,1);
		o.refparam = float4(d,0,0,0);


		return o;
	}

	float4 frag(outvertex i) : COLOR
	{
		half2 dnc = (i.clipSpace.xy / i.clipSpace.w)/2.0f + 0.5f;
		half2 coords = half2(dnc.x, dnc.y);
		coords.y = 1 - coords.y;
		half4 flecol = tex2D(_ReflectionTex, coords);
		half4 fracol = tex2D(_RefractionTex, coords);

		//half4 flecol = tex2D(_ReflectionTex, float2(i.uv0) / i.uv0.w);
		//half4 fracol = tex2D(_RefractionTex, float2(i.uv0) / i.uv0.w);


		half4 outcolor = half4(1,1,1,1);
		if (_RefType == 0)
		{
			outcolor = lerp(flecol,fracol,i.refparam.r);
		}
		else if (_RefType == 1)
		{
			outcolor = flecol;
		}
		else if (_RefType == 2)
		{
			outcolor = fracol;
		}
		return outcolor*_RefColor;
	}
		ENDCG
	}
	}
}