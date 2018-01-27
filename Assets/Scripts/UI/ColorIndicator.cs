using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Game;

public class ColorIndicator : MonoBehaviour
{
	private RectTransform _rectTransform;

	private void Awake()
	{
		_rectTransform = transform as RectTransform;
		game.colorPool.onColorChanged += Rotate;
	}

	private void Rotate(float targetRotation)
	{
		_rectTransform.rotation = Quaternion.Euler(0f, 0f, -targetRotation * 360);
	}
}
