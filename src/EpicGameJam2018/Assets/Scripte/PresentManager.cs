using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PresentManager : MonoBehaviour {

	//The prefab
	public GameObject Present;
	//all instances of the prefab
	private List<Present> _allPresents = new List<Present>();
	private Transform _presentHolder;

	//Left screen border in worldspace = - stageDimensions.x,
	//right screen border in wordlspace = stageDimensions.x,
	//center of screen in worldspace = 0;
	//thus spawning range x = [-_stageDimensions.x, _stageDimensions.x];
	private Vector3 _stageDimensions;
	public float ScreenSpawnPaddingX = 0.05f;
	public float ScreenSpawnPaddingY = 0.5f;

	public float WaitingTimeForNextPresent = 3.0f;
	private float _timeOfLastSpawnedPresent = 0.0f;

	private GameManager _gameManager;

	[Range(-0.01f, 0.01f)]
	public float WindDirection = 0.0f;
	private float _windDirectionStepSize = 0.01f;
	[Range(0.0f, 2.0f)]
	public float WindStrength = 0.0f;
	public readonly float WindStrengthDelta = 0.05f;



	// Use this for initialization
	void Start () {
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		//Todo: check if gamemanager == null

		_stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));

		_presentHolder = new GameObject("Presents").transform;

		
		Dictionary<KeyCallback, Action> player3Actions = new Dictionary<KeyCallback, Action>();
		player3Actions.Add(KeyCallback.KeyOneDown, SetWindDirectionToLeft);
		player3Actions.Add(KeyCallback.KeyOneUp, SetWindDirectionToNeutral);
		player3Actions.Add(KeyCallback.KeyTwoDown, SetWindDirectionToRight);
		player3Actions.Add(KeyCallback.KeyTwoUp, SetWindDirectionToNeutral);
		_gameManager.SetActionsForPlayer(PlayerId.P3, player3Actions);

		Dictionary<KeyCallback, Action> player4Actions = new Dictionary<KeyCallback, Action>();
		player4Actions.Add(KeyCallback.KeyOnePressed, DecreaseWindStrentgh);
		player4Actions.Add(KeyCallback.KeyTwoPressed, IncreaseWindStrentgh);
		_gameManager.SetActionsForPlayer(PlayerId.P4, player4Actions);
	}

	//Player 4
	public void IncreaseWindStrentgh()
	{
		if (WindStrength + WindStrengthDelta > 2.0f) return;
		WindStrength += WindStrengthDelta;
	}

	public void DecreaseWindStrentgh()
	{
		if (WindStrength - WindStrengthDelta < 0.0f) return;
		WindStrength -= WindStrengthDelta;
	}

	//Player 3
	public void SetWindDirectionToLeft()
	{
		if (WindDirection - _windDirectionStepSize < -0.01f) return;
		WindDirection = -_windDirectionStepSize;
	}

	public void SetWindDirectionToNeutral()
	{
		WindDirection = 0.0f;
	}


	public void SetWindDirectionToRight()
	{
		if (WindDirection + _windDirectionStepSize > 0.01f) return;
		WindDirection = _windDirectionStepSize;
	}
	
	// Update is called once per frame
	void Update () {
		//SpawnPresent
		if (Time.time - _timeOfLastSpawnedPresent > WaitingTimeForNextPresent)
		{
			_timeOfLastSpawnedPresent = Time.time;
			SpawnPresent();
		}

		MoveAllPresents();
	}

	private void SpawnPresent()
	{
		float spawnX = UnityEngine.Random.Range(-_stageDimensions.x + ScreenSpawnPaddingX, _stageDimensions.x - ScreenSpawnPaddingX);
		float spawnY = _stageDimensions.y /*+ ScreenSpawnPaddingY*/;
		GameObject go = Instantiate(Present, new Vector3 (spawnX, spawnY), Quaternion.identity) as GameObject;
		go.transform.SetParent(_presentHolder);
		_allPresents.Add(go.GetComponent<Present>());
	}

	private void MoveAllPresents()
	{
		if (_allPresents.Count == 0) return;
		List<Present> toBeRemoved = new List<Present>();
		foreach (Present p in _allPresents)
		{
			//a * x^2 + a
			p.Move(WindDirection * (WindStrength * WindStrength) + WindDirection);
			if (p.transform.position.y < - (_stageDimensions.y + 2.0f))
			{
				toBeRemoved.Add(p);
			}
		}
		//delete if necessary
		foreach (Present p in toBeRemoved)
		{
			
			DeletePresent(p);
		}
	}

	public void DeletePresent(Present p)
	{
		_allPresents.Remove(p);
		p.DestroyPresent();
	}
}
