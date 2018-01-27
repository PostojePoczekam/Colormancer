using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public float rotationSpeed = 1f;
	public float distance = 5f;

	private void Awake()
	{
		Enemy.onEnemyKilled += ShakeCamera;
	}

	private void LateUpdate()
	{
		Vector3 rotationAngles = transform.localEulerAngles;
		rotationAngles.x -= Input.GetAxis("Mouse Y") * rotationSpeed;
		rotationAngles.y += Input.GetAxis("Mouse X") * rotationSpeed;
		if (rotationAngles.x > 60 && rotationAngles.x < 180) rotationAngles.x = 60;
		else if (rotationAngles.x < 300 && rotationAngles.x > 180) rotationAngles.x = 300;
		transform.localEulerAngles = rotationAngles;
		Vector3 distanceVector = new Vector3(0f, 0f, -distance);
		transform.localPosition = transform.localRotation * distanceVector + Vector3.up * 5;
		transform.localRotation *= shakeQuaternion;
	}

	#region shake
	public float shakeAmount;
	public float shakeDuration;

	float shakePercentage;
	float shakeStrenght;
	float startAmount;
	float startDuration;
	float shakeTime;

	bool isShaking = false;
	Quaternion shakeQuaternion = Quaternion.identity;

	public bool smooth;
	public float smoothAmount = 5f;

	void ShakeCamera()
	{

		startAmount = shakeAmount;
		shakeStrenght = shakeAmount;
		shakeTime = shakeDuration;
		startDuration = shakeDuration;
		if (!isShaking) StartCoroutine(Shake());
	}

	IEnumerator Shake()
	{
		isShaking = true;

		while (shakeTime > 0.04f)
		{
			Vector3 rotationAmount = Random.insideUnitSphere * shakeStrenght;
			rotationAmount.z = 0;

			shakePercentage = shakeTime / startDuration;

			shakeStrenght = startAmount * shakePercentage;
			shakeTime = Mathf.Lerp(shakeTime, 0, Time.deltaTime);


			if (smooth)
				shakeQuaternion = Quaternion.Lerp(shakeQuaternion, Quaternion.Euler(rotationAmount), Time.deltaTime * smoothAmount);
			else
				shakeQuaternion = Quaternion.Euler(rotationAmount);

			yield return null;
		}
		shakeQuaternion = Quaternion.identity;
		isShaking = false;
	}
	#endregion
}
