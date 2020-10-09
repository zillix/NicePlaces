using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ShaderState
{
	public bool normalize;
	public bool flip;

	public float stripeHeight;

	public bool stripeSineEnabled;
	public float stripeSinePeriod;
	public float stripeSineTwiddle;
	public float stripeSineTwaddle;
	public float stripeSineOffset;
	public float stripeSineThreshold;

	public bool wipeEnabled;
	public float wipeSlope;
	public float wipeYOffset;

	public bool radialEnabled;
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
	public bool ringNormalized;
	public float ringSinePeriod;
	public float ringSineScale;
	public float ringSineOffset;
	public float ringSineThreshold;
	public Vector2 ringScale = new Vector2(1, 1);
	public Vector2 ringPos;

	public Animator animator;
}