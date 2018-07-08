using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CloudManager : MonoBehaviour {

	public Sprite[] sprites;

	private Vector3 _stageDimensions;

	private Transform _cloudHolder;

	public int MaxAmountOfClouds = 6;
	private int _cloudCount = 0;

	public float WaitingTimeForNextCloud = 7.0f;
	private float _timeOfLastSpawnedCloud = 0.0f;

	private Dictionary<GameObject, Rigidbody2D> _allClouds = new Dictionary<GameObject, Rigidbody2D>();

	private PresentManager _presentManager;

	// Use this for initialization
	void Start () {
		_presentManager = GameObject.Find("PresentManager").GetComponent<PresentManager>();

		_stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));

		_cloudHolder = new GameObject("CloudsHolder").transform;

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - _timeOfLastSpawnedCloud > WaitingTimeForNextCloud)
		{
			_timeOfLastSpawnedCloud = Time.time;
			SpawnCloud();
		}
		MoveAllClouds();
	}
	
	private void SpawnCloud()
	{
		if (_cloudCount >= MaxAmountOfClouds) return;
		GameObject go = new GameObject("Cloud");
		Rigidbody2D rb2d = go.AddComponent<Rigidbody2D>();
		rb2d.constraints = RigidbodyConstraints2D.FreezePositionY;
		rb2d.mass = 0.5f;
		go.transform.SetParent(_cloudHolder);

		SpriteRenderer sr = go.AddComponent<SpriteRenderer>();
		int i = UnityEngine.Random.Range(0, sprites.Length - 1);
		sr.sprite = sprites[i];

		float spawnX = _stageDimensions.x * 1.2f;
		float spawnY = UnityEngine.Random.Range(-_stageDimensions.y * 0.5f, _stageDimensions.y);
		if(_presentManager.GetWindDirection() > 0.0f) spawnX *= -1;
		go.transform.position = new Vector3(spawnX, spawnY, 0f);

		_allClouds.Add(go, rb2d);
		++_cloudCount;
	}

	public void MoveAllClouds()
	{
		if (_cloudCount > 0)
		{
			foreach(Rigidbody2D rb2d in _allClouds.Values)
			{
				rb2d.AddForce( new Vector2(_presentManager.CalcWind(), 0.0f) );
			}
			DeleteClouds();
		}
	}

	public void DeleteClouds()
	{
		float deleteX = _stageDimensions.x * 1.3f;
		List<GameObject> toBeDeleted = new List<GameObject>();
		foreach(GameObject go in _allClouds.Keys)
		{
			if (deleteX - Math.Abs(go.transform.position.x) < 0.01f)
			{
				toBeDeleted.Add(go);
			}
		}
		foreach (GameObject go in toBeDeleted)
		{
			_allClouds.Remove(go);
			Destroy(go);
			--_cloudCount;
		}
	}
}
