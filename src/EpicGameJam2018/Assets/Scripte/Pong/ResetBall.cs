using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.name == "WallLeft")
        {
            collision.gameObject.GetComponent<BallBehaviour>().AddPoint(Side.left);
        }
        else
        {
            collision.gameObject.GetComponent<BallBehaviour>().AddPoint(Side.right);
        }

        if (collision.gameObject.name == "Ball")
        {
            collision.gameObject.GetComponent<BallBehaviour>().ResetBall();
        }
    }
}
