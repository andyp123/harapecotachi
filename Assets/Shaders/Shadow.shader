// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:4,bdst:1,dpts:2,wrdp:False,dith:0,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:False,igpj:False,qofs:1,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:-1,ofsu:2,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33537,y:32409,varname:node_3138,prsc:2|emission-8321-OUT;n:type:ShaderForge.SFN_Slider,id:5667,x:32411,y:32682,ptovrint:False,ptlb:Max,ptin:_Max,varname:_Min_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_RemapRangeAdvanced,id:5807,x:32797,y:32513,varname:node_5807,prsc:2|IN-9154-OUT,IMIN-8910-OUT,IMAX-5667-OUT,OMIN-8910-OUT,OMAX-7510-OUT;n:type:ShaderForge.SFN_Vector1,id:8910,x:32596,y:32412,varname:node_8910,prsc:2,v1:0;n:type:ShaderForge.SFN_Vector1,id:7510,x:32596,y:32462,varname:node_7510,prsc:2,v1:1;n:type:ShaderForge.SFN_Clamp,id:9154,x:32596,y:32513,varname:node_9154,prsc:2|IN-8866-OUT,MIN-8910-OUT,MAX-5667-OUT;n:type:ShaderForge.SFN_TexCoord,id:1861,x:31872,y:32522,varname:node_1861,prsc:2,uv:0;n:type:ShaderForge.SFN_RemapRange,id:8657,x:32044,y:32522,varname:node_8657,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-1861-UVOUT;n:type:ShaderForge.SFN_Length,id:1476,x:32206,y:32522,varname:node_1476,prsc:2|IN-8657-OUT;n:type:ShaderForge.SFN_Color,id:9962,x:32797,y:32368,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_9962,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_OneMinus,id:8866,x:32378,y:32522,varname:node_8866,prsc:2|IN-1476-OUT;n:type:ShaderForge.SFN_OneMinus,id:1994,x:32989,y:32513,varname:node_1994,prsc:2|IN-5807-OUT;n:type:ShaderForge.SFN_Multiply,id:6858,x:32989,y:32378,varname:node_6858,prsc:2|A-9962-RGB,B-5807-OUT;n:type:ShaderForge.SFN_Add,id:2909,x:33180,y:32513,varname:node_2909,prsc:2|A-6858-OUT,B-1994-OUT;n:type:ShaderForge.SFN_Clamp01,id:8321,x:33343,y:32513,varname:node_8321,prsc:2|IN-2909-OUT;proporder:5667-9962;pass:END;sub:END;*/

Shader "PecoPeco/Shadow" {
    Properties {
        _Max ("Max", Range(0, 1)) = 1
        _Color ("Color", Color) = (0,0,0,1)
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
            Blend DstColor Zero
            ZWrite Off
            Offset -1, 2
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _Max;
            uniform float4 _Color;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float2 node_8657 = (i.uv0*2.0+-1.0);
                float node_8910 = 0.0;
                float node_5807 = (node_8910 + ( (clamp((1.0 - length(node_8657)),node_8910,_Max) - node_8910) * (1.0 - node_8910) ) / (_Max - node_8910));
                float3 emissive = saturate(((_Color.rgb*node_5807)+(1.0 - node_5807)));
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
