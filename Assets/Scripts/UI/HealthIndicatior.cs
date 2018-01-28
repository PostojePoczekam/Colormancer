using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicatior : MonoBehaviour
{
	private Image _image;

	private void Awake()
	{
		_image = GetComponent<Image>();
		AvatarCollider.onHealthChanged += UpdateHealth;
	}

	private void UpdateHealth(float health)
	{
		Color color = _image.color;
		color.a = health;
		_image.color = color;
	}
}
