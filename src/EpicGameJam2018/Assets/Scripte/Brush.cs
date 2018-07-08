using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Brush : MonoBehaviour {
	
	public float BrushStepSize = 0.05f;
	public PlayerId TeamMember1;
	public PlayerId TeamMember2;

	private GameManager _gameManager;

	private Vector3 _stageDimensions;

	// Use this for initialization
	void Start () {
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		_stageDimensions = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height,0));

		Dictionary<KeyCallback, Action> teamMember1Actions = new Dictionary<KeyCallback, Action>();
		teamMember1Actions.Add(KeyCallback.KeyOnePressed, MoveBrushUp);
		teamMember1Actions.Add(KeyCallback.KeyTwoPressed, MoveBrushDown);
		_gameManager.SetActionsForPlayer(TeamMember1, teamMember1Actions);

		Dictionary<KeyCallback, Action> teamMember2Actions = new Dictionary<KeyCallback, Action>();
		teamMember2Actions.Add(KeyCallback.KeyOnePressed, MoveBrushLeft);
		teamMember2Actions.Add(KeyCallback.KeyTwoPressed, MoveBrushRight);
		_gameManager.SetActionsForPlayer(TeamMember2, teamMember2Actions);
	}
	
	private void MoveBrushUp()
	{
		//if (transform.position.y < _stageDimensions.y) return;
		transform.position = new Vector3(transform.position.x, transform.position.y - BrushStepSize, 0);
	}

	private void MoveBrushDown()
	{
		transform.position = new Vector3(transform.position.x, transform.position.y + BrushStepSize, 0);
	}

	private void MoveBrushLeft()
	{
		transform.position = new Vector3(transform.position.x - BrushStepSize, transform.position.y, 0);
	}

	private void MoveBrushRight()
	{
		transform.position = new Vector3(transform.position.x + BrushStepSize, transform.position.y, 0);
	}
}
