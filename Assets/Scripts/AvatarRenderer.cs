using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Game;

public class AvatarRenderer : MonoBehaviour
{
	public SkinnedMeshRenderer robeRenderer;

	private void Awake()
	{
		game.colorPool.onColorChanged += UpdateColor;
		robeRenderer.materials[0].color = Color.HSVToRGB(1f, 1f, 1f);
		robeRenderer.materials[2].color = Color.HSVToRGB(1f, 1f, 1f);
	}

	private void UpdateColor(float hue)
	{
		robeRenderer.materials[0].color = Color.HSVToRGB(game.colorPool.hue, game.colorPool.value, 0.5f + game.colorPool.value / 2f);
		robeRenderer.materials[2].color = Color.HSVToRGB(game.colorPool.hue, game.colorPool.value, 0.5f + game.colorPool.value / 2f);
	}
}
