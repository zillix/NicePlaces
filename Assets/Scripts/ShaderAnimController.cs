using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[ExecuteInEditMode]
public class ShaderAnimController : MonoBehaviour
{
	public ShaderState state = new ShaderState();
	public ShaderAnimControlConfig config = new ShaderAnimControlConfig();
	public bool update = true;

	public List<MeshRenderer> objectRenderers = new List<MeshRenderer>();
	public bool grabChildren = false;

	private float time;

	private Dictionary<ControlVal, float> baseCache = new Dictionary<ControlVal, float>();

	public void Update()
	{
		if (grabChildren)
		{
			objectRenderers = new List<MeshRenderer>(gameObject.GetComponentsInChildren<MeshRenderer>());
		}

		time += Time.fixedDeltaTime;

		if (objectRenderers != null && state != null && update )
		{
			foreach (MeshRenderer renderer in objectRenderers)
			{
				if (!renderer) continue;

				if (config.updateFlip)
					renderer.sharedMaterial.SetFloat("_Flip", state.flip ? 1 : 0);


				if (config.updateNormalize)
					renderer.sharedMaterial.SetFloat("_Normalize", state.normalize ? 1 : 0);

				if (config.updateStripeHeight)
					renderer.sharedMaterial.SetFloat("_StripeHeight", state.stripeHeight);

				if (config.updateStripeSine)
				{
					renderer.sharedMaterial.SetFloat("_StripeSineEnabled", state.stripeSineEnabled ? 1 : 0);
					renderer.sharedMaterial.SetFloat("_StripeSinePeriod", state.stripeSinePeriod);
					renderer.sharedMaterial.SetFloat("_StripeSineTwiddle", state.stripeSineTwiddle);
					renderer.sharedMaterial.SetFloat("_StripeSineTwaddle", state.stripeSineTwaddle);
					applyControlVal(renderer, "_StripeSineOffset", ref state.stripeSineOffset, config.stripeSineOffsetControl);
					applyControlVal(renderer, "_StripeSineThreshold", ref state.stripeSineThreshold, config.stripeSineThresholdControl);
				}
				if (config.updateWipe)
				{
					renderer.sharedMaterial.SetFloat("_WipeEnabled", state.wipeEnabled ? 1 : 0);
					renderer.sharedMaterial.SetFloat("_WipeNormalized", state.wipeNormalized ? 1 : 0);
					renderer.sharedMaterial.SetFloat("_WipeSlope", state.wipeSlope);
					applyControlVal(renderer, "_WipeYOffset", ref state.wipeYOffset, config.wipeYOffsetControl);
					renderer.sharedMaterial.SetFloat("_WipeSinePeriod", state.wipeSinePeriod);
					renderer.sharedMaterial.SetFloat("_WipeSineScale", state.wipeSineScale);
					renderer.sharedMaterial.SetFloat("_WipeSineInvertAxes", state.wipeSineInvertAxes ? 1 : 0);
					applyControlVal(renderer, "_WipeSineOffset", ref state.wipeSineOffset, config.wipeSineOffset);
					applyControlVal(renderer, "_WipeSineThreshold", ref state.wipeSineThreshold, config.wipeSineThreshold);
					renderer.sharedMaterial.SetFloat("_WipeSineTwiddle", state.wipeSineTwiddle);
					renderer.sharedMaterial.SetFloat("_WipeSineTwaddle", state.wipeSineTwaddle);
					renderer.sharedMaterial.SetFloat("_WipeSineShapePeriod", state.wipeSineShapePeriod);
					applyControlVal(renderer, "_WipeSineShapeOffset", ref state.wipeSineShapeOffset, config.wipeSineShapeOffset);
					applyControlVal(renderer, "_WipeSineShapeScale", ref state.wipeSineShapeScale, config.wipeSineShapeScale);
					renderer.sharedMaterial.SetFloat("_WipeSineShapeTwiddle", state.wipeSineShapeTwiddle);
					renderer.sharedMaterial.SetFloat("_WipeSineShapeTwaddle", state.wipeSineShapeTwaddle);
				}

				if (config.updateRadial)
				{
					renderer.sharedMaterial.SetFloat("_RadialEnabled", state.radialEnabled ? 1 : 0);
					renderer.sharedMaterial.SetFloat("_RadialNormalized", state.radialNormalized ? 1 : 0);
					renderer.sharedMaterial.SetFloat("_RadialSinePeriod", state.radialSinePeriod);
					applyControlVal(renderer, "_RadialSineOffset", ref state.radialSineOffset, config.radialSineOffsetControl);
					applyControlVal(renderer, "_RadialSineThreshold", ref state.radialSineThreshold, config.radialSineThresholdControl);
					renderer.sharedMaterial.SetFloat("_RadialSineScale", state.radialSineScale);
					renderer.sharedMaterial.SetFloat("_RadialMinimum", state.radialMinimum);
					renderer.sharedMaterial.SetVector("_RadialArcStart", state.radialArcStart);
					renderer.sharedMaterial.SetVector("_RadialArcStop", state.radialArcStop);
					renderer.sharedMaterial.SetFloat("_RadialX", state.radialPos.x);
					renderer.sharedMaterial.SetFloat("_RadialY", state.radialPos.y);
				}

				if (config.updateRing)
				{
					renderer.sharedMaterial.SetFloat("_RingEnabled", state.ringEnabled ? 1 : 0);
					renderer.sharedMaterial.SetFloat("_RingNormalized", state.ringNormalized ? 1 : 0);
					renderer.sharedMaterial.SetFloat("_RingEnabled", state.ringEnabled ? 1 : 0);
					renderer.sharedMaterial.SetFloat("_RingSinePeriod", state.ringSinePeriod);
					renderer.sharedMaterial.SetFloat("_RingSineScale", state.ringSineScale);
					renderer.sharedMaterial.SetFloat("_RingSineThreshold", state.ringSineThreshold);
					applyControlVal(renderer, "_RingSineOffset", ref state.ringSineOffset, config.ringSineOffsetControl);
					applyControlVal(renderer, "_RingSineThreshold", ref state.ringSineThreshold, config.ringSineThresholdControl);
					renderer.sharedMaterial.SetVector("_RingScale", state.ringScale);
					renderer.sharedMaterial.SetFloat("_RingX", state.ringPos.x);
					renderer.sharedMaterial.SetFloat("_RingY", state.ringPos.y);
				}
			}
		}


	}

	private void applyControlVal(MeshRenderer renderer, string name, ref float state, ControlVal control)
	{
		if (!GameManager.Started)
		{
			return;
		}

		if (!baseCache.ContainsKey(control))
		{
			baseCache.Add(control, state);
		}
		float baseVal = baseCache[control];


		float assign = state;
		if (control.speed != 0)
		{
			if (control.radMagnitude != 0)
			{
				assign = baseVal + Mathf.Sin(time * control.speed) * control.radMagnitude;
			}
			else
			{
				state += Time.fixedDeltaTime * control.speed;
			}
		}
		renderer.sharedMaterial.SetFloat(name, assign);
	}


}
