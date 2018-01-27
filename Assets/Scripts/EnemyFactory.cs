using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
	public GameObject enemyPrefab;
	public float spawnPeriod = 10f;

	private GameObject _playerAvatar;
	private float _timer = 0f;

	private void SpawnEnemy()
	{
		Enemy enemy = Instantiate(enemyPrefab).GetComponent<Enemy>();
		enemy.gameObject.SetActive(true);
		enemy.SetTarget(_playerAvatar.transform);
	}

	private void Awake()
	{
		_playerAvatar = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		spawnPeriod = Mathf.Lerp(spawnPeriod, 1f, Time.deltaTime * 0.01f);
		_timer += Time.deltaTime;
		if (_timer > spawnPeriod)
		{
			SpawnEnemy();
			_timer = 0f;
		}
	}
}
