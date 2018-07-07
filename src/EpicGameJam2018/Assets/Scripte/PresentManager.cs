using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


	// Use this for initialization
	void Start () {
		_stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
		Debug.Log(_stageDimensions);

		_presentHolder = new GameObject("Presents").transform;
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
		float spawnX = Random.Range(-_stageDimensions.x + ScreenSpawnPaddingX, _stageDimensions.x - ScreenSpawnPaddingX);
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
			p.MoveDownwards();
			if (p.transform.position.y < - (_stageDimensions.y + 2.0f))
			{
				toBeRemoved.Add(p);
			}
		}
		//delete if necessary
		foreach (Present p in toBeRemoved)
		{
			
			_allPresents.Remove(p);
			p.DestroyPresent();
		}
	}
}
