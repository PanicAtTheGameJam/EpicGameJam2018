using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Collector : MonoBehaviour {

	[Tooltip("Must be either Player 1 or Player 2.")]
	public PlayerId Playerid;

	public Sprite Sprite;

	private Vector3 _stageDimensions;
	private float HorizontalMovementExtender = 3.0f;
	public float StepSize = 0.1f;

	private GameManager _gameManager;
	private PresentManager _presentManager;

	public GameObject TextNode;
	private Text _text;

	void OnCollisionEnter2D (Collision2D col)
    {
        if(col.gameObject.tag == "Present")
        {
			_gameManager.AddScore(Playerid);
			PlayerId teamMate = PlayerId.P3;
			if (Playerid == PlayerId.P2)
				teamMate = PlayerId.P4;
			_gameManager.AddScore(teamMate);
			_text.text = _gameManager.GetScore(Playerid).ToString();
			_presentManager.DeletePresent(col.gameObject.GetComponent<Present>());
        }
    }

	// Use this for initialization
	void Start () {
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		//Todo: check if gamemanager == null
		_presentManager = GameObject.Find("PresentManager").GetComponent<PresentManager>();

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

		//link to UI component.
		_text = TextNode.GetComponent<Text>();

	}

	public void MoveLeft()
	{
		if (transform.position.x < -_stageDimensions.y - HorizontalMovementExtender) return;
		transform.position = new Vector3 (transform.position.x - StepSize, transform.position.y, 0);
	}

	public void MoveRight()
	{
		if (transform.position.x > _stageDimensions.y + HorizontalMovementExtender) return;
		transform.position = new Vector3 (transform.position.x + StepSize, transform.position.y, 0);
	}
	
}
