﻿using UnityEngine;
using System.Collections;

public class MathUtil {

	public static Vector3 AngleToVector(float degrees)
	{
		return new Vector3(Mathf.Cos(Mathf.Deg2Rad * degrees),
			Mathf.Sin(Mathf.Deg2Rad * degrees)).normalized;
	}

	public static float VectorToAngle(Vector3 vector)
	{
		return Mathf.Rad2Deg * Mathf.Atan2(vector.y, vector.x);
	}

	public static float AngleBetween(float angle1, float angle2)
	{
		// This is pretty hilariously bad
		return Vector3.Angle(AngleToVector(angle1), AngleToVector(angle2));
	}

	public static float RotateAngle(float startAngle, float targetAngle, float delta)
	{
		Vector3 startVec = AngleToVector(startAngle);
		Vector3 targetVec = AngleToVector(targetAngle);

		float diff = Vector3.Angle(startVec, targetVec);
        if (diff < delta)
		{
			return targetAngle;
		}

		Vector3 rightStart = new Vector3(startVec.y, -startVec.x);

		// If the right-facing vector is in the direction of the target, rotate that way
		if (Vector3.Dot(rightStart, targetVec) > 0)
		{
			startAngle -= delta;
		}
		else
		{
			startAngle += delta;
		}

		return startAngle;

	}

	public static Vector3 RotateVector(Vector3 start, Vector3 end, float angleDelta)
	{
		float startMagnitude = start.magnitude;
		// Super inefficient
		float targetAngle = RotateAngle(VectorToAngle(start), VectorToAngle(end), angleDelta);
		return AngleToVector(targetAngle) * startMagnitude;
	}

	public static bool SignsMatch(float val1, float val2)
	{
		return (val1 > 0 && val2 > 0) || (val1 == 0 && val2 == 0) || (val1 < 0 && val2 < 0);
	}

	public static Vector3 RotateVector(Vector3 input, float rotateZDegrees)
	{
		// NOTE(alex): This just ignores the z coord entirely!

		float cosine = Mathf.Cos(toRadians(rotateZDegrees));
		float sine = Mathf.Sin(toRadians(rotateZDegrees));
		return new Vector3(
			cosine * input.x - sine * input.y,
			sine * input.x + cosine * input.y,
			input.z);
	}

	public static float toRadians(float degrees)
	{
		return degrees * Mathf.PI / 180;
	}

	public static float toDegrees(float radians)
	{
		return radians * 180 / Mathf.PI;
	}
}
