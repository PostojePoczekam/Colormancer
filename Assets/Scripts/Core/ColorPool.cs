using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColorPool : MonoBehaviour
{
	public event Action<float> onColorChanged;
	public float captureSpped = 1f;

	public float hue
	{
		get
		{
			return _hue;
		}
	}
	public float value
	{
		get
		{
			return _value;
		}
	}

	private float _hue = 0f;
	private float _value = 1f;

	public void Fill(float capturedHue)
	{
		_hue = Mathf.LerpAngle(_hue * 360, capturedHue * 360, Time.deltaTime * captureSpped) / 360f;
		_hue = Mathf.Repeat(_hue, 1f);
		_value = Mathf.Clamp01(_value += Time.deltaTime * captureSpped);
		onColorChanged?.Invoke(_hue);
	}

	public void Empty()
	{
		_value = Mathf.Clamp01(_value -= Time.deltaTime * captureSpped / 4f);
		onColorChanged?.Invoke(_hue);
	}

	public void EmptyAll()
	{
		_value = 0f;
		onColorChanged?.Invoke(_hue);
	}
}
