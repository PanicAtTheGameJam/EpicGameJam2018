using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour {

	public float HorizontalFallSpeed = 0.01f;

	void Start(){

	}

	public void MoveDownwards() { 
		transform.position = new Vector3(transform.position.x, transform.position.y - HorizontalFallSpeed, 0);
	}

	public void DestroyPresent()
	{
			Destroy(gameObject);
	}
	
}
