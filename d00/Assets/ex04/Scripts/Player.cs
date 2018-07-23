using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public string key_up;
	public string key_down;

	[HideInInspector]
	private Player		player;
	private int 		speed;
	public	int 		score;

	// Use this for initialization
	void Start () {
		player = this;
		speed = 2;
		score = 0;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKey(key_up) && player.transform.position.y < 4)
			player.transform.Translate(Vector3.up * Time.deltaTime * speed);
		else if (Input.GetKey(key_down) && player.transform.position.y > -4)
			player.transform.Translate(Vector3.down * Time.deltaTime * speed);
	}
}
