using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentManager : MonoBehaviour {

	public GameObject Present;

	//Left screen border in worldspace = - stageDimensions.x,
	//right screen border in wordlspace = stageDimensions.x,
	//center of screen in worldspace = 0;
	//thus spawning range x = [-_stageDimensions.x, _stageDimensions.x];
	private Vector3 _stageDimensions;
	public float ScreenSpawnPaddingX = 0.05f;
	public float ScreenSpawnPaddingY = 0.5f;

	public float WaitingTimeForNextPresent = 0.5f;
	private float _timeOfLastSpawnedPresent = 0.0f;


	// Use this for initialization
	void Start () {
		_stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));
		Debug.Log(_stageDimensions);

		
	}
	
	// Update is called once per frame
	void Update () {
		//SpawnPresent
		if (Time.time - _timeOfLastSpawnedPresent > WaitingTimeForNextPresent)
		{
			_timeOfLastSpawnedPresent = Time.time;
			SpawnPresent();
		}
	}

	private void SpawnPresent()
	{
		float spawnX = Random.Range(-_stageDimensions.x + ScreenSpawnPaddingX, _stageDimensions.x - ScreenSpawnPaddingX);
		float spawnY = _stageDimensions.y /*+ ScreenSpawnPaddingY*/;
		GameObject go = Instantiate(Present, new Vector3 (spawnX, spawnY), Quaternion.identity) as GameObject;
	}
}
