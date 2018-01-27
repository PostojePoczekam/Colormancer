using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColorPool : MonoBehaviour
{
	public event Action<float> onHueCaptured;
	public float captureSpped = 1f;

	private float _hue = 0f;
	private float _value = 1f;

	public void Fill(float capturedHue)
	{
		_hue = Mathf.LerpAngle(_hue * 360, capturedHue * 360, Time.deltaTime * captureSpped) / 360f;
		_value = Mathf.Clamp01(_value += Time.deltaTime * captureSpped);
		onHueCaptured?.Invoke(_hue);
	}

	public bool Empty()
	{
		return true;
	}
}
