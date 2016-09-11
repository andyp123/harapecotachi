// Shader created with Shader Forge v1.28 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.28;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False;n:type:ShaderForge.SFN_Final,id:3138,x:33136,y:32546,varname:node_3138,prsc:2|emission-7980-OUT,alpha-9624-R;n:type:ShaderForge.SFN_Color,id:6167,x:32292,y:32568,ptovrint:False,ptlb:Color1,ptin:_Color1,varname:node_6167,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.9537406,c2:0.9779412,c3:0.5393057,c4:1;n:type:ShaderForge.SFN_Color,id:9994,x:32292,y:33016,ptovrint:False,ptlb:Color2,ptin:_Color2,varname:node_9994,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.4758621,c3:0,c4:1;n:type:ShaderForge.SFN_Tex2d,id:6421,x:31683,y:32641,ptovrint:False,ptlb:Normal,ptin:_Normal,varname:node_6421,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:460d15cd692ee0f4e9e62873ee2a4b1d,ntxv:3,isnm:True;n:type:ShaderForge.SFN_Tex2d,id:9624,x:32875,y:32720,ptovrint:False,ptlb:Alpha,ptin:_Alpha,varname:node_9624,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:765bc254ab9de874b87aac811334bd2f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_LightVector,id:8800,x:31865,y:32933,varname:node_8800,prsc:2;n:type:ShaderForge.SFN_Dot,id:2745,x:32058,y:32725,varname:node_2745,prsc:2,dt:4|A-6570-OUT,B-6498-OUT;n:type:ShaderForge.SFN_Multiply,id:8572,x:32518,y:32568,varname:node_8572,prsc:2|A-6167-RGB,B-5048-OUT;n:type:ShaderForge.SFN_Add,id:7980,x:32875,y:32568,varname:node_7980,prsc:2|A-8572-OUT,B-9740-OUT;n:type:ShaderForge.SFN_OneMinus,id:4595,x:32292,y:32871,varname:node_4595,prsc:2|IN-5048-OUT;n:type:ShaderForge.SFN_Multiply,id:9740,x:32518,y:32871,varname:node_9740,prsc:2|A-4595-OUT,B-9994-RGB;n:type:ShaderForge.SFN_LightAttenuation,id:9535,x:32058,y:32869,varname:node_9535,prsc:2;n:type:ShaderForge.SFN_Multiply,id:5048,x:32292,y:32725,varname:node_5048,prsc:2|A-2745-OUT,B-9535-OUT;n:type:ShaderForge.SFN_NormalBlend,id:6570,x:31865,y:32725,varname:node_6570,prsc:2|BSE-6421-RGB,DTL-6482-OUT;n:type:ShaderForge.SFN_Vector3,id:6498,x:31865,y:32847,varname:node_6498,prsc:2,v1:0,v2:-2,v3:-1;n:type:ShaderForge.SFN_NormalVector,id:6482,x:31683,y:32797,prsc:2,pt:False;proporder:6421-9624-6167-9994;pass:END;sub:END;*/

Shader "PecoPeco/NormalMappedParticle" {
    Properties {
        _Normal ("Normal", 2D) = "bump" {}
        _Alpha ("Alpha", 2D) = "white" {}
        _Color1 ("Color1", Color) = (0.9537406,0.9779412,0.5393057,1)
        _Color2 ("Color2", Color) = (1,0.4758621,0,1)
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma multi_compile_fwdbase
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float4 _Color1;
            uniform float4 _Color2;
            uniform sampler2D _Normal; uniform float4 _Normal_ST;
            uniform sampler2D _Alpha; uniform float4 _Alpha_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float3 normalDir : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 normalDirection = i.normalDir;
////// Lighting:
                float attenuation = 1;
////// Emissive:
                float3 _Normal_var = UnpackNormal(tex2D(_Normal,TRANSFORM_TEX(i.uv0, _Normal)));
                float3 node_6570_nrm_base = _Normal_var.rgb + float3(0,0,1);
                float3 node_6570_nrm_detail = i.normalDir * float3(-1,-1,1);
                float3 node_6570_nrm_combined = node_6570_nrm_base*dot(node_6570_nrm_base, node_6570_nrm_detail)/node_6570_nrm_base.z - node_6570_nrm_detail;
                float3 node_6570 = node_6570_nrm_combined;
                float node_5048 = (0.5*dot(node_6570,float3(0,-2,-1))+0.5*attenuation);
                float3 emissive = ((_Color1.rgb*node_5048)+((1.0 - node_5048)*_Color2.rgb));
                float3 finalColor = emissive;
                float4 _Alpha_var = tex2D(_Alpha,TRANSFORM_TEX(i.uv0, _Alpha));
                return fixed4(finalColor,_Alpha_var.r);
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
