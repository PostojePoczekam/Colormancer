using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	private Vector3 _direction;

	public void SetDirection(Vector3 direction)
	{
		_direction = direction;
		StartCoroutine(ByeBye());
	}

	private void Update()
	{
		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * 3f);
		transform.position += _direction * 0.2f;
	}

	private IEnumerator ByeBye()
	{
		yield return new WaitForSeconds(10f);
		Destroy(gameObject);
	}
}
