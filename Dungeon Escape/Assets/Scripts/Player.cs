using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed=2f;

    private Rigidbody2D rigidBody;
	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        float translation = Input.GetAxis("Horizontal")*speed;

        rigidBody.velocity = new Vector2(translation,rigidBody.velocity.y);
	}
}
