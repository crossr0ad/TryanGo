Shader "Unlit/WaterShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _BaseColor("BaseColor",Color) = (1,1,1,1)
        _SurfaceColor("SurfaceColor",Color) = (1,1,1,1)
        _SurfaceThickness("SurfaceThickness",Range(0.0,0.5)) = 0.05
        _WaveHeight("WaveHeight",Range(0.0,0.2)) = 0.01
        _WaveLength("WaveLength",Range(0.0,1.0)) = 0.5
        _InputPoint0("InputPoint0",vector) = (0,0,0,0)
        _InputPoint1("InputPoint1",vector) = (0,0,0,0)
        _InputPoint2("InputPoint2",vector) = (0,0,0,0)
        _InputPoint3("InputPoint3",vector) = (0,0,0,0)
        _InputPoint4("InputPoint4",vector) = (0,0,0,0)
        _WavePower("WavePower",Range(0.0,1.0)) = 1.0
    }
    SubShader
    {
        Tags { "Queue" = "Transparent"
            "RenderType" = "Transparent" }
        LOD 100

        Pass
        {
            ZWrite Off
            Blend SrcAlpha OneMinusSrcAlpha           
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
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
                float4 vertex : SV_POSITION;
            };

            float4 _BaseColor;
            float4 _SurfaceColor;
            float _SurfaceThickness;
            float _WaveHeight;
            float _WaveLength;
            float4 _InputPoint0;
            float4 _InputPoint1;
            float4 _InputPoint2;
            float4 _InputPoint3;
            float4 _InputPoint4;
            float _WavePower;

            v2f vert (appdata v)
            {
                v2f o;

                float4 wpos = mul(unity_ObjectToWorld, v.vertex);
                float dis0 = distance(_InputPoint0.xyz, wpos.xyz) / 2;
                float waveH0 = cos((_Time.y) / _WaveLength) * saturate(1 - dis0) * _WavePower * _InputPoint0.w;
                float dis1 = distance(_InputPoint1.xyz, wpos.xyz) / 2;
                float waveH1 = cos((_Time.y) / _WaveLength) * saturate(1 - dis1) * _WavePower * _InputPoint1.w;
                float dis2 = distance(_InputPoint2.xyz, wpos.xyz) / 2;
                float waveH2 = cos((_Time.y) / _WaveLength) * saturate(1 - dis2) * _WavePower * _InputPoint2.w;
                float dis3 = distance(_InputPoint3.xyz, wpos.xyz) / 2;
                float waveH3 = cos((_Time.y) / _WaveLength) * saturate(1 - dis3) * _WavePower * _InputPoint3.w;
                float dis4 = distance(_InputPoint4.xyz, wpos.xyz) / 2;
                float waveH4 = cos((_Time.y) / _WaveLength) * saturate(1 - dis4) * _WavePower * _InputPoint4.w;

                float waveH = waveH0 + waveH1 + waveH2 + waveH3 + waveH4;
                wpos.y += sin((_Time.x * 3 + wpos.x) / _WaveLength) * _WaveHeight + waveH;
                o.vertex = mul(UNITY_MATRIX_VP, wpos);
                //o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {               
                float surface = saturate((_SurfaceThickness - (1 - i.uv.y)) * (1 / _SurfaceThickness));

                fixed4 col = _BaseColor;
                col.rgb += surface * _SurfaceColor.rgb;
                col.a += surface * (1 - _BaseColor.a);                
                return col;
            }
            ENDCG
        }
    }
}
