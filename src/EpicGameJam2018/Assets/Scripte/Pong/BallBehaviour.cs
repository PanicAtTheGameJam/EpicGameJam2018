using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehaviour : MonoBehaviour {
    public float speedLeft { private set; get; }
    public float speedRight { private set; get; }

    public float Speed
    {
        get
        {
            return (speedLeft + speedRight) / 2;
        }
    }

    void Start()
    {
        ResetBall();
    }

    void Update()
    {
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

    public void SetSpeedLeft(float speed)
    {
        speedLeft = speed;
    }

    public void SetSpeedRight(float speed)
    {
        speedRight = speed;
    }
}
