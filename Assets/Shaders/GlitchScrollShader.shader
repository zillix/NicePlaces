Shader "GlitchScroll"
{
    Properties
    {
		_MainTex("-", 2D) = "" {}
		_ScrollTex  ("-", 2D) = "" {}
    }

    CGINCLUDE

    #include "UnityCG.cginc"

	sampler2D _MainTex; 
	sampler2D _ScrollTex;

    float4 frag(v2f_img i) : SV_Target 
    {
        float4 glitch = tex2D(_ScrollTex, i.uv);
        
		// Shift row
		float x = i.uv.x + glitch.xy.x;
		if (x >= 1)
		{
			x -= 1;
		}
		float2 uv = float2(x, i.uv.y);
        float4 source = tex2D(_MainTex, uv);

        return float4(source.rgb, source.a);
    }

    ENDCG

    SubShader
    {
        Pass
        {
            ZTest Always Cull Off ZWrite Off
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #pragma target 3.0
            ENDCG
        }
    }
}
