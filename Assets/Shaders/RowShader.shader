Shader "Zillix/RowShader"
{
	Properties
	{
		_Texture1("Texture 1", 2D) = "" {}
		_Texture2("Texture 2", 2D) = "" {}
		_Color1("Color 1", Color) = (1,1,1,1)
		_Color2("Color 2", Color) = (1,1,1,1)
		[Toggle] _TextureMode("TextureMode", Float) = 0
		[Toggle] _Flip("Flip", Float) = 0
		[Toggle] _Normalize("Normalize", Float) = 0


		_StripeHeight("Stripe Height", Float) = 10
		[Toggle] _StripeSineEnabled("Stripe Sine Enabled", Float) = 0
		[Toggle] _StripeSineDitherEnabled("Stripe Sine Dither Enabled", Float) = 0
		_StripeSinePeriod("Stripe Sine Period", Float) = 0
		_StripeSineOffset("Stripe Sine Offset", Float) = 0
		_StripeSineTwiddle("Stripe Sine Twiddle", Float) = 0
		_StripeSineTwaddle("Stripe Sine Twaddle", Float) = 0
		_StripeSineThreshold("Stripe Sine Threshold", Float) = 0

		[Toggle] _WipeEnabled("Wipe Enabled", Float) = 0
		[Toggle] _WipeNormalized("Wipe Normalized", Float) = 0
		_WipeSlope("Wipe Slope", Float) = 0
		_WipeYOffset("Wipe Y Offset", Float) = 0
		_WipeSinePeriod("Wipe Sine Period", Float) = 0
		_WipeSineScale("Wipe Sine Scale", Float) = 1
		_WipeSineOffset("Wipe Sine Offset", Float) = 1
		_WipeSineThreshold("Wipe Sine Threshold", Float) = 0
		_WipeSineTwiddle("Wipe Sine Twiddle", Float) = 0
		_WipeSineTwaddle("Wipe Sine Twaddle", Float) = 0
		[Toggle]_WipeSineInvertAxes("Wipe Sine Invert Axes", Float) = 0
		_WipeSineShapePeriod("Wipe Sine Shape Period", Float) = 0
		_WipeSineShapeOffset("Wipe Sine Shape Offset", Float) = 0
		_WipeSineShapeScale("Wipe Sine Shape Scale", Float) = 1
		_WipeSineShapeTwiddle("Wipe Sine Shape Twiddle", Float) = 0
		_WipeSineShapeTwaddle("Wipe Sine Shape Twaddle", Float) = 0

		[Toggle] _RadialEnabled("Radial Enabled", Float) = 0
		[Toggle] _RadialNormalized("Radial Normalized", Float) = 0
		[Toggle] _RadialSineDitherEnabled("Radial Sine Dither Enabled", Float) = 0
		_RadialSinePeriod("Radial Sine Period", Float) = 0
		_RadialSineScale("Radial Sine Scale", Float) = 1
		_RadialSineOffset("Radial Sine Offset", Float) = 0
		_RadialSineThreshold("Radial Sine Threshold", Float) = 0
		_RadialMinimum("Radial Minimum", Float) = 0
		_RadialArcStart("Radial Arc Start", Vector) = (0,0,0,0)
		_RadialArcStop("Radial Arc Stop", Float) = (0,0,0,0)
		_RadialX("Radial X", Float) = 0
		_RadialY("Radial Y", Float) = 0

		[Toggle] _RingEnabled("Ring Enabled", Float) = 0
		[Toggle] _RingNormalized("Ring Normalized", Float) = 0
		[Toggle] _RingSineDitherEnabled("Ring Sine Dither Enabled", Float) = 0
		_RingSinePeriod("Ring Sine Period", Float) = 0
		_RingSineScale("Ring Sine Scale", Float) = 0
		_RingSineOffset("Ring Sine Offset", Float) = 0
		_RingSineThreshold("Ring Sine Threshold", Float) = 0
		_RingScale("Ring Scale", Vector) = (0,0,0,0)
		_RingX("Ring X", Float) = 0
		_RingY("Ring Y", Float) = 0

		[Toggle] _DitherEnabled("Dither Enabled", Float) = 0
		_DitherTexture("Dither Texture", 2D) = "" {}
		_DitherScale("Dither Scale", float) = 1
		_DitherThreshold("Dither Threshold", float) = .5
		_DitherOffset("Dither Offset", Vector) = (0,0,0,0)

	}

		CGINCLUDE

		fixed4 _Color1;
	fixed4 _Color2;
	sampler2D _Texture1;
	sampler2D _Texture2;
	float _TextureMode;
	float _Flip;
	float _Normalize;
	float _StripeHeight;

	float _StripeSineEnabled;
	float _StripeSinePeriod;
	float _StripeSineTwiddle;
	float _StripeSineTwaddle;
	float _StripeSineOffset;
	float _StripeSineThreshold;
	float _StripeSineDitherEnabled;

	float _WipeEnabled;
	float _WipeNormalized;
	float _WipeSlope;
	float _WipeYOffset;
	float _WipeSinePeriod;
	float _WipeSineScale;
	float _WipeSineThreshold;
	float _WipeSineInvertAxes;
	float _WipeSineOffset;
	float _WipeSineTwiddle;
	float _WipeSineTwaddle;
	float _WipeSineShapePeriod;
	float _WipeSineShapeOffset;
	float _WipeSineShapeScale;
	float _WipeSineShapeTwiddle;
	float _WipeSineShapeTwaddle;

	float _RadialEnabled;
	float _RadialNormalized;
	float _RadialSinePeriod;
	float _RadialSineScale;
	float _RadialSineOffset;
	float _RadialSineThreshold;
	float _RadialMinimum;
	float4 _RadialArcStart;
	float4 _RadialArcStop;
	float _RadialSineDitherEnabled;

	float _RadialX;
	float _RadialY;

	float _RingEnabled;
	float _RingNormalized;
	float _RingSinePeriod;
	float _RingSineScale;
	float _RingSineOffset;
	float _RingSineThreshold;
	float4 _RingScale;
	float _RingSineDitherEnabled;
	//float _RadialLength = 1
	//float _RadialX[1];

	float _RingX;
	float _RingY;

	float _DitherEnabled;
	sampler2D _DitherTexture;
	float2 _DitherTexture_TexelSize;
	float _DitherScale;
	float _DitherThreshold;
	float4 _DitherOffset;

	

	
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

		// derez
		float2 ditherPos = float2(0, 0);
		float ditherVal = 0;

		bool anyDither = _DitherEnabled || _StripeSineDitherEnabled || _RadialSineDitherEnabled || _RingSineDitherEnabled;
		float2 ditherCoordinate = float2(0, 0);
		if (anyDither) {

			float ditherX = (i.pos.x / _ScreenParams.x) + _DitherOffset.x; // i.uv.x;// / _ScreenParams.x;
			float ditherY = (i.pos.y / _ScreenParams.x) + _DitherOffset.y; // i.uv.y;// / _ScreenParams.y;
			float2 ditherCoordinate = float2(ditherX, ditherY);
			ditherCoordinate *= _DitherScale;
			ditherVal = tex2D(_DitherTexture, ditherCoordinate).r;// +(.5f / 256); // gamma correction?
			if (_DitherEnabled && ditherVal > _DitherThreshold) {
				flip = !flip;
			}

			float ditherRez = (_DitherTexture_TexelSize.x) *  _ScreenParams.x / _DitherScale;
			// derez
			ditherPos.x = floor(i.pos.x / ditherRez) * ditherRez;
			ditherPos.y = floor(i.pos.y / ditherRez) * ditherRez;
		}


		// Stripe
		if (_StripeSineEnabled) {
			float2 pos = float2(x, y);
			if (_StripeSineDitherEnabled) {
				pos = ditherPos;
			}

			float sinAngle = (pos.y + _StripeSineOffset) * _StripeSinePeriod;
			float sinInput = sinAngle;
			if (_StripeSineTwiddle != 0 || _StripeSineTwaddle != 0) {
				sinInput = -_StripeSineTwiddle * cos(sinAngle) + sinAngle * (_StripeSineTwiddle + _StripeSineTwaddle);
			}
			float rawStripeSine = sin(sinInput);
			float stripeSine = _StripeHeight * rawStripeSine;
			if (_StripeSineDitherEnabled) {
				float sineDitherThreshold = (stripeSine + 1) / 2;
				flip = sineDitherThreshold > ditherVal ? !flip : flip; // true : false; // !flip : flip;
			}
			else if (stripeSine > _StripeSineThreshold)
			{
				flip = !flip;
			}
		}
		else {
			if (uint(i.uv.y / _StripeHeight) % 2 == 1)
				flip = !flip;
		}

		fixed4 c1 = _Color1;
		fixed4 c2 = _Color2;

		// Wipe
		if (_WipeEnabled) {

			float wipeX = _WipeNormalized ? (i.pos.x / _ScreenParams.x) : i.pos.x;
			float wipeY = _WipeNormalized ? (i.pos.y / _ScreenParams.y) : i.pos.y;
			float baseX = wipeX;
			float baseY = wipeY;
			if (_WipeSineInvertAxes != 0) {
				float temp = baseX;
				baseX = baseY;// 1 / (_WipeSlope == 0 ? .0001 : _WipeSlope);
				baseY = temp;
			}


			float inputX = baseX;
			float inputY = baseY + baseX * _WipeSlope + _WipeYOffset;

			if (_WipeSineShapePeriod != 0) {
				float shapeSinAngle = (baseY + _WipeSineShapeOffset) * _WipeSineShapePeriod;
				float shapeSinInput = shapeSinAngle;
				if (_WipeSineShapeTwiddle != 0 || _WipeSineShapeTwaddle != 0) {
					shapeSinInput = -_WipeSineShapeTwiddle * cos(shapeSinAngle) + shapeSinAngle * (_WipeSineShapeTwiddle + _WipeSineShapeTwaddle);
				}
				float calculatedSin = sin(shapeSinInput);

				float2 slopeNormVec = _WipeSineShapeScale * normalize(float2(-_WipeSlope, baseX));
				slopeNormVec *= calculatedSin;
				inputX += slopeNormVec.x;
				inputY += slopeNormVec.y;
			}
			float sinAngle = (inputY + _WipeSineOffset) * _WipeSinePeriod;
			
			float sinInput = sinAngle;
			if (_WipeSineTwiddle != 0 || _WipeSineTwaddle != 0) {
				sinInput = -_WipeSineTwiddle * cos(sinAngle) + sinAngle * (_WipeSineTwiddle + _WipeSineTwaddle);
			}
			float stripeSine = _WipeSineScale * sin(sinInput);
			if (stripeSine > _WipeSineThreshold)
				flip = !flip;
		}

		// Radial
		if (_RadialEnabled) {
			//float2 pos = i.pos.xy;
			/*if (_RadialSineDitherEnabled) {
				pos = ditherPos;
			}*/
			float2 pos = i.pos.xy;
			if (_RadialSineDitherEnabled) {
				float ditherRez = (_DitherTexture_TexelSize.x) *  _ScreenParams.x / _DitherScale;
				// derez
				pos.x = floor(pos.x / ditherRez) * ditherRez;
				pos.y = floor(pos.y / ditherRez) * ditherRez;
			}


			float srcX = _RadialNormalized ? (pos.x / _ScreenParams.x) : pos.x;
			float srcY = _RadialNormalized ? (pos.y / _ScreenParams.y) : pos.y;
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
						float rawSine = sin((ang + _RadialSineOffset) * _RadialSinePeriod);
						float sine = _RadialSineScale * rawSine;
						
						if (_RadialSineDitherEnabled) {
							float sineDitherThreshold = pow(sine, 1); // (rawSine + 1) / 2;
							flip = sineDitherThreshold > ditherVal ? !flip : flip; // false : true; // !flip : flip;
						}
						else if (sine > _RadialSineThreshold) {
							flip = !flip;
						}
					}
				}
			}
		}

		// Ring
		if (_RingEnabled) {
			float2 pos = i.pos.xy;
			if (_RingSineDitherEnabled) {
				pos = ditherPos;
			}


			float srcX = _RingNormalized ? (pos.x / _ScreenParams.x) : pos.x;
			float srcY = _RingNormalized ? (pos.y / _ScreenParams.y) : pos.y;
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
				float rawSine = sin((dist + _RingSineOffset) * _RingSinePeriod);
				float sine = _RingSineScale * rawSine;
				if (_RingSineDitherEnabled) {
					float sineDitherThreshold = (sine + 1) / 2;
					flip = sineDitherThreshold > ditherVal ? !flip : flip; // false : true; // !flip : flip;
				}
				else if (sine > _RingSineThreshold) {
					flip = !flip;
				}
			}
		}

		if (_TextureMode) {
			return flip ? tex2D(_Texture1, i.uv) : tex2D(_Texture2, i.uv);
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
