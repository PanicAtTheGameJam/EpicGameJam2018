using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarControls: MonoBehaviour
{
    public GameObject gameManagerParent;
    public PlayerId playerId;
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
        gameManager.SetActionsForPlayer(playerId, callbacks);
	}

    private void OnKeyOneDown()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 1) * speed;
    }

    private void OnKeyOnePressed()
    {
        Vector2 vector = GetComponent<Rigidbody2D>().velocity;
        if (vector.x.Equals(0) && vector.y.Equals(0))
        {
            OnKeyOneDown();
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * 1.1f;
        }
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
        Vector2 vector = GetComponent<Rigidbody2D>().velocity;
        if (vector.x.Equals(0) && vector.y.Equals(0))
        {
            OnKeyTwoDown();
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity * 1.1f;
        }
    }

    private void OnKeyTwoUp()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}
