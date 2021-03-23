// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Custom/Block"{
Properties {
    _Color1 ("Color 1", COLOR) = (1,1,1,1)
    _Color2 ("Color 2", COLOR) = (1,1,1,1)
    _Width ("Width", Range (0.0, 5)) = 0.5
    _Bias ("Bias", Range (0.0, 1)) = 0.5
	[Space]
    _Glossy ("Glossy", Range (0.0, 3)) = 0.5
    _Metallic ("Metallic", Range(0,1)) = 0
    _Ambient ("Ambient", Range(0,1)) = 0.2
    [Toggle]_UseNoise ("Use Noise", FLOAT) = 1
    _Noise ("Noise Strength", Range(0,1)) = 0.2
    _NoiseScale ("Noise Scale", FLOAT) = 25
    _NoiseType ("Noise Type", Range(0,3)) = 0
}
SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 250

    CGPROGRAM
    #pragma target 3.0
    #pragma surface surf MobileBlinnPhong vertex:vert exclude_path:prepass nolightmap noambient noforwardadd interpolateview

    half _Glossy;
    half _Metallic;
    half _Ambient;
    half _Noise;
    half _NoiseScale;
    half _UseNoise;
    half _NoiseType;
    fixed4 _Color1;
    fixed4 _Color2;
    half _Width;
    half _Bias;
    
    samplerCUBE _HdriGlobal;
    sampler2D _NoiseGlobal;

    struct CustomSurfaceOutput {
        fixed3 Albedo;
        fixed3 Normal;
        fixed3 Emission;
        fixed Specular;
        fixed Gloss;
        fixed Alpha;
        fixed3 worldPos;
    };

    inline fixed4 LightingMobileBlinnPhong (CustomSurfaceOutput s, fixed3 lightDir, fixed atten)
    {
        fixed3 normal =  normalize(s.Normal);
        fixed3 reflDir = reflect(normalize(s.worldPos - _WorldSpaceCameraPos),normal);
        fixed diff = max (0, dot (normal, lightDir));
        fixed nh = max (0, dot (reflDir, lightDir));
        fixed spec = pow (nh, 30) * s.Gloss;

        fixed4 reflection = texCUBE(_HdriGlobal, reflDir) * s.Gloss*0.2;

        fixed4 c;
        c.rgb = s.Albedo * (_LightColor0.rgb * diff * atten*(1-s.Specular)+_Ambient) + 
            _LightColor0.rgb * spec * atten + reflection;
        UNITY_OPAQUE_ALPHA(c.a);
        return c;
    }

    struct Input {
        half3 localPos;
        half3 normal;
        float3 worldPos;
		float2 texcoord;
    };

    
    void vert(inout appdata_full v,out Input i){
        UNITY_INITIALIZE_OUTPUT(Input,i);
        i.normal = v.normal;
        if(_NoiseType>2){
            half sx = length(mul(unity_ObjectToWorld,half4(1,0,0,0)));
            half sy = length(mul(unity_ObjectToWorld,half4(0,1,0,0)));
            half sz = length(mul(unity_ObjectToWorld,half4(0,0,1,0)));
            i.localPos = v.vertex.xyz * half3(sx,sy,sz);
        }else
            i.localPos = v.vertex.xyz;
        i.texcoord = v.texcoord;
    }

    void surf (Input IN, inout CustomSurfaceOutput o) {
        o.Albedo = lerp(_Color1.rgb,_Color2.rgb,step( frac(100*IN.localPos.z/_Width) ,_Bias) );
        fixed noise = 0;
        if(_UseNoise>0.5){
            if(_NoiseType<=1){
                noise = tex2D(_NoiseGlobal,IN.texcoord*_NoiseScale).x*_Noise;
            }else{
                float2 uv = float2(0,0);
                if(abs(IN.normal.x)>0.577)
                    uv = IN.localPos.yz;
                else if(abs(IN.normal.y)>0.577)
                    uv = IN.localPos.xz;
                else
                    uv = IN.localPos.xy;
                noise = tex2D(_NoiseGlobal,uv*_NoiseScale).x*_Noise;
            }
        }
        o.Gloss = max(_Glossy-noise,0);
        o.Specular = _Metallic;
        o.Alpha = 1;
        o.worldPos = IN.worldPos;
    }
    ENDCG
    }
    FallBack "Mobile/VertexLit"
}