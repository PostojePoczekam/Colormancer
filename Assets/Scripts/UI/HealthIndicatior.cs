using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthIndicatior : MonoBehaviour
{
	public Image image
	{
		get
		{
			if (_image == null)
				_image = GetComponent<Image>();
			return _image;
		}
	}
	private Image _image;

	private void Awake()
	{
		_image = GetComponent<Image>();
		AvatarCollider.onHealthChanged += UpdateHealth;
	}

	private void UpdateHealth(float health)
	{
		image.fillAmount = health;
	}
}
