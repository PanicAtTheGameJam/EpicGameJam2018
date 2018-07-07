using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Side { left, right }

public class SpeedControls : MonoBehaviour {
    private readonly float MAX = 50;
    private readonly float MIN = 5;
    private readonly float STEPS = 0.5f;

    public GameObject gameManagerObject;
    public GameObject ballObject;
    public PlayerId playerId;
    public Side side;

    private GameManager gameManager;
    private BallBehaviour ballBehaviour;

	void Start () {
        gameManager = gameManagerObject.GetComponent<GameManager>();
        ballBehaviour = ballObject.GetComponent<BallBehaviour>();

        Dictionary<KeyCallback, Action> callbacks = new Dictionary<KeyCallback, Action>();
        callbacks.Add(KeyCallback.KeyOnePressed, OnKeyOnePressed);
        callbacks.Add(KeyCallback.KeyTwoPressed, OnKeyTwoPressed);

        gameManager.SetActionsForPlayer(playerId, callbacks);
	}

    private void OnKeyOnePressed()
    {
        if (side == Side.left)
        {
            if (ballBehaviour.speedLeft > MIN)
            {
                ballBehaviour.SetSpeedLeft(ballBehaviour.speedLeft - STEPS);
            }
        }
        else
        {
            if (ballBehaviour.speedRight > MIN)
            {
                ballBehaviour.SetSpeedRight(ballBehaviour.speedRight - STEPS);
            }
        }
    }
	
    private void OnKeyTwoPressed()
    {
        if(side == Side.left)
        {
            if(ballBehaviour.speedLeft < MAX)
            {
                ballBehaviour.SetSpeedLeft(ballBehaviour.speedLeft + STEPS);
            }
        }
        else
        {
            if (ballBehaviour.speedRight < MAX)
            {
                ballBehaviour.SetSpeedRight(ballBehaviour.speedRight + STEPS);
            }
        }
    }
}
