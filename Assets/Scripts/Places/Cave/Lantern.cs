using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour
{
	public ShaderAnimController controlledController;
	public Swing swing;

	private float startArcOffsetAngle;
	private float stopArcOffsetAngle;
	private float radialSineOffset;

	public Camera myCam;

	private Vector2 offset;

	public void Start()
	{
		Vector3 delta = swing.transform.position - swing.anchor.position;
		Vector3 norm = new Vector3(-delta.y, delta.x);


		startArcOffsetAngle = -Vector3.Angle(norm, controlledController.state.radialArcStart);
		stopArcOffsetAngle = Vector3.Angle(norm, controlledController.state.radialArcStop);
		radialSineOffset = controlledController.state.radialSineOffset;

		Vector2 pos = swing.transform.position;
		pos = myCam.WorldToScreenPoint(pos);
		if (controlledController.state.radialNormalized)
		{
			pos = new Vector2(pos.x / Screen.width, pos.y / Screen.height);
		}
		offset = controlledController.state.radialPos - pos;

	}
	public void Update()
	{

		Vector3 delta = swing.transform.position - swing.anchor.position;
		Vector3 norm = new Vector3(-delta.y, delta.x);
		float normAngle = MathUtil.VectorToAngle(norm);

		Vector3 radialArcStart = MathUtil.AngleToVector(normAngle + startArcOffsetAngle);
		Vector3 radialArcStop = MathUtil.AngleToVector(normAngle + stopArcOffsetAngle);
		controlledController.state.radialArcStart = radialArcStart;
		controlledController.state.radialArcStop = radialArcStop;
		controlledController.state.radialSineOffset = radialSineOffset + MathUtil.toRadians(-normAngle);
		Vector3 pos = swing.transform.position;
		pos = myCam.WorldToScreenPoint(pos);
		if (controlledController.state.radialNormalized)
		{
			pos = new Vector3(pos.x / Screen.width, pos.y / Screen.height);
		}

		Vector3 rotatedOffset = MathUtil.RotateVector(offset, normAngle);

		controlledController.state.radialPos = pos + rotatedOffset;

		transform.localRotation = Quaternion.Euler(0, 0, normAngle);
	}
}
