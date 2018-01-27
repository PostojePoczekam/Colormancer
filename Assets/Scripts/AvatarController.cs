using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Game;

[RequireComponent(typeof(Rigidbody))]
public class AvatarController : MonoBehaviour
{
	public float moveSpeed = 1f;
	public float jumpForce = 1f;
	public Transform cameraTransform;
	public Transform bodyTransform;
	public Transform leftHandTransform;
	public Transform rightHandTransform;
	public SphereShield shield;

	private Rigidbody _rigidBody;
	private float _jumpTimer = 0f;

	private void Awake()
	{
		_rigidBody = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		Vector3 directionVector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
		MoveAvatar(directionVector);
		if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
			RotateAvatar();
		else
			RotateAvatar(directionVector);
		if (Input.GetMouseButton(0))
			ShootColor();
		if (Input.GetMouseButton(1))
			CaptureColor();
		game.particleAttractor.Move((leftHandTransform.position + rightHandTransform.position) / 2f);
		if (Input.GetKeyDown(KeyCode.LeftAlt) && game.colorPool.value != 0)
		{
			shield.Cast();
			game.colorPool.EmptyAll();
		}
		if (Input.GetKeyDown(KeyCode.Space))
		{
			_rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			_jumpTimer = 0f;
		}
		_jumpTimer += Time.deltaTime;
		if (transform.position.y < -10f)
			transform.position = Vector3.zero;
		if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
			game.particleAttractor.Show();
		else
			game.particleAttractor.Hide();
	}

	private void MoveAvatar(Vector3 direction)
	{
		float yVelocity = _rigidBody.velocity.y;
		_rigidBody.velocity = Quaternion.Euler(0, cameraTransform.localEulerAngles.y, 0) * direction * moveSpeed;
		_rigidBody.velocity = new Vector3(_rigidBody.velocity.x, yVelocity, _rigidBody.velocity.z);
	}

	private void RotateAvatar(Vector3 direction)
	{
		if (direction.sqrMagnitude == 0)
			return;
		Vector3 rotatedDirection = Quaternion.AngleAxis(cameraTransform.eulerAngles.y, Vector3.up) * direction;
		Quaternion targetRotation = Quaternion.LookRotation(rotatedDirection, Vector3.up);
		bodyTransform.rotation = Quaternion.Slerp(bodyTransform.rotation, targetRotation, Time.deltaTime * 10f);
	}

	private void RotateAvatar()
	{
		Vector3 rotatedDirection = Quaternion.AngleAxis(cameraTransform.eulerAngles.y, Vector3.up) * Vector3.forward;
		Quaternion targetRotation = Quaternion.LookRotation(rotatedDirection, Vector3.up);
		bodyTransform.rotation = Quaternion.Slerp(bodyTransform.rotation, targetRotation, Time.deltaTime * 10f);
	}

	private void CaptureColor()
	{
		Vector3 point;
		float hue;
		if (!game.colorRaycaster.RaycastColor(out point, out hue))
			return;
		game.particleAttractor.Attract(Camera.main.transform.position + Camera.main.transform.forward * 10f);
		game.colorPool.Fill(hue);
	}

	private void ShootColor()
	{
		Vector3 point;
		if (game.colorPool.value == 0f)
			return;
		game.particleAttractor.Attract(Camera.main.transform.position + Camera.main.transform.forward * 10f);
		game.colorPool.Empty();
		game.colorRaycaster.RaycastEnemy(out point, game.colorPool.hue);
	}
}
