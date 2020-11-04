Shader "Zillix/DerezShader"
{
	Properties
	{
		_Texture1("Texture1", 2D) = "" {}
		_Resolution("Resolution", Vector) = (1024,1024,0,0)
	}

	CGINCLUDE

	sampler2D _Texture1;
	fixed4 _Resolution;

	
    #include "UnityCG.cginc"

    float4 frag(v2f_img i) : SV_Target 
	{
		float2 pos = i.uv.xy;
		float2 rez = _ScreenParams.xy / _Resolution.xy;
		pos.x = floor(pos.x * _ScreenParams.x / rez.x) * rez.x / _ScreenParams.x;
		pos.y = floor(pos.y * _ScreenParams.y / rez.y) * rez.y / _ScreenParams.y;
		//pos.y = floor(pos.y / 1) * 1;

		return tex2D(_Texture1, pos);
    }

    ENDCG

    SubShader
    {
        Pass
        {
            //ZTest Always Cull Off ZWrite Off
            Tags {"RenderType"="Opaque"}
			CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #pragma target 3.0
            ENDCG
        }
    }
}
