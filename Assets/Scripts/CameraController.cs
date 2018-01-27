using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float rotationSpeed = 1f;
	public float distance = 5f;

	private void LateUpdate()
	{
		Vector3 rotationAngles = transform.localEulerAngles;
		rotationAngles.x -= Input.GetAxis("Mouse Y") * rotationSpeed;
		rotationAngles.y += Input.GetAxis("Mouse X") * rotationSpeed;
		if (rotationAngles.x > 60 && rotationAngles.x < 180) rotationAngles.x = 60;
		else if (rotationAngles.x < 300 && rotationAngles.x > 180) rotationAngles.x = 300;
		transform.localEulerAngles = rotationAngles;
		Vector3 distanceVector = new Vector3(0f, 0f, -distance);
		transform.localPosition = transform.localRotation * distanceVector + Vector3.up * 2;
	}
}
