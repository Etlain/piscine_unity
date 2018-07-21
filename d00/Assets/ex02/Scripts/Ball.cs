using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public Club club;
	public GameObject up_border;
	public GameObject down_border;
	public GameObject hole;

	bool start_speed_ball = false;
	float ball_speed = 0;
	bool direction_up = true;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if (club.move_ball == true)
		{
			if (!start_speed_ball)
			{
				ball_speed = club.force_distance * (10.0f);
				if (ball_speed < 0)
					ball_speed = ball_speed * -1;
				start_speed_ball = true;
				if (!club.ball_up_hole)
					direction_up = true;
				else
					direction_up = false;
			}
			float ball_distance = ball_speed * Time.deltaTime;
			if (direction_up)
			{
				if (up_border.transform.localPosition.y > ball_distance + this.gameObject.transform.localPosition.y)
					this.gameObject.transform.Translate(Vector3.up * ball_distance);
				else
				{
					this.gameObject.transform.Translate(Vector3.up * (up_border.transform.localPosition.y - this.gameObject.transform.localPosition.y));
					direction_up = false;
				}
			}
			else
			{
				if (down_border.transform.localPosition.y < this.gameObject.transform.localPosition.y - ball_distance)
					this.gameObject.transform.Translate(Vector3.down * ball_distance);
				else
				{
					this.gameObject.transform.Translate(Vector3.down * (this.gameObject.transform.localPosition.y - down_border.transform.localPosition.y));
					direction_up = true;
				}
				//this.gameObject.transform.Translate(Vector3.down * ball_distance);
			}

			//print("before ball_speed : "+ball_speed);
			ball_speed = ball_speed - 1.0f;
			//club.transform.localPosition = new Vector3 (this.gameObject.localPosition.x, 0, 0);
			//print("after ball_speed : "+ball_speed);
			if (ball_speed < 0)
			{
				print("finish");
				club.force_distance = 0;
				club.move_ball = false;
				start_speed_ball = false;
			}
			if (this.gameObject.transform.localPosition.y > hole.transform.localPosition.y)
				club.ball_up_hole = true;
			else
				club.ball_up_hole = false;
			//club.force_distance = 0;
			//club.move_ball = false;


			//if ()
			//print(club.force_distance);
		}
	}
}
