#include "UnityCG.cginc"
#include "Lighting.cginc"

// shadow helper functions and macros
#include "AutoLight.cginc"

// this gets fed to the vertex shader
struct appdata {
	float4 vertex : POSITION;
	//float3 normal : NORMAL;
	half4 color : COLOR;
	float2 uv0 : TEXCOORD0;
};

// this gets fed to the fragment shader
struct v2f {
	float4 pos : SV_POSITION;
	half4 color : COLOR;
	float2 uv : TEXCOORD0;
	//float4 posWorld : TEXCOORD1;
	//float3 normalDir : TEXCOORD2;
	//SHADOW_COORDS(4) // put shadows data into TEXCOORD N
};

v2f vert(appdata v) {
	v2f o;
	o.pos = UnityObjectToClipPos(v.vertex);
	o.color = v.color;
	
	// compute shadows data
	//TRANSFER_SHADOW(o);

	//o.posWorld = mul(unity_ObjectToWorld, v.vertex);
	//o.normalDir = normalize(mul(float4(v.normal, 0.0), unity_WorldToObject).xyz);

	o.uv = v.uv0;

	return o;
}

half4 frag(v2f i) : COLOR{
	//float3 normalDirection = i.normalDir;
	//float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
	//float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
	//float atten = 1.0;

	// Lighting
	//float3 diffuseReflection = atten * _LightColor0.xyz * saturate(dot(normalDirection, lightDirection));

	fixed4 col = i.color;
	// compute shadow attenuation (1.0 = fully lit, 0.0 = fully shadowed)
	//fixed shadow = SHADOW_ATTENUATION(i);
	// darken light's illumination with shadow, keep ambient intact
	//fixed3 lighting = diffuseReflection * shadow + UNITY_LIGHTMODEL_AMBIENT.rgb;
	//col.rgb *= lighting;

	return col;
}
