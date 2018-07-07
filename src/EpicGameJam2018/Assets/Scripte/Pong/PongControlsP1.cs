using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongControlsP1: MonoBehaviour
{
    public GameObject gameManagerParent;
    public float speed = 5;

    private GameManager gameManager;
    // Use this for initialization
    void Start()
    {
        Dictionary<KeyCallback, Action> callbacks = new Dictionary<KeyCallback, Action>();
        callbacks.Add(KeyCallback.KeyOneDown, OnKeyOneDown);
        callbacks.Add(KeyCallback.KeyOnePressed, OnKeyOnePressed);
        callbacks.Add(KeyCallback.KeyOneUp, OnKeyOneUp);
        callbacks.Add(KeyCallback.KeyTwoDown, OnKeyTwoDown);
        callbacks.Add(KeyCallback.KeyTwoPressed, OnKeyTwoPressed);
        callbacks.Add(KeyCallback.KeyTwoUp, OnKeyTwoUp);

        gameManager = gameManagerParent.GetComponent<GameManager>();
        gameManager.SetActionsForPlayer(PlayerId.P1, callbacks);
	}

    private void OnKeyOneDown()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * speed;
    }

    private void OnKeyOnePressed()
    {
        GetComponent<Rigidbody2D>().velocity.Scale(new Vector2(0, 1.5f));
    }

    private void OnKeyOneUp()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    private void OnKeyTwoDown()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, -1) * speed;
    }

    private void OnKeyTwoPressed()
    {
        GetComponent<Rigidbody2D>().velocity.Scale(new Vector2(0, -1.5f));
    }

    private void OnKeyTwoUp()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}
