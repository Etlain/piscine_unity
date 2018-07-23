using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

	public GameObject	ground;

	[HideInInspector]
	private Bird		bird;
	private float		old_Y;
	private float		old_time = 0;
	private Vector3 	tmp;
	private bool		alive;

	// Use this for initialization
	void Start () {
		bird = this;
		old_Y = bird.transform.position.y;
		tmp = bird.transform.position;
		alive = true;
	}

	public void dead()
	{
		alive = false;
	}

	// Update is called once per frame
	void Update () {
		// gestion mouvement bird space sur l axe Y
		if (alive)
		{
			if (Input.GetKeyDown("space")){
				old_time = Time.fixedTime;
			}
			float y;
			float t = 4 * (Time.fixedTime - old_time);
			y = -4f * t + 8f;
			//bird.transform.Rotate(Vector3.forward * Time.deltaTime /** 10 * y*//*);
			//print("y : "+y+" time : "+t);
			bird.transform.Translate(Vector3.up * y * Time.deltaTime);
			//bird.transform.Rotate(0f,0f,60f * Time.deltaTime, Space.Self);
			//print("space : "+Space.World);
			if (ground.transform.position.y - 0.3 <= bird.transform.position.y && ground.transform.position.y + 0.3 >= bird.transform.position.y)
			{
				bird.dead();
			}
		}
		//bird.transform.Rotate(0f,0f,y, Space.Self);
	}
}
