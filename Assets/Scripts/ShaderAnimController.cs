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
					updateFloat(renderer, "_Flip", state.flip ? 1 : 0);


				if (config.updateNormalize)
					updateFloat(renderer, "_Normalize", state.normalize ? 1 : 0);

				if (config.updateStripeHeight)
					updateFloat(renderer, "_StripeHeight", state.stripeHeight);

				if (config.updateStripeSine)
				{
					updateFloat(renderer, "_StripeSineEnabled", state.stripeSineEnabled ? 1 : 0);
					updateFloat(renderer, "_StripeSineDitherEnabled", state.stripeSineDitherEnabled ? 1 : 0);
					updateFloat(renderer, "_StripeDitherPow", state.stripeDitherPow);
					updateFloat(renderer, "_StripeSinePeriod", state.stripeSinePeriod);
					updateFloat(renderer, "_StripeSineTwiddle", state.stripeSineTwiddle);
					updateFloat(renderer, "_StripeSineTwaddle", state.stripeSineTwaddle);
					applyControlVal(renderer, "_StripeSineOffset", ref state.stripeSineOffset, config.stripeSineOffsetControl);
					applyControlVal(renderer, "_StripeSineThreshold", ref state.stripeSineThreshold, config.stripeSineThresholdControl);
				}
				if (config.updateWipe)
				{
					updateFloat(renderer, "_WipeEnabled", state.wipeEnabled ? 1 : 0);
					updateFloat(renderer, "_WipeSineDitherEnabled", state.wipeSineDitherEnabled ? 1 : 0);
					updateFloat(renderer, "_WipeDitherPow", state.wipeDitherPow);
					updateFloat(renderer, "_WipeNormalized", state.wipeNormalized ? 1 : 0);
					updateFloat(renderer, "_WipeSlope", state.wipeSlope);
					applyControlVal(renderer, "_WipeYOffset", ref state.wipeYOffset, config.wipeYOffsetControl);
					updateFloat(renderer, "_WipeSinePeriod", state.wipeSinePeriod);
					updateFloat(renderer, "_WipeSineScale", state.wipeSineScale);
					updateFloat(renderer, "_WipeSineInvertAxes", state.wipeSineInvertAxes ? 1 : 0);
					applyControlVal(renderer, "_WipeSineOffset", ref state.wipeSineOffset, config.wipeSineOffset);
					applyControlVal(renderer, "_WipeSineThreshold", ref state.wipeSineThreshold, config.wipeSineThreshold);
					updateFloat(renderer, "_WipeSineTwiddle", state.wipeSineTwiddle);
					updateFloat(renderer, "_WipeSineTwaddle", state.wipeSineTwaddle);
					updateFloat(renderer, "_WipeSineShapePeriod", state.wipeSineShapePeriod);
					applyControlVal(renderer, "_WipeSineShapeOffset", ref state.wipeSineShapeOffset, config.wipeSineShapeOffset);
					applyControlVal(renderer, "_WipeSineShapeScale", ref state.wipeSineShapeScale, config.wipeSineShapeScale);
					updateFloat(renderer, "_WipeSineShapeTwiddle", state.wipeSineShapeTwiddle);
					updateFloat(renderer, "_WipeSineShapeTwaddle", state.wipeSineShapeTwaddle);
				}

				if (config.updateRadial)
				{
					updateFloat(renderer, "_RadialEnabled", state.radialEnabled ? 1 : 0);
					updateFloat(renderer, "_RadialSineDitherEnabled", state.radialSineDitherEnabled ? 1 : 0);
					updateFloat(renderer, "_RadialDitherPow", state.radialDitherPow);
					updateFloat(renderer, "_RadialNormalized", state.radialNormalized ? 1 : 0);
					updateFloat(renderer, "_RadialSinePeriod", state.radialSinePeriod);
					applyControlVal(renderer, "_RadialSineOffset", ref state.radialSineOffset, config.radialSineOffsetControl);
					applyControlVal(renderer, "_RadialSineThreshold", ref state.radialSineThreshold, config.radialSineThresholdControl);
					updateFloat(renderer, "_RadialSineScale", state.radialSineScale);
					updateFloat(renderer, "_RadialMinimum", state.radialMinimum);
					updateVector(renderer, "_RadialArcStart", state.radialArcStart);
					updateVector(renderer, "_RadialArcStop", state.radialArcStop);
					updateFloat(renderer, "_RadialX", state.radialPos.x);
					updateFloat(renderer, "_RadialY", state.radialPos.y);
				}

				if (config.updateRing)
				{
					updateFloat(renderer, "_RingSineDitherEnabled", state.ringSineDitherEnabled ? 1 : 0);
					updateFloat(renderer, "_RingDitherPow", state.ringDitherPow);
					updateFloat(renderer, "_RingEnabled", state.ringEnabled ? 1 : 0);
					updateFloat(renderer, "_RingNormalized", state.ringNormalized ? 1 : 0);
					updateFloat(renderer, "_RingEnabled", state.ringEnabled ? 1 : 0);
					updateFloat(renderer, "_RingSinePeriod", state.ringSinePeriod);
					updateFloat(renderer, "_RingSineScale", state.ringSineScale);
					updateFloat(renderer, "_RingSineThreshold", state.ringSineThreshold);
					applyControlVal(renderer, "_RingSineOffset", ref state.ringSineOffset, config.ringSineOffsetControl);
					applyControlVal(renderer, "_RingSineThreshold", ref state.ringSineThreshold, config.ringSineThresholdControl);
					updateVector(renderer, "_RingScale", state.ringScale);
					updateFloat(renderer, "_RingX", state.ringPos.x);
					updateFloat(renderer, "_RingY", state.ringPos.y);
				}

				if (config.updateDither)
				{
					updateFloat(renderer, "_DitherEnabled", state.ditherAll ? 1 : 0);
					updateFloat(renderer, "_DitherThreshold", state.ditherAllThreshold);
					updateFloat(renderer, "_DitherScale", state.ditherScale);
					updateTexture(renderer, "_DitherTexture", state.ditherTexture);
					applyControlVal(renderer, "_DitherOffsetX", ref state.ditherOffsetX, config.ditherAllOffsetX);
					applyControlVal(renderer, "_DitherOffsetY", ref state.ditherOffsetY, config.ditherAllOffsetY);
					applyControlVal(renderer, "_DitherThreshold", ref state.ditherAllThreshold, config.ditherAllThreshold);
					applyControlVal(renderer, "_DitherBandVal", ref state.ditherBandVal, config.ditherBandVal);
					applyControlVal(renderer, "_DitherBandWidth", ref state.ditherBandWidth, config.ditherBandWidth);
				}
			}
		}


	}

	private void updateFloat(MeshRenderer renderer, string name, float state)
	{
		if (!Application.isPlaying)
		{
			renderer.sharedMaterial.SetFloat(name, state);
			return;
		}

		renderer.material.SetFloat(name, state);
	}

	private void updateVector(MeshRenderer renderer, string name, Vector2 state)
	{
		if (!Application.isPlaying)
		{
			renderer.sharedMaterial.SetVector(name, state);
			return;
		}

		renderer.material.SetVector(name, state);
	}

	private void updateTexture(MeshRenderer renderer, string name, Texture state)
	{
		if (!Application.isPlaying)
		{
			renderer.sharedMaterial.SetTexture(name, state);
			return;
		}

		renderer.material.SetTexture(name, state);
	}

	private void applyControlVal(MeshRenderer renderer, string name, ref float state, ControlVal control)
	{
		if (!Application.isPlaying)
		{
			renderer.sharedMaterial.SetFloat(name, state);
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
		renderer.material.SetFloat(name, assign);
	}


}
