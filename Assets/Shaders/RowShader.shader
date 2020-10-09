Shader "Zillix/RowShader"
{
	Properties
	{
		_Color1("Color 1", Color) = (1,1,1,1)
		_Color2("Color 2", Color) = (1,1,1,1)
		[Toggle] _Flip("Flip", Float) = 0
		[Toggle] _Normalize("Normalize", Float) = 0


		_StripeHeight("Stripe Height", Float) = 10
		[Toggle] _StripeSineEnabled("Stripe Sine Enabled", Float) = 0
		_StripeSinePeriod("Stripe Sine Period", Float) = 0
		_StripeSineOffset("Stripe Sine Offset", Float) = 0
		_StripeSineTwiddle("Stripe Sine Twiddle", Float) = 0
		_StripeSineTwiddle("Stripe Sine Twaddle", Float) = 0
		_StripeSineThreshold("Stripe Sine Threshold", Float) = 0

		[Toggle] _WipeEnabled("Wipe Enabled", Float) = 0
		_WipeSlope("Wipe Slope", Float) = 0
		_WipeYOffset("Wipe Y Offset", Float) = 0

		[Toggle] _RadialEnabled("Radial Enabled", Float) = 0
		[Toggle] _RadialNormalized("Radial Normalized", Float) = 0
		_RadialSinePeriod("Radial Sine Period", Float) = 0
		_RadialSineScale("Radial Sine Scale", Float) = 1
		_RadialSineOffset("Radial Sine Offset", Float) = 0
		_RadialSineThreshold("Radial Sine Threshold", Float) = 0
		_RadialMinimum("Radial Minimum", Float) = 0
		_RadialArcStart("Radial Arc Start", Vector) = (0,0,0,0)
		_RadialArcStop("Radial Arc Stop", Float) = (0,0,0,0)

		[Toggle] _RingEnabled("Ring Enabled", Float) = 0
		[Toggle] _RingNormalized("Ring Normalized", Float) = 0
		_RingSinePeriod("Ring Sine Period", Float) = 0
		_RingSineScale("Ring Sine Scale", Float) = 0
		_RingSineOffset("Ring Sine Offset", Float) = 0
		_RingSineThreshold("Ring Sine Threshold", Float) = 0
		_RingScale("Ring Scale", Vector) = (0,0,0,0)
		_RingX("Ring X", Float) = 0
		_RingY("Ring Y", Float) = 0

	}

		CGINCLUDE

		fixed4 _Color1;
	fixed4 _Color2;
	float _Flip;
	float _Normalize;
	float _StripeHeight;

	float _StripeSineEnabled;
	float _StripeSinePeriod;
	float _StripeSineTwiddle;
	float _StripeSineTwaddle;
	float _StripeSineOffset;
	float _StripeSineThreshold;

	float _WipeEnabled;
	float _WipeSlope;
	float _WipeYOffset;

	float _RadialEnabled;
	float _RadialNormalized;
	float _RadialSinePeriod;
	float _RadialSineScale;
	float _RadialSineOffset;
	float _RadialSineThreshold;
	float _RadialMinimum;
	float4 _RadialArcStart;
	float4 _RadialArcStop;

	float _RadialX;
	float _RadialY;

	float _RingEnabled;
	float _RingNormalized;
	float _RingSinePeriod;
	float _RingSineScale;
	float _RingSineOffset;
	float _RingSineThreshold;
	float4 _RingScale;
	//float _RadialLength = 1
	//float _RadialX[1];

	float _RingX;
	float _RingY;

	

	
    #include "UnityCG.cginc"

    float4 frag(v2f_img i) : SV_Target 
	{
		/*float4 glitch = tex2D(_ScrollTex, i.uv);

		// Shift row
		float x = i.uv.x + glitch.xy.x;
		if (x >= 1)
		{
			x -= 1;
		}
		float2 uv = float2(x, i.uv.y);
		float4 source = tex2D(_MainTex, uv);

		return float4(source.rgb, source.a);
		*/
		bool flip = _Flip;
		float x = _Normalize ? (i.pos.x / _ScreenParams.x) : i.pos.x;
		float y = _Normalize ? (i.pos.y / _ScreenParams.y) : i.pos.y;


		// Stripe
		if (_StripeSineEnabled) {
			float sinAngle = (y + _StripeSineOffset) * _StripeSinePeriod;
			float sinInput = sinAngle;
			if (_StripeSineTwiddle != 0 || _StripeSineTwaddle != 0) {
				sinInput = -_StripeSineTwiddle * cos(sinAngle) + sinAngle * (_StripeSineTwiddle + _StripeSineTwaddle);
			}
			float stripeSine = _StripeHeight * sin(sinInput);
			if (stripeSine > _StripeSineThreshold)
				flip = !flip;
		}
		else {
			if (uint(i.pos.y / _StripeHeight) % 2 == 1)
				flip = !flip;
		}

		fixed4 c1 = _Color1;
		fixed4 c2 = _Color2;

		// Wipe
		if (_WipeEnabled) {
			float lineY = x * _WipeSlope + _WipeYOffset;
			if (lineY > y)
			{
				flip = !flip;
				//c1.r = lineY;
				//c2.r = lineY;
			}
		}

		// Radial
		if (_RadialEnabled) {
			float srcX = _RadialNormalized ? (i.pos.x / _ScreenParams.x) : i.pos.x;
			float srcY = _RadialNormalized ? (i.pos.y / _ScreenParams.y) : i.pos.y;
			float radX = _RadialX; // _RadialNormalized ? .5 : _ScreenParams.x / 2;
			float radY = _RadialY; // _RadialNormalized ? .5 : _ScreenParams.y / 2;
			if (srcX != radX && srcY != radY) {
				bool success = true;
				if (_RadialMinimum > 0) {
					float dist = sqrt(pow(srcX - _RadialX, 2) + pow(srcY - _RadialY, 2));
					if (dist < _RadialMinimum) {
						success = false;
						flip = !flip;
					}
				}
				if (success) {
					float ang = atan2(srcY - radY, srcX - radX); // -PI/2, PI/2
					bool angCheck = true;
					/*if (_RadialAngleArc != 0) {
						const float PI = 3.14159;
						float bound = ang > 0 ? ang : (2 * PI + x); // 0, 2*PI
						float endAng = _RadialAngleStart + _RadialAngleArc;
						if (endAng < 2 * PI) {
							if (bound > _RadialAngleStart && bound < endAng)
								angCheck = true;
						}
						else {
							if (bound < 2 * PI) {
								bound += 2 * PI;
							}
							if (bound > _RadialAngleStart && bound < endAng)
								angCheck = false;
						}
					}
					else {
						angCheck = true;
					}*/
					if (_RadialArcStart.x != 0 || _RadialArcStart.y != 0) {
						float2 vec = float2(srcX - radX, srcY - radY);
						//https://stackoverflow.com/questions/13652518/efficiently-find-points-inside-a-circle-sector
						float2 startNorm = float2(-_RadialArcStart.y, _RadialArcStart.x);
						float startNormDot = dot(vec, startNorm);

						float2 endNorm = float2(-_RadialArcStop.y, _RadialArcStop.x);
						float endNormDot = dot(vec, endNorm);
						angCheck = startNormDot > 0 && endNormDot <= 0;
					}
					else {
						angCheck = true;
					}

					if (angCheck) {
						float sine = _RadialSineScale * sin((ang + _RadialSineOffset) * _RadialSinePeriod);
						if (sine > _RadialSineThreshold) {
							flip = !flip;
						}
					}
				}
			}
		}

		// Ring
		if (_RingEnabled) {
			float srcX = _RingNormalized ? (i.pos.x / _ScreenParams.x) : i.pos.x;
			float srcY = _RingNormalized ? (i.pos.y / _ScreenParams.y) : i.pos.y;
			float ringX = _RingX; //RingNormalized ?  : _ScreenParams.x / 2;
			float ringY = _RingY;//_RingNormalized ? .5 : _ScreenParams.y / 2;
			if (srcX != ringX && srcY != ringY) {
				float2 delta = float2(srcX - _RingX, srcY - ringY);
				if ((_RingScale.x != 0 && _RingScale.y != 0) &&
					(_RingScale.x != 1 || _RingScale.y != 1)) {
					delta.x /= _RingScale.x;
					delta.y /= _RingScale.y;
				}

				float dist = sqrt(pow(delta.x, 2) + pow(delta.y, 2));
				float sine = _RingSineScale * sin((dist + _RingSineOffset) * _RingSinePeriod);
				if (sine > _RingSineThreshold) {
					flip = !flip;
				}
			}
		}

		return flip ? c2 : c1;
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
