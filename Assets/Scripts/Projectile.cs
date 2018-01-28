using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Game;

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
		transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * 3f * game.timeManager.timeFactor);
		transform.position += _direction * 0.3f * game.timeManager.timeFactor;
	}

	private IEnumerator ByeBye()
	{
		yield return new WaitForSeconds(10f);
		Destroy(gameObject);
	}
}
