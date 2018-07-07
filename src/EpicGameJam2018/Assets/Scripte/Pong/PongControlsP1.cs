using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongControlsP1: MonoBehaviour
{
    public GameObject gameManagerParent;
    public float speed = 30;

    private GameManager gameManager;
    // Use this for initialization
    void Start()
    {
        gameManager = gameManagerParent.GetComponent<GameManager>();
        gameManager.SetActionsForPlayer(GameManager.PlayerId.P1, OnKeyOnePressed, OnKeyTwoPressed);
	}

    private void OnKeyOnePressed()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * speed;
    }

    private void OnKeyTwoPressed()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * speed;
    }
}
