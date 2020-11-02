using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class PlaceData
{
	public RenderTexture renderTexture;
	public Material transitionMaterial;
	public TransitionType transitionType;
	public float tickTime = .1f;
	public float transitionPerTick = .1f;

}
