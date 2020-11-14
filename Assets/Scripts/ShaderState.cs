using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ShaderState
{
	public bool normalize;
	public bool flip;
	public bool ditherAll;
	public float ditherAllThreshold;

	public float stripeHeight;

	public bool stripeSineEnabled;
	public bool stripeSineDitherEnabled;
	public float stripeDitherPow = 1;
	public float stripeSinePeriod;
	public float stripeSineTwiddle;
	public float stripeSineTwaddle;
	public float stripeSineOffset;
	public float stripeSineThreshold;

	public bool wipeEnabled;
	public bool wipeSineDitherEnabled;
	public float wipeDitherPow = 1;
	public bool wipeNormalized;
	public float wipeSlope;
	public float wipeYOffset;
	public float wipeSinePeriod;
	public float wipeSineScale;
	public float wipeSineOffset;
	public float wipeSineThreshold;
	public float wipeSineTwiddle;
	public float wipeSineTwaddle;
	public bool wipeSineInvertAxes;
	public float wipeSineShapePeriod;
	public float wipeSineShapeOffset;
	public float wipeSineShapeScale;
	public float wipeSineShapeTwiddle;
	public float wipeSineShapeTwaddle;

	public bool radialEnabled;
	public bool radialSineDitherEnabled;
	public float radialDitherPow = 1;
	public bool radialNormalized;
	public float radialSinePeriod;
	public float radialSineScale;
	public float radialSineOffset;
	public float radialSineThreshold;
	public float radialMinimum;
	public Vector2 radialArcStart;
	public Vector2 radialArcStop;
	public Vector2 radialPos;

	public bool ringEnabled;
	public bool ringSineDitherEnabled;
	public float ringDitherPow = 1;
	public bool ringNormalized;
	public float ringSinePeriod;
	public float ringSineScale;
	public float ringSineOffset;
	public float ringSineThreshold;
	public Vector2 ringScale = new Vector2(1, 1);
	public Vector2 ringPos;


	public float ditherScale = 1;
	public Texture ditherTexture;
	public float ditherOffsetX = 0;
	public float ditherOffsetY = 0;
	public float ditherThreshold = 0;

	public Animator animator;
}