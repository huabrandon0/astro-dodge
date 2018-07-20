﻿Shader "Custom/Gradient"
{
    Properties
    {
        _Color2("Top Color", Color) = (1,1,1,1)
        _Color("Bottom Color", Color) = (0,1,0,1)
        _MainTex("Base (RGB)", 2D) = "white" {}
    }
    SubShader
    {
        Tags{ "RenderType" = "Opaque" }
        LOD 200

    CGPROGRAM
    #pragma surface surf Lambert

    sampler2D _MainTex;
    fixed4 _Color;
    fixed4 _Color2;

    struct Input
    {
        float2 uv_MainTex;
        float4 screenPos;
    };

    void surf(Input IN, inout SurfaceOutput o)
    {
        float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
        fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * lerp(_Color, _Color2, screenUV.y);
        o.Albedo = c.rgb;
        o.Alpha = c.a;
    }
    ENDCG
    }
    
    Fallback "VertexLit"
}
