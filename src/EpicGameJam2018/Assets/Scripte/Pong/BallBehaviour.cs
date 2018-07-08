using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallBehaviour : MonoBehaviour {
    public GameObject textP2;
    public GameObject textP4;
    public GameObject textSpeed;
    public GameObject textScoreLeft;
    public GameObject textScoreRight;

    public float speedLeft { private set; get; }
    public float speedRight { private set; get; }

    private int scoreLeft = 0;
    private int scoreRight = 0;

    private AudioSource audioSource;

    public float Speed
    {
        get
        {
            return (speedLeft + speedRight) / 2;
        }
    }

    void Start()
    {
        GetComponent<AudioSource>();
        ResetBall();
        GetComponent<Rigidbody2D>().AddTorque(0.05f);
    }

    void Update()
    {
        textP2.GetComponent<Text>().text = ((int)speedLeft).ToString();
        textP4.GetComponent<Text>().text = ((int)speedRight).ToString();
        textSpeed.GetComponent<Text>().text = ((int)Speed).ToString();
        GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity.normalized * Speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Note: 'col' holds the collision information. If the
        // Ball collided with a racket, then:
        //   col.gameObject is the racket
        //   col.transform.position is the racket's position
        //   col.collider is the racket's collider

        // Hit the left Racket?
        if (col.gameObject.name == "RacketLeft")
        {
            // Calculate hit Factor
            float y = HitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(1, y).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * Speed;
        }

        // Hit the right Racket?
        if (col.gameObject.name == "RacketRight")
        {
            // Calculate hit Factor
            float y = HitFactor(transform.position,
                                col.transform.position,
                                col.collider.bounds.size.y);

            // Calculate direction, make length=1 via .normalized
            Vector2 dir = new Vector2(-1, y).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * Speed;
        }
    }

    private float HitFactor(Vector2 ballPos, Vector2 racketPos,
                float racketHeight)
    {
        // ascii art:
        // ||  1 <- at the top of the racket
        // ||
        // ||  0 <- at the middle of the racket
        // ||
        // || -1 <- at the bottom of the racket
        return (ballPos.y - racketPos.y) / racketHeight;
    }

    public void ResetBall()
    {
        speedLeft = 15;
        speedRight = 15;

        System.Random random = new System.Random();
        int x = random.Next(100);

        GetComponent<Rigidbody2D>().transform.position = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        GetComponent<Rigidbody2D>().velocity = (x > 50 ? Vector2.left : Vector2.right) * Speed;
    }

    public void AddPoint(Side side)
    {
        GameManager gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        switch(side)
        {
            case Side.left:
                gameManager.AddScore(PlayerId.P1);
                gameManager.AddScore(PlayerId.P2);
                textScoreLeft.GetComponent<Text>().text = (++scoreLeft).ToString();
                break;
            case Side.right:
                gameManager.AddScore(PlayerId.P3);
                gameManager.AddScore(PlayerId.P4);
                textScoreRight.GetComponent<Text>().text = (++scoreRight).ToString();
                break;
        }
    }

    public void SetSpeedLeft(float speed)
    {
        speedLeft = speed;
    }

    public void SetSpeedRight(float speed)
    {
        speedRight = speed;
    }
}
