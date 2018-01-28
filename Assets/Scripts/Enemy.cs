using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Game;

public class Enemy : MonoBehaviour
{
	public static event System.Action onEnemyKilled;
	public GameObject projectilePrefab;
	public GameObject explosion;

	private float _value = 1f;
	private float _hue = 0f;
	private bool _alive = true;
	private float _shootTimer = 0f;
	private float _evadeTimer = 0f;
	private float _jumpTimer = 0f;


	private MeshRenderer[] _meshRenderers;
	private Rigidbody _rigidBody;
	private Transform _target;
	public void SetTarget(Transform target)
	{
		_target = target;
	}

	public void Drain(float hue, float multiplier = 20f)
	{
		float delta = Mathf.Abs(Mathf.DeltaAngle(hue * 360f, _hue * 360f) / 360f);
		_value = Mathf.Clamp01(_value -= Time.deltaTime * delta * multiplier);
		foreach (var meshRenderer in _meshRenderers)
			meshRenderer.material.color = Color.HSVToRGB(_hue, _value, 0.5f + _value / 2f);
		_rigidBody.AddForce((transform.position - _target.position).normalized + Vector3.up * 0.3f, ForceMode.Impulse);
		if (_value == 0 && _alive)
			Die();
	}

	private void Awake()
	{
		_hue = Random.Range(0f, 1f);
		_meshRenderers = GetComponentsInChildren<MeshRenderer>();
		_rigidBody = GetComponent<Rigidbody>();
		foreach (var meshRenderer in _meshRenderers)
		{
			meshRenderer.material = new Material(meshRenderer.material);
			meshRenderer.material.color = Color.HSVToRGB(_hue, 1f, 1f);
		}
	}

	private void Update()
	{
		if (!_alive)
			return;
		if (Vector3.Distance(_target.position, transform.position) > 25f && _evadeTimer > 1f)
			_rigidBody.AddForce((_target.position - transform.position).normalized * game.timeManager.timeFactor, ForceMode.VelocityChange);
		if (Vector3.Distance(_target.position, transform.position) < 15f && _evadeTimer > 2f)
		{
			_rigidBody.AddForce((transform.position - _target.position).normalized * 10f * game.timeManager.timeFactor + Vector3.up * Random.Range(2f, 8f), ForceMode.Impulse);
			_evadeTimer = 0f;
		}
		transform.LookAt(_target);
		transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
		_evadeTimer += Time.deltaTime;
		_shootTimer += Time.deltaTime;
		_jumpTimer += Time.deltaTime;
		if (_shootTimer > 2f && Vector3.Distance(_target.position, transform.position) < 20f)
		{
			Shoot();
			_shootTimer = 0f;
		}

		if (_jumpTimer > Random.Range(4f, 8f))
		{
			if (Random.Range(0, 2) == 0)
				_rigidBody.AddForce(transform.right * 10f * game.timeManager.timeFactor, ForceMode.Impulse);
			else
				_rigidBody.AddForce(-transform.right * 10f * game.timeManager.timeFactor, ForceMode.Impulse);
			_jumpTimer = 0f;
		}
	}

	private void Shoot()
	{
		GameObject projectile = Instantiate(projectilePrefab);
		projectile.SetActive(true);
		projectile.transform.position = transform.position + transform.forward;
		projectile.GetComponent<Projectile>().SetDirection((_target.position - transform.position).normalized);
	}

	private void Die()
	{
		_alive = false;
		explosion.SetActive(true);
		_rigidBody.constraints = RigidbodyConstraints.None;
		_rigidBody.AddForceAtPosition((transform.position - _target.position).normalized * 40f + Vector3.up * 20f, Vector3.up, ForceMode.Impulse);
		Enemy[] enemies = FindObjectsOfType<Enemy>();
		for (int x = 0; x < enemies.Length; x++)
		{
			if (Vector3.Distance(enemies[x].transform.position, transform.position) < 10f)
			{
				enemies[x].Drain(_hue, 30);
				enemies[x].GetComponent<Rigidbody>().AddForce((enemies[x].transform.position - transform.position).normalized * 10f, ForceMode.Impulse);

			}
		}
		StartCoroutine(ByeBye());
		onEnemyKilled?.Invoke();
	}

	private IEnumerator ByeBye()
	{
		yield return new WaitForSeconds(5f);
		Destroy(gameObject);
	}
}
