using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimator : MonoBehaviour
{
	public float animationSpeed = 1f;

	private Vector3 _basePosition;
	private float _xSeed, _ySeed;

	private void Awake()
	{
		_basePosition = transform.localPosition;
		_xSeed = Random.Range(0f, 1f);
		_ySeed = Random.Range(0f, 1f);
	}
	private void Update()
	{
		transform.localPosition = _basePosition + new Vector3(Mathf.PerlinNoise(_xSeed, Time.time) - 0.5f, Mathf.PerlinNoise(Time.time, _ySeed) - 0.5f, 0f) * animationSpeed;
	}

}
