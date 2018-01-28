using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	public float timeFactor { get; private set; } = 1f;

	public void SlowDown()
	{
		timeFactor = 0.1f;
	}

	private void Update()
	{
		timeFactor = Mathf.Lerp(timeFactor, 1f, Time.deltaTime * 0.3f);
	}
}
