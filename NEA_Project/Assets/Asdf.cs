using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asdf : MonoBehaviour {

    public Vector2 startForce;
    public Rigidbody2D rb;

	void Start () {
        rb.AddForce(startForce);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
