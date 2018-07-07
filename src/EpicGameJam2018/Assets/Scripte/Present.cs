using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour {

	public float HorizontalFallSpeed = 0.01f;

	void Start(){

	}

	public void MoveDownwards(float wind = 0.0f) { 
		if (wind == 0.0f) wind = 0.001f;
		transform.position = new Vector3(transform.position.x + wind, transform.position.y - HorizontalFallSpeed, 0);
	}

	public void DestroyPresent()
	{
			Destroy(gameObject);
	}
	
}
