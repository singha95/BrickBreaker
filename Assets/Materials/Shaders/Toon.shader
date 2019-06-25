Shader "Roystan/Toon"
{
	Properties
	{
		_AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1)
		_Color("Color", Color) = (0.5, 0.65, 1, 1)
		_SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1)
		_Glossiness("Glossiness", Float) = 32
		_RimColor("Rim Color", Color) = (0,0,0,1)
		_RimAmount("Rim Amount", Range(0, 1)) = 0.1
		_MainTex("Main Texture", 2D) = "white" {}	
	}
	SubShader
	{
		Tags{ 
			"LightMode" = "ForwardBase"
			"PassFlags" = "OnlyDirectional"
		}
		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_fwdbase
			
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "AutoLight.cginc"

			struct appdata
			{
				float4 vertex : POSITION;				
				float4 uv : TEXCOORD0;
				float3 normal : NORMAL; 
			};

			struct v2f
			{
				float4 pos : SV_POSITION;
				float2 uv : TEXCOORD0;
				float3 worldNormal : NORMAL;
				float3 viewDir : TEXCOORD1;
				SHADOW_COORDS(2)
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.viewDir = WorldSpaceViewDir(v.vertex);
				TRANSFER_SHADOW(o)
				return o;
			}
			
			float4 _Color;
			float4 _AmbientColor;
			float _Glossiness;
			float4 _SpecularColor;
			float4 _RimColor;
			float _RimAmount;

			float interpolate(float intensity)
			{
				if (intensity > 0.70)
					return 1.0;
				else if (intensity > 0.5)
					return 0.7;
				else if (intensity > 0.15)
					return 0.35;
				else
					return 0.0;
			}

			float4 frag (v2f i) : SV_Target
			{
				//Shadows and normal calculations 
				float shadow = SHADOW_ATTENUATION(i);
				float4 sample = tex2D(_MainTex, i.uv);
				float3 normal= normalize(i.worldNormal); 
				float NdotL = dot(_WorldSpaceLightPos0, normal); 
				float lightIntensity = interpolate(NdotL * shadow);
				float4 light = lightIntensity * _LightColor0;

				//Specular Light calculations
				float3 viewDir = normalize(i.viewDir);
				float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir);
				float NdotH = dot(normal, halfVector);
				float ifSpecular = lightIntensity == 0.0 ? 0 : 1; 
				float specularIntensity = pow(NdotH * ifSpecular, _Glossiness * _Glossiness);
				//Smoothing of the Specular light
				float specularIntensitySmooth = smoothstep(0.05, 0.1, specularIntensity);
				float4 specular = specularIntensitySmooth * _SpecularColor;
				

				//Rim of the the object 
				float4 rimDot = 1 - dot(viewDir, normal);
				float rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimDot);
				float4 rim = rimIntensity * _RimColor;


				return _Color * sample *  (_AmbientColor + lightIntensity + specular + rim);
			}
			ENDCG
		}
		UsePass "Legacy Shaders/VertexLit/SHADOWCASTER"
	}
}