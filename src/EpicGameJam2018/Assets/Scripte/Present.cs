using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour {

	public float HorizontalFallSpeed = 0.01f;
	private Rigidbody2D rb2d;

	void Awake()
	{
		rb2d = gameObject.GetComponent<Rigidbody2D>();
	}

	public void Move(float wind = 0.0f) { 
		if (wind == 0.0f) wind = 0.001f;
		transform.position = new Vector3(transform.position.x, transform.position.y - HorizontalFallSpeed, 0);
		rb2d.AddForce( new Vector2(wind, 0.0f) );
	}

	public void DestroyPresent()
	{
			Destroy(gameObject);
	}
	
}
