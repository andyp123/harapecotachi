// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33014,y:32525,varname:node_3138,prsc:2|emission-5095-OUT,custl-9752-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:31800,y:32455,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Dot,id:6092,x:31443,y:33194,varname:node_6092,prsc:2,dt:1|A-2419-OUT,B-2322-OUT;n:type:ShaderForge.SFN_LightVector,id:2419,x:31270,y:33194,varname:node_2419,prsc:2;n:type:ShaderForge.SFN_NormalVector,id:2322,x:31270,y:33315,prsc:2,pt:True;n:type:ShaderForge.SFN_Append,id:2422,x:31626,y:33194,varname:node_2422,prsc:2|A-6092-OUT,B-4820-OUT;n:type:ShaderForge.SFN_Vector1,id:4820,x:31443,y:33343,varname:node_4820,prsc:2,v1:0;n:type:ShaderForge.SFN_Tex2d,id:700,x:31806,y:33194,varname:node_700,prsc:2,tex:b973173210093294aab5dca3d00e8280,ntxv:0,isnm:False|UVIN-2422-OUT,TEX-1197-TEX;n:type:ShaderForge.SFN_LightAttenuation,id:4176,x:31806,y:33325,varname:node_4176,prsc:2;n:type:ShaderForge.SFN_LightColor,id:1401,x:31806,y:33452,varname:node_1401,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4187,x:32059,y:33194,varname:node_4187,prsc:2|A-6092-OUT,B-4176-OUT,C-1401-RGB;n:type:ShaderForge.SFN_Color,id:4600,x:32260,y:33338,ptovrint:False,ptlb:ShadowColor,ptin:_ShadowColor,varname:_Color_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_OneMinus,id:1748,x:32260,y:33194,varname:node_1748,prsc:2|IN-4187-OUT;n:type:ShaderForge.SFN_Multiply,id:9171,x:32441,y:33194,varname:node_9171,prsc:2|A-1748-OUT,B-4600-RGB;n:type:ShaderForge.SFN_Multiply,id:2720,x:32030,y:32614,varname:node_2720,prsc:2|A-7241-RGB,B-6783-RGB;n:type:ShaderForge.SFN_Add,id:6995,x:32518,y:32617,varname:node_6995,prsc:2|A-8219-OUT,B-9171-OUT,C-8411-OUT;n:type:ShaderForge.SFN_ViewReflectionVector,id:5466,x:31046,y:32945,varname:node_5466,prsc:2;n:type:ShaderForge.SFN_Dot,id:9254,x:31219,y:32945,varname:node_9254,prsc:2,dt:1|A-5466-OUT,B-2419-OUT;n:type:ShaderForge.SFN_Slider,id:6125,x:31062,y:32860,ptovrint:False,ptlb:SpecularPower,ptin:_SpecularPower,varname:node_6125,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:20;n:type:ShaderForge.SFN_Power,id:9620,x:31388,y:32860,varname:node_9620,prsc:2|VAL-9254-OUT,EXP-6125-OUT;n:type:ShaderForge.SFN_Color,id:9596,x:32059,y:32855,ptovrint:False,ptlb:SpecularColor,ptin:_SpecularColor,varname:_ShadowColor_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:5418,x:32059,y:32999,varname:node_5418,prsc:2|A-1401-RGB,B-1124-RGB,C-9596-RGB,D-4187-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:1197,x:31800,y:32805,ptovrint:False,ptlb:Ramp,ptin:_Ramp,varname:node_1197,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b973173210093294aab5dca3d00e8280,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Append,id:5758,x:31620,y:32999,varname:node_5758,prsc:2|A-9620-OUT,B-4820-OUT;n:type:ShaderForge.SFN_Tex2d,id:1124,x:31789,y:32999,varname:node_1124,prsc:2,tex:b973173210093294aab5dca3d00e8280,ntxv:0,isnm:False|UVIN-5758-OUT,TEX-1197-TEX;n:type:ShaderForge.SFN_Tex2d,id:6783,x:31800,y:32614,ptovrint:False,ptlb:Diffuse,ptin:_Diffuse,varname:node_6783,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:7b0dfa6d118af5446aa8851c806ad5aa,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:8219,x:32231,y:32458,varname:node_8219,prsc:2|A-5287-RGB,B-2720-OUT,C-547-OUT;n:type:ShaderForge.SFN_AmbientLight,id:5287,x:32030,y:32458,varname:node_5287,prsc:2;n:type:ShaderForge.SFN_Multiply,id:6233,x:32299,y:32999,varname:node_6233,prsc:2|A-2720-OUT,B-4187-OUT;n:type:ShaderForge.SFN_Add,id:7901,x:32518,y:32771,varname:node_7901,prsc:2|A-5418-OUT,B-6233-OUT;n:type:ShaderForge.SFN_Slider,id:547,x:32030,y:32367,ptovrint:False,ptlb:Ambient,ptin:_Ambient,varname:node_547,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Fresnel,id:1286,x:32538,y:33387,varname:node_1286,prsc:2;n:type:ShaderForge.SFN_OneMinus,id:8393,x:33251,y:33464,varname:node_8393,prsc:2|IN-4765-OUT;n:type:ShaderForge.SFN_Vector1,id:5346,x:32709,y:33321,varname:node_5346,prsc:2,v1:7.5;n:type:ShaderForge.SFN_Multiply,id:5095,x:32735,y:32617,varname:node_5095,prsc:2|A-6995-OUT,B-5162-OUT;n:type:ShaderForge.SFN_Multiply,id:9752,x:32735,y:32771,varname:node_9752,prsc:2|A-7901-OUT,B-5162-OUT;n:type:ShaderForge.SFN_Multiply,id:4372,x:32709,y:33387,varname:node_4372,prsc:2|A-1286-OUT,B-909-OUT;n:type:ShaderForge.SFN_Power,id:4523,x:32882,y:33387,varname:node_4523,prsc:2|VAL-4372-OUT,EXP-9180-OUT;n:type:ShaderForge.SFN_Vector1,id:909,x:32709,y:33511,varname:node_909,prsc:2,v1:1.25;n:type:ShaderForge.SFN_Step,id:4765,x:33073,y:33387,varname:node_4765,prsc:2|A-4523-OUT,B-1813-OUT;n:type:ShaderForge.SFN_Vector1,id:1813,x:33073,y:33334,varname:node_1813,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Color,id:526,x:33251,y:33611,ptovrint:False,ptlb:OutlineColor,ptin:_OutlineColor,varname:node_526,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:6209,x:33454,y:33464,varname:node_6209,prsc:2|A-8393-OUT,B-526-RGB,C-2720-OUT;n:type:ShaderForge.SFN_Add,id:5162,x:33454,y:33330,varname:node_5162,prsc:2|A-4765-OUT,B-6209-OUT;n:type:ShaderForge.SFN_Slider,id:9180,x:32738,y:33205,ptovrint:False,ptlb:OutlineWidth,ptin:_OutlineWidth,varname:node_9180,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:1,cur:7.5,max:20;n:type:ShaderForge.SFN_Fresnel,id:8344,x:32342,y:32182,varname:node_8344,prsc:2|EXP-6724-OUT;n:type:ShaderForge.SFN_Vector1,id:5824,x:32342,y:32302,varname:node_5824,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:8411,x:32528,y:32182,varname:node_8411,prsc:2|A-8344-OUT,B-5824-OUT,C-2720-OUT;n:type:ShaderForge.SFN_Vector1,id:6724,x:32168,y:32206,varname:node_6724,prsc:2,v1:2;proporder:7241-4600-6125-9596-1197-6783-547-526-9180;pass:END;sub:END;*/

Shader "PecoPeco/PlayerToon" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _ShadowColor ("ShadowColor", Color) = (0,0,0,1)
        _SpecularPower ("SpecularPower", Range(0, 20)) = 2
        _SpecularColor ("SpecularColor", Color) = (1,1,1,1)
        _Ramp ("Ramp", 2D) = "white" {}
        _Diffuse ("Diffuse", 2D) = "white" {}
        _Ambient ("Ambient", Range(0, 1)) = 1
        _OutlineColor ("OutlineColor", Color) = (0,0,0,1)
        _OutlineWidth ("OutlineWidth", Range(1, 20)) = 7.5
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float4 _ShadowColor;
            uniform float _SpecularPower;
            uniform float4 _SpecularColor;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _Ambient;
            uniform float4 _OutlineColor;
            uniform float _OutlineWidth;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
////// Emissive:
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 node_2720 = (_Color.rgb*_Diffuse_var.rgb);
                float node_6092 = max(0,dot(lightDirection,normalDirection));
                float3 node_4187 = (node_6092*attenuation*_LightColor0.rgb);
                float node_4765 = step(pow(((1.0-max(0,dot(normalDirection, viewDirection)))*1.25),_OutlineWidth),0.5);
                float3 node_5162 = (node_4765+((1.0 - node_4765)*_OutlineColor.rgb*node_2720));
                float3 emissive = (((UNITY_LIGHTMODEL_AMBIENT.rgb*node_2720*_Ambient)+((1.0 - node_4187)*_ShadowColor.rgb)+(pow(1.0-max(0,dot(normalDirection, viewDirection)),2.0)*0.5*node_2720))*node_5162);
                float node_4820 = 0.0;
                float2 node_5758 = float2(pow(max(0,dot(viewReflectDirection,lightDirection)),_SpecularPower),node_4820);
                float4 node_1124 = tex2D(_Ramp,TRANSFORM_TEX(node_5758, _Ramp));
                float3 finalColor = emissive + (((_LightColor0.rgb*node_1124.rgb*_SpecularColor.rgb*node_4187)+(node_2720*node_4187))*node_5162);
                return fixed4(finalColor,1);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdadd_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float4 _ShadowColor;
            uniform float _SpecularPower;
            uniform float4 _SpecularColor;
            uniform sampler2D _Ramp; uniform float4 _Ramp_ST;
            uniform sampler2D _Diffuse; uniform float4 _Diffuse_ST;
            uniform float _Ambient;
            uniform float4 _OutlineColor;
            uniform float _OutlineWidth;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                LIGHTING_COORDS(3,4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float node_4820 = 0.0;
                float2 node_5758 = float2(pow(max(0,dot(viewReflectDirection,lightDirection)),_SpecularPower),node_4820);
                float4 node_1124 = tex2D(_Ramp,TRANSFORM_TEX(node_5758, _Ramp));
                float node_6092 = max(0,dot(lightDirection,normalDirection));
                float3 node_4187 = (node_6092*attenuation*_LightColor0.rgb);
                float4 _Diffuse_var = tex2D(_Diffuse,TRANSFORM_TEX(i.uv0, _Diffuse));
                float3 node_2720 = (_Color.rgb*_Diffuse_var.rgb);
                float node_4765 = step(pow(((1.0-max(0,dot(normalDirection, viewDirection)))*1.25),_OutlineWidth),0.5);
                float3 node_5162 = (node_4765+((1.0 - node_4765)*_OutlineColor.rgb*node_2720));
                float3 finalColor = (((_LightColor0.rgb*node_1124.rgb*_SpecularColor.rgb*node_4187)+(node_2720*node_4187))*node_5162);
                return fixed4(finalColor * 1,0);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
