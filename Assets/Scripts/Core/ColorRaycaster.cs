using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Game;

public class ColorRaycaster : MonoBehaviour
{
	public LayerMask enemyLayer;
	public LayerMask colorLayer;

	public bool RaycastEnemy(out Vector3 point, float hue)
	{
		point = Vector3.zero;
		if (game.colorPool.value == 0)
			return false;
		RaycastHit raycastHit;
		bool valid = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out raycastHit, 100f, enemyLayer);
		if (valid)
		{
			point = raycastHit.point;
			raycastHit.transform.GetComponent<Enemy>().Drain(hue);
		}
		return valid;
	}

	public bool RaycastColor(out Vector3 point, out float hue)
	{
		point = Vector3.zero;
		hue = 0f;
		RaycastHit raycastHit;
		bool valid = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out raycastHit, 100f, colorLayer);
		if (valid)
		{
			point = raycastHit.point;
			valid = raycastHit.transform.GetComponent<ColorObject>().Drain(out hue);
		}
		return valid;
	}
}
