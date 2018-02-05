using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
	//singleton af
	private static Game _game;
	public static Game game
	{
		get
		{
			if (_game == null)
				_game = FindObjectOfType<Game>();
			return _game;
		}
	}

	public ColorPool colorPool;
	public ColorRaycaster colorRaycaster;
	public ParticleAttractor particleAttractor;
	public TimeManager timeManager;

	private void Awake()
	{
		Cursor.visible = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}
}
