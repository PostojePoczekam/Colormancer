using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : MonoBehaviour
{
	private float _value = 1f;
	private float _hue = 0f;

	private MeshRenderer _meshRenderer;
	private void Awake()
	{
		_hue = Random.Range(0f, 1f);
		_meshRenderer = GetComponent<MeshRenderer>();
		_meshRenderer.material = new Material(_meshRenderer.material);
		_meshRenderer.material.color = Color.HSVToRGB(_hue, 1f, _value);
	}

	public bool Drain(out float hue)
	{
		hue = _hue;
		_value = Mathf.Clamp01(_value -= Time.deltaTime);
		_meshRenderer.material.color = Color.HSVToRGB(_hue, 1f, _value);
		return _value != 0f;
	}
}
