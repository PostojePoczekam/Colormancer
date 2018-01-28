using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandFloat : MonoBehaviour
{
	public Vector3 from, to;
	public float floatSpeed = 1f;
	private float _timer = 0f;

	private void Update()
	{
		transform.position = Vector3.Lerp(from, to, Mathf.PingPong(_timer, 1f));
		_timer += Time.deltaTime * floatSpeed;
	}
}
