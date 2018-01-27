using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereShield : MonoBehaviour
{
	public float targetSize = 20f;
	public AnimationCurve animationCurve;


	private bool _isCasting = false;
	private float _timer = 0f;

	public void Cast()
	{
		_isCasting = true;
	}

	private void Update()
	{
		transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * targetSize, animationCurve.Evaluate(_timer));
		if (!_isCasting)
		{
			_timer = 0f;
		}
		else
		{
			_timer += Time.deltaTime * 5f;
		}
		if (_timer > 1f)
		{
			_timer = 0f;
			_isCasting = false;
		}
	}

	void OnTriggerStay(Collider other)
	{
		Destroy(other.gameObject);
	}
}
