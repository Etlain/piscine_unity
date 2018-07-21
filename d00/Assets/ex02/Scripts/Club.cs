using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Club : MonoBehaviour {

	// Use this for initialization

	[HideInInspector]
	public float force_distance = 0;
	public bool move_ball = false;
	public bool hit = false;
	public bool ball_up_hole = false;
	public int score = -15;


	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (move_ball == false)
		{
			if (Input.GetKey("space"))
			{
				if (!ball_up_hole)
				{
					this.gameObject.transform.Translate(Vector3.down * 3 * Time.deltaTime);
					force_distance += -1 * 3 * Time.deltaTime;
				}
				else
				{
					this.gameObject.transform.Translate(Vector3.up * 3 * Time.deltaTime);
					force_distance += 1 * 3 * Time.deltaTime;
				}
				hit = true;
			}
			else if (hit == true)
			{
				if (!ball_up_hole)
					this.gameObject.transform.Translate(Vector3.up * (-force_distance));
				else
					this.gameObject.transform.Translate(Vector3.down * (-force_distance));
				if (force_distance > 10 || force_distance < -10)
					force_distance = 5;
				hit = false;
				move_ball = true;
			}
		}
	}
}
