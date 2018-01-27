using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorRaycaster : MonoBehaviour
{
	public LayerMask enemyLayer;
	public LayerMask colorLayer;

	public bool RaycastEnemy(out Vector3 point, out float hue)
	{
		point = Vector3.zero;
		hue = 0f;
		RaycastHit raycastHit;
		bool hit = Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out raycastHit, 100f, enemyLayer);
		if (hit)
		{
			point = raycastHit.point;
		}
		return hit;
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
