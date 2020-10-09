using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ShaderAnimControlConfig
{
	public bool updateFlip;
	public bool updateNormalize;
	public bool updateStripeHeight;
	public bool updateStripeSine;
	public bool updateWipe;
	public bool updateRadial;
	public bool updateRing;

	public ControlVal stripeSineOffsetControl;
	public ControlVal stripeSineThresholdControl;
	public ControlVal wipeYOffsetControl;
	public ControlVal radialSineOffsetControl;
	public ControlVal radialSineThresholdControl;
	public ControlVal ringSineOffsetControl;
	public ControlVal ringSineThresholdControl;
}
