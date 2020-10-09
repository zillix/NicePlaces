using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwiddleTest : MonoBehaviour
{
	public float period = 1;
	public float twiddle = 1;
	public float offset = 0;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; ++i)
		{

			float sinInput = 0;
			float sinAngle = i + offset;
			sinInput = -twiddle * Mathf.Cos(sinAngle) + sinAngle * (period * twiddle);
			float sine = Mathf.Sin(sinInput);
			Debug.Log(sine);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
