Shader "GameShaders/Snow"
{
    Properties
    {
        _Color ("Color", COLOR) = (1,1,1,1)
        _MainTex ("Texture", 2D) = "white" {}
        _DomainSize ("Domain Size", Float) = 10
        _Distortion ("Distortion", FLoat) = 0.05
        _DistortionScale ("Distortion Scale", FLoat) = 0.7
        _Speed ("Speed", FLoat) = 0.7
        _Size ("Size", Float) = 1
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite off
        Cull off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float3 pos : TEXCOORD1;
                //UNITY_FOG_COORDS(2)
                float4 vertex : SV_POSITION;
            };

            fixed4 _Color;
            sampler2D _MainTex;
            half _Size;
            half _DomainSize;
            half _Distortion;
            half _DistortionScale;
            half _Speed;
            float4 _MainTex_ST;

            float safe_floor(float v){
                return int(v) - step(v,0.0);
            }

            float reminder(float v1,float v2){
                return v1-safe_floor(v1/v2)*v2;
            }

            v2f vert (appdata v)
            {
                v2f o;
                v.vertex.xyz *= _DomainSize;
                v.vertex.y -= _Time.x*_DomainSize*_Speed;
                fixed3 noise = fixed3(
                            cos(6.2832*(v.vertex.y*_DistortionScale+v.vertex.x*1.23456+v.vertex.z*2.34567)/_DomainSize),
                            cos(6.2832*(v.vertex.y*_DistortionScale+v.vertex.x*12.3456+v.vertex.z*23.4567)/_DomainSize),
                            cos(6.2832*(v.vertex.y*_DistortionScale+v.vertex.x*123.456+v.vertex.z*234.567)/_DomainSize));
                v.vertex.xyz += noise*_Distortion;
                v.vertex.xyz -= mul(unity_ObjectToWorld,float4(0,0,0,1));
                v.vertex.x = reminder(v.vertex.x,_DomainSize)-_DomainSize*0.5;
                v.vertex.y = reminder(v.vertex.y,_DomainSize)-_DomainSize*0.5;
                v.vertex.z = reminder(v.vertex.z,_DomainSize)-_DomainSize*0.5;
                o.pos = mul(unity_ObjectToWorld,v.vertex);
                o.vertex = mul(UNITY_MATRIX_P, mul(UNITY_MATRIX_MV,v.vertex)
                     + float4(v.uv.x-0.5, v.uv.y-0.5,0,0)*_Size);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                //UNITY_TRANSFER_FOG(o,v.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed alpha = tex2D(_MainTex, i.uv);
                _Color.a *= min(max( (alpha-0.8),0)*15,1);
                //UNITY_APPLY_FOG(i.fogCoord, _Color);
                return _Color;
            }
            ENDCG
        }
    }
}