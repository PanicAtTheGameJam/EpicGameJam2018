using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collector : MonoBehaviour {

	[Tooltip("Must be either Player 1 or Player 2.")]
	public PlayerId Playerid;

	public Sprite Sprite;

	private Vector3 _stageDimensions;
	private float HorizontalMovementExtender = 3.0f;
	public float StepSize = 0.1f;

	private GameManager _gameManager;


	// Use this for initialization
	void Start () {
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		//Todo: check if gamemanager == null

		GetComponentInChildren<SpriteRenderer>().sprite = Sprite;

		_stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));

		//spawn position
		float k = 1.0f;
		if (Playerid == PlayerId.P2) k = -1.0f;
		transform.position = new Vector3 (k * _stageDimensions.x * 0.5f, -_stageDimensions.y + 1.0f, 0);

		//setup player actions
		Dictionary<KeyCallback, Action> playerActions = new Dictionary<KeyCallback, Action>();
		playerActions.Add(KeyCallback.KeyOnePressed, MoveLeft);
		playerActions.Add(KeyCallback.KeyTwoPressed, MoveRight);
		_gameManager.SetActionsForPlayer(Playerid, playerActions);

	}

	public void MoveLeft()
	{
		Debug.Log(transform.position);
		if (transform.position.x < -_stageDimensions.y - HorizontalMovementExtender) return;
		transform.position = new Vector3 (transform.position.x - StepSize, transform.position.y, 0);
	}

	public void MoveRight()
	{
		Debug.Log(_stageDimensions);
		if (transform.position.x > _stageDimensions.y + HorizontalMovementExtender) return;
		transform.position = new Vector3 (transform.position.x + StepSize, transform.position.y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
