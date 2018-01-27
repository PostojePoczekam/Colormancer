using System.Collections;
using UnityEngine;
[RequireComponent(typeof(ParticleSystem))]
public class ParticleAttractor : MonoBehaviour
{
	public float speed = 5f;

	private ParticleSystem _particleSystem;
	private ParticleSystem.Particle[] _particles;
	private int _particlesCount;

	public void Move(Vector3 target)
	{
		transform.position = target;
	}

	public void Show()
	{
		if (!gameObject.activeInHierarchy)
			gameObject.SetActive(true);
	}

	public void Hide()
	{
		if (gameObject.activeInHierarchy)
			gameObject.SetActive(false);
	}

	public void Attract(Vector3 target)
	{
		_particles = new ParticleSystem.Particle[_particleSystem.main.maxParticles];
		_particlesCount = _particleSystem.GetParticles(_particles);
		float step = speed * Time.deltaTime;
		for (int i = 0; i < _particlesCount; i++)
		{
			_particles[i].position = Vector3.LerpUnclamped(_particles[i].position, target, step);
		}
		_particleSystem.SetParticles(_particles, _particlesCount);
	}

	private void Awake()
	{
		_particleSystem = GetComponent<ParticleSystem>();
	}
}
