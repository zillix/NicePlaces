using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static bool Started = false;

	public List<PlaceData> Places = new List<PlaceData>();
	private Dictionary<int, GameObject> indexToInstanceMap = new Dictionary<int, GameObject>();
	public int activeIndex = 0;
	public Renderer mainRenderer;
    // Start is called before the first frame update
    void Start()
    {
		Started = true;
		setUp(activeIndex);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			cycle(-1);
		}
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			cycle(1);
		}
    }

	private void cycle(int dir)
	{
		int previousIndex = activeIndex;
		if (dir < 0 && Mathf.Abs(dir) > activeIndex)
		{
			activeIndex = Places.Count + dir;
		}
		else
		{
			activeIndex = (activeIndex + dir) % Places.Count;
		}

		transition(previousIndex, activeIndex);
	}

	private void transition(int previousIndex, int newIndex)
	{
		setUp(previousIndex, newIndex, true);

		PlaceData oldData = Places[previousIndex];
		PlaceData newData = Places[newIndex];
		Material transMaterial = mainRenderer.material;
		TransitionType transType = newData.transitionType;
		StartCoroutine(doTransition(transMaterial, transType, newData.tickTime, newData.transitionPerTick));
	}

	private void setUp(int index, int index2 = -1, bool useIndex2 = false)
	{
		if (Places.Count <= index)
			return;

		PlaceData transitionPlace = useIndex2 ? Places[index2] : Places[index];

		PlaceData data = Places[index];
		mainRenderer.material = transitionPlace.transitionMaterial;
		mainRenderer.material.SetTexture("_Texture1", data.renderTexture);
		string transitionFieldName = getTransitionFieldName(transitionPlace.transitionType);
		mainRenderer.material.SetFloat(transitionFieldName, getTransitionStart(transitionPlace.transitionType));

		if (index2 >= 0)
		{
			PlaceData data2 = Places[index2];
			mainRenderer.material.SetTexture("_Texture2", data2.renderTexture);
		}
	}

	private IEnumerator doTransition(Material transitionMaterial, TransitionType transType, float tickTime, float amtPerTick)
	{
		float transitionVal = getTransitionStart(transType);
		float valPerFrame = amtPerTick;
		string transitionFieldName = getTransitionFieldName(transType);
		
		while (transitionVal < getTransitionEnd(transType))
		{
			transitionVal += valPerFrame;
			transitionMaterial.SetFloat(transitionFieldName, transitionVal);
			yield return new WaitForSeconds(tickTime);
		}
	}

	private float getTransitionStart(TransitionType transType)
	{
		switch (transType)
		{
			case TransitionType.Dither:
				return 0;
			default:
				return -1;
		}
	}

	private float getTransitionEnd(TransitionType transType)
	{
		switch (transType)
		{
			default:
				return 1;
		}
	}

	private string getTransitionFieldName(TransitionType transType)
	{
		switch (transType)
		{
			default:
			case TransitionType.None:
				return "";
			case TransitionType.Radial:
				return "_RadialSineThreshold";
			case TransitionType.Ring:
				return "_RingSineThreshold";
			case TransitionType.SineStripe:
				return "_StripeSineThreshold";
			case TransitionType.Wipe:
				return "_WipeSineThreshold";
			case TransitionType.Dither:
				return "_DitherThreshold";
		}
	}
}
