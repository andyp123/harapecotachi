// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:False,qofs:1,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:-1,ofsu:2,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33118,y:32504,varname:node_3138,prsc:2|diff-5556-OUT,alpha-5755-OUT;n:type:ShaderForge.SFN_Tex2d,id:9624,x:32568,y:32504,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:node_9624,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9a0c8666d1881a64686329bf74bf2f75,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Multiply,id:5556,x:32783,y:32504,varname:node_5556,prsc:2|A-9624-RGB,B-7717-RGB;n:type:ShaderForge.SFN_Color,id:7717,x:32568,y:32692,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7717,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.5,c2:0.5,c3:0.5,c4:1;n:type:ShaderForge.SFN_TexCoord,id:3344,x:32087,y:32915,varname:node_3344,prsc:2,uv:0;n:type:ShaderForge.SFN_OneMinus,id:1978,x:32087,y:33057,varname:node_1978,prsc:2|IN-3344-V;n:type:ShaderForge.SFN_Multiply,id:3979,x:32296,y:32990,varname:node_3979,prsc:2|A-3344-V,B-1978-OUT,C-1785-OUT;n:type:ShaderForge.SFN_Power,id:2475,x:32632,y:32989,varname:node_2475,prsc:2|VAL-4767-OUT,EXP-2688-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1785,x:32296,y:33136,ptovrint:False,ptlb:Strength,ptin:_Strength,varname:node_1785,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:8;n:type:ShaderForge.SFN_ValueProperty,id:2688,x:32632,y:33135,ptovrint:False,ptlb:EdgeFade,ptin:_EdgeFade,varname:node_2688,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Slider,id:1680,x:32475,y:32919,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_1680,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:5755,x:32927,y:32920,varname:node_5755,prsc:2|A-1680-OUT,B-2475-OUT,C-7527-OUT;n:type:ShaderForge.SFN_TexCoord,id:4877,x:32493,y:33223,varname:node_4877,prsc:2,uv:1;n:type:ShaderForge.SFN_Add,id:3549,x:32844,y:33233,varname:node_3549,prsc:2|A-6380-OUT,B-5627-OUT;n:type:ShaderForge.SFN_Slider,id:640,x:32475,y:33401,ptovrint:False,ptlb:PathDistanceFade,ptin:_PathDistanceFade,varname:node_640,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_RemapRange,id:5627,x:32844,y:33361,varname:node_5627,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-640-OUT;n:type:ShaderForge.SFN_Clamp01,id:3059,x:33022,y:33233,varname:node_3059,prsc:2|IN-3549-OUT;n:type:ShaderForge.SFN_Power,id:7527,x:33201,y:33233,varname:node_7527,prsc:2|VAL-3059-OUT,EXP-9343-OUT;n:type:ShaderForge.SFN_Vector1,id:9343,x:33201,y:33355,varname:node_9343,prsc:2,v1:32;n:type:ShaderForge.SFN_OneMinus,id:6380,x:32661,y:33233,varname:node_6380,prsc:2|IN-4877-U;n:type:ShaderForge.SFN_Clamp01,id:4767,x:32463,y:32989,varname:node_4767,prsc:2|IN-3979-OUT;proporder:9624-7717-2688-1785-1680-640;pass:END;sub:END;*/

Shader "PecoPeco/Path" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _Color ("Color", Color) = (0.5,0.5,0.5,1)
        _EdgeFade ("EdgeFade", Float ) = 2
        _Strength ("Strength", Float ) = 8
        _Opacity ("Opacity", Range(0, 1)) = 1
        _PathDistanceFade ("PathDistanceFade", Range(0, 1)) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="Geometry+1"
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            Offset -1, 2
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _LightColor0;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _Color;
            uniform float _Strength;
            uniform float _EdgeFade;
            uniform float _Opacity;
            uniform float _PathDistanceFade;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                LIGHTING_COORDS(4,5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos(v.vertex );
                TRANSFER_VERTEX_TO_FRAGMENT(o)
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
/////// Diffuse:
                float NdotL = max(0.0,dot( normalDirection, lightDirection ));
                float3 directDiffuse = max( 0.0, NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += UNITY_LIGHTMODEL_AMBIENT.rgb; // Ambient Light
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(i.uv0, _MainTex));
                float3 diffuseColor = (_MainTex_var.rgb*_Color.rgb);
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse;
                return fixed4(finalColor,(_Opacity*pow(saturate((i.uv0.g*(1.0 - i.uv0.g)*_Strength)),_EdgeFade)*pow(saturate(((1.0 - i.uv1.r)+(_PathDistanceFade*2.0+-1.0))),32.0)));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
