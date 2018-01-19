Shader "Custom/VertexColorShader" {

	Properties {
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque" "Queue" = "Geometry" }
		Tags{ "LightMode" = "ForwardBase" }

		Lighting On
		Blend Off
		ZWrite On
		
		Fog { Mode Global }

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#pragma target 3.0
			#pragma fragmentoption ARB_precision_hint_fastest

			// compile shader into multiple variants, with and without shadows
			// (we don't care about any lightmaps yet, so skip these variants)
			#pragma multi_compile_fwdbase nolightmap nodirlightmap nodynlightmap novertexlight


			#include "./VertexColorShaderBase.cginc"
			ENDCG
		}

		// shadow casting support
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}