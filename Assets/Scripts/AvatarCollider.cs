using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarCollider : MonoBehaviour
{
	public static event System.Action<float> onHealthChanged;
	//This shouldnt be here, lel
	//Jam game, lmao
	private float _health = 1f;
	void OnTriggerEnter(Collider other)
	{
		_health -= 0.1f;
		Destroy(other.gameObject);
		onHealthChanged?.Invoke(_health);
		if (_health < 0f)
			Application.Quit();
	}
}
