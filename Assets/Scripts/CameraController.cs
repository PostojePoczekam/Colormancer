using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using static Game;

public class CameraController : MonoBehaviour
{
	public float rotationSpeed = 1f;
	public float distance = 5f;
	public PostProcessingProfile profile;

	private void Awake()
	{
		AvatarCollider.onHealthChanged += (hp) =>
		{
			ShakeCamera();
			game.timeManager.SlowDown();
		};
		Enemy.onEnemyKilled += ShakeCamera;
		localAngles = transform.localEulerAngles;
	}

	private Vector3 localAngles;
	private void LateUpdate()
	{
		Vector3 rotationAngles = localAngles;
		rotationAngles.x -= Input.GetAxis("Mouse Y") * rotationSpeed;
		rotationAngles.y += Input.GetAxis("Mouse X") * rotationSpeed;
		if (rotationAngles.x > 60 && rotationAngles.x < 180) rotationAngles.x = 60;
		else if (rotationAngles.x < 300 && rotationAngles.x > 180) rotationAngles.x = 300;
		localAngles = rotationAngles;
		Vector3 distanceVector = new Vector3(0f, 0f, -distance);
		transform.localEulerAngles = localAngles;
		transform.localPosition = transform.localRotation * distanceVector + Vector3.up * 5;
		transform.localRotation *= shakeQuaternion;
	}

	private void Update()
	{
		var colorSettings = profile.colorGrading.settings;
		colorSettings.basic.saturation = game.timeManager.timeFactor;
		profile.colorGrading.settings = colorSettings;

		var bloomSettings = profile.bloom.settings;
		bloomSettings.bloom.intensity = 0.1f / game.timeManager.timeFactor;
		profile.bloom.settings = bloomSettings;
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
