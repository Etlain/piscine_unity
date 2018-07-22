using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

	[HideInInspector]
	private GameObject	bird;
	private float		old_Y;
	private float		old_time = 0;

	// Use this for initialization
	void Start () {
		bird = this.gameObject;
		old_Y = bird.transform.position.y;
	}

	// Update is called once per frame
	void Update () {
		// gestion mouvement bird space sur l axe Y
		if (Input.GetKeyDown("space")){
			old_time = Time.fixedTime;
		}
		float y;
		float t = 4 * (Time.fixedTime - old_time);
		y = -4f * t + 8f;
		//bird.transform.Rotate(Vector3.forward * Time.deltaTime /** 10 * y*//*);
		//print("y : "+y+" time : "+t);
		bird.transform.Translate(Vector3.up * y * Time.deltaTime);
	}
}
