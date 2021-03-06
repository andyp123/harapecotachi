// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:1,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:32726,y:32531,varname:node_3138,prsc:2|emission-7901-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:31965,y:32436,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_ViewReflectionVector,id:5466,x:31046,y:32945,varname:node_5466,prsc:2;n:type:ShaderForge.SFN_Dot,id:9254,x:31219,y:32945,varname:node_9254,prsc:2,dt:3|A-5466-OUT,B-9807-OUT;n:type:ShaderForge.SFN_Slider,id:6125,x:31062,y:32860,ptovrint:False,ptlb:SpecularPower,ptin:_SpecularPower,varname:node_6125,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:20;n:type:ShaderForge.SFN_Power,id:9620,x:31388,y:32860,varname:node_9620,prsc:2|VAL-9254-OUT,EXP-6125-OUT;n:type:ShaderForge.SFN_Color,id:9596,x:32059,y:32841,ptovrint:False,ptlb:SpecularColor,ptin:_SpecularColor,varname:_ShadowColor_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:5418,x:32059,y:32999,varname:node_5418,prsc:2|A-9620-OUT,B-9596-RGB;n:type:ShaderForge.SFN_Tex2dAsset,id:1197,x:31789,y:32827,ptovrint:False,ptlb:Ramp,ptin:_Ramp,varname:node_1197,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Append,id:5758,x:31620,y:32999,varname:node_5758,prsc:2|A-9620-OUT,B-6448-OUT;n:type:ShaderForge.SFN_Tex2d,id:1124,x:31789,y:32999,varname:node_1124,prsc:2,ntxv:0,isnm:False|UVIN-5758-OUT,TEX-1197-TEX;n:type:ShaderForge.SFN_Add,id:7901,x:32532,y:32631,varname:node_7901,prsc:2|A-796-OUT,B-5418-OUT;n:type:ShaderForge.SFN_Vector1,id:6448,x:31620,y:33131,varname:node_6448,prsc:2,v1:0;n:type:ShaderForge.SFN_Fresnel,id:9738,x:31965,y:32578,varname:node_9738,prsc:2|NRM-1158-OUT,EXP-8498-OUT;n:type:ShaderForge.SFN_Multiply,id:796,x:32149,y:32578,varname:node_796,prsc:2|A-7241-RGB,B-9738-OUT,C-8498-OUT;n:type:ShaderForge.SFN_LightVector,id:9807,x:31046,y:33069,varname:node_9807,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:8498,x:31965,y:32721,ptovrint:False,ptlb:FresnelPower,ptin:_FresnelPower,varname:node_8498,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_NormalVector,id:4770,x:31046,y:33187,prsc:2,pt:False;n:type:ShaderForge.SFN_NormalVector,id:9515,x:31376,y:32371,prsc:2,pt:True;n:type:ShaderForge.SFN_Add,id:6545,x:31565,y:32381,varname:node_6545,prsc:2|A-9515-OUT,B-1224-XYZ;n:type:ShaderForge.SFN_Normalize,id:1158,x:31733,y:32381,varname:node_1158,prsc:2|IN-6545-OUT;n:type:ShaderForge.SFN_Vector4Property,id:1224,x:31376,y:32545,ptovrint:False,ptlb:FresnelOffset,ptin:_FresnelOffset,varname:node_1224,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0,v2:0,v3:0,v4:0;proporder:7241-6125-9596-1197-8498-1224;pass:END;sub:END;*/

Shader "PecoPeco/HelmetGlass" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _SpecularPower ("SpecularPower", Range(0, 20)) = 2
        _SpecularColor ("SpecularColor", Color) = (1,1,1,1)
        _Ramp ("Ramp", 2D) = "white" {}
        _FresnelPower ("FresnelPower", Float ) = 2
        _FresnelOffset ("FresnelOffset", Vector) = (0,0,0,0)
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Front
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color;
            uniform float _SpecularPower;
            uniform float4 _SpecularColor;
            uniform float _FresnelPower;
            uniform float4 _FresnelOffset;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float4 posWorld : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.normalDir = UnityObjectToWorldNormal(-v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos(v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
////// Lighting:
////// Emissive:
                float node_9620 = pow(abs(dot(viewReflectDirection,lightDirection)),_SpecularPower);
                float3 emissive = ((_Color.rgb*pow(1.0-max(0,dot(normalize((normalDirection+_FresnelOffset.rgb)), viewDirection)),_FresnelPower)*_FresnelPower)+(node_9620*_SpecularColor.rgb));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
