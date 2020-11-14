using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
	public Transform anchor;

	public float swingMagnitude;
	public float swingPeriod;
	private float angle = 0;
	private float magnitude = 0;
	private float startZ = 0;

	private float offsetAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
		startZ = transform.position.z;
		Vector3 vec = transform.position - anchor.position;
		vec.z = 0;
		angle = MathUtil.VectorToAngle(vec);
		magnitude = vec.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
		offsetAngle += Time.fixedDeltaTime * swingPeriod;

		Vector3 targetVec = MathUtil.AngleToVector(Mathf.Sin(offsetAngle) * swingMagnitude + angle); 
		Vector3 newPos = anchor.position + targetVec * magnitude;
		newPos.z = startZ;
		transform.position = newPos;
    }
}
